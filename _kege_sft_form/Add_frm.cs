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

namespace _kege_sft_form
{
    public partial class Add_frm : Form
    {
       
        private Action update_lv;
        public Add_frm(Action update_lv)
        {
            InitializeComponent();
            this.update_lv = update_lv;
            langs.Click += langs_Click;
        }
        
        private void langs_Click(object sender, EventArgs e)
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
            // Проверяем условие SoftwareType
            if (added_prog.SoftwareType == "IDE")
            {
                StringBuilder selectedLangsStringBuilder = new StringBuilder();

                foreach (object item in langs.CheckedItems)
                {
                    selectedLangsStringBuilder.Append(item.ToString());
                    selectedLangsStringBuilder.Append(" "); // Добавляем пробел между элементами
                }

                added_prog.ProgrammingLanguage = selectedLangsStringBuilder.ToString().Trim(); // Присваиваем значение переменной ProgrammingLanguage
            }
            else
            {
                added_prog.ProgrammingLanguage = null; // Присваиваем null, если условие не выполняется
            }
            added_prog.Id = (ch_kege_sft_frm.programs.Count).ToString();
            added_prog.RegisterType = "Dictionary";
            added_prog.SoftwareType = "IDE";
            added_prog.Name = name.Text;
            added_prog.Version = version.Text;
            
            ch_kege_sft_frm.programs.Add(added_prog);
            ch_kege_sft_frm.list_groups.AddRange(new[] { added_prog.SoftwareType });
            update_lv();
            this.Close();




        }

        
    }
}
