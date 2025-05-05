

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _kege_sft_form
{
    public partial class Edit_frm : Form
    {
        public RegisteredSoftware editable_item;
        public int index;

        private Action update_lv;
        public Edit_frm(Action update_lv)
        {
            InitializeComponent();
            this.update_lv = update_lv;
            //подписка на события
            type.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            langs.Click += langs_Click;
            ok_btn.Focus();
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
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Edit_frm_Load(object sender, EventArgs e)
        {
            name.Text = editable_item.Name;
            version.Text = editable_item.Version;

            // Устанавливаем значение типа
            var typeMapping = new List<string> { "IDE", "TextEditor", "Spreadsheet" };
            int typeIndex = typeMapping.IndexOf(editable_item.SoftwareType);
            if (typeIndex >= 0)
            {
                type.SelectedIndex = typeIndex;
            }

            // Устанавливаем языки, если это IDE
            if (editable_item.SoftwareType == "IDE" && !string.IsNullOrEmpty(editable_item.ProgrammingLanguage))
            {
                var langMapping = new List<string> { "CSharp", "CPlusPlus", "Java", "Python", "Pascal", "Sal" };
                var selectedLangs = editable_item.ProgrammingLanguage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < langs.Items.Count; i++)
                {
                    if (selectedLangs.Contains(langMapping[i]))
                    {
                        langs.SetItemChecked(i, true);
                    }
                }
                langs.Enabled = true;
            }
            else
            {
                langs.Enabled = false;
            }
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (type.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (version.Text.Length == 0)
            {
                MessageBox.Show("Введите версию добавляемого ПО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeMapping = new List<string> { "IDE", "TextEditor", "Spreadsheet" };
            editable_item.SoftwareType = typeMapping[type.SelectedIndex];

            if (editable_item.SoftwareType == "IDE")
            {
                var langMapping = new List<string> { "CSharp", "CPlusPlus", "Java", "Python", "Pascal", "Sal" };
                StringBuilder selectedLangs = new StringBuilder();

                for (int i = 0; i < langs.Items.Count; i++)
                {
                    if (langs.GetItemChecked(i))
                    {
                        selectedLangs.Append(langMapping[i]).Append(" ");
                    }
                }

                editable_item.ProgrammingLanguage = selectedLangs.ToString().Trim();
            }
            else
            {
                editable_item.ProgrammingLanguage = null;
            }

            editable_item.RegisterType = "Dictionary";
            editable_item.Name = name.Text;
            editable_item.Version = version.Text;

            ch_kege_sft_frm.programs[index] = editable_item;
            update_lv();
            this.Close();
        }

    }
}
