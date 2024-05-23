

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
        public RegisteredSoftware editabel_item;
        public int index;

        private Action update_lv;
        public Edit_frm(Action update_lv)
        {
            InitializeComponent();
            this.update_lv = update_lv;
            ok_btn.Focus();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Edit_frm_Load(object sender, EventArgs e)
        {
            name_tb.Text = editabel_item.Name;
            version_tb.Text = editabel_item.Version;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            editabel_item.Name = name_tb.Text;
            editabel_item.Version = version_tb.Text;

            ch_kege_sft_frm.programs[index] = editabel_item;
            update_lv();
            this.Close();
        }
    }
}
