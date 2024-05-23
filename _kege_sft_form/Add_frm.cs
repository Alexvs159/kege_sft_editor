using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace _kege_sft_form
{
    public partial class Add_frm : Form
    {
       
        private Action update_lv;
        public Add_frm(Action update_lv)
        {
            InitializeComponent();
            this.update_lv = update_lv;
            //подписка на события
            type.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            langs.Click += langs_Click;
            add_btn.Focus();
        }
        
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем выбранный элемент в ComboBox
            if (type.SelectedIndex == 0)
            {
                // Включаем CheckedListBox
                langs.Enabled = true;
            }
            else 
            {
                // Выключаем CheckedListBox
                for (int i = 0; i < langs.Items.Count; i++) { langs.SetItemCheckState(i, CheckState.Unchecked); }
                langs.Enabled = false;
            }
        }
        
        private void langs_Click(object sender, EventArgs e)
        //обработка выбора языка программирования, чтобы чекбокс включался по одиночному клику
        {
            Point point = langs.PointToClient(Cursor.Position);
            int index = langs.IndexFromPoint(point);

            if (index != ListBox.NoMatches)
            {
                bool newState = !langs.GetItemChecked(index);
                langs.SetItemChecked(index, newState);
            }
        }
        
        private void add_btn_Click(object sender, EventArgs e)
        {
            RegisteredSoftware added_prog = new RegisteredSoftware();
            if (type.SelectedItem != null)
            {
                added_prog.SoftwareType = type.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать тип", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (version.Text.Length == 0) { MessageBox.Show("Введите версию добавляемого ПО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            List<String> TypesToFile = new List<string> { "IDE", "TextEditor", "Spreadsheet" };
            added_prog.SoftwareType = TypesToFile[type.SelectedIndex];
            // Проверяем условие SoftwareType
            if (added_prog.SoftwareType == "IDE")
            {
                List<String> LangsToFile = new List<string> { "CSharp", "CPlusPlus", "Java", "Python", "Pascal", "Sal"};
                StringBuilder selectedLangsStringBuilder = new StringBuilder();

                for (int i = 0; i < langs.Items.Count; i++)
                {
                    if (langs.GetItemChecked(i))
                    {
                        selectedLangsStringBuilder.Append(LangsToFile[i]);
                        selectedLangsStringBuilder.Append(" "); // Добавляем пробел между элементами
                    }
                }
                                
                added_prog.ProgrammingLanguage = selectedLangsStringBuilder.ToString().Trim(); // Присваиваем значение переменной ProgrammingLanguage
            }
            else
            {
                added_prog.ProgrammingLanguage = null; // Присваиваем null, если условие не выполняется
            }
            added_prog.Id = (ch_kege_sft_frm.programs.Count + 1).ToString();
            added_prog.RegisterType = "Dictionary";
            added_prog.Name = name.Text;
            added_prog.Version = version.Text;
            
            ch_kege_sft_frm.programs.Add(added_prog);
            ch_kege_sft_frm.list_groups.AddRange(new[] { added_prog.SoftwareType });
            update_lv();
            this.Close();




        }

        
    }
}
