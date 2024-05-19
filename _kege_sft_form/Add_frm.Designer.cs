namespace _kege_sft_form
{
    partial class Add_frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_frm));
            this.add_btn = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.version = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.langs = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // add_btn
            // 
            this.add_btn.Location = new System.Drawing.Point(70, 211);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(75, 23);
            this.add_btn.TabIndex = 0;
            this.add_btn.Text = "Сохранить";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // type
            // 
            this.type.AutoCompleteCustomSource.AddRange(new string[] {
            "IDE",
            "Text Editor",
            "Spreadsheets"});
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "IDE",
            "TextEditor",
            "Spreadsheet"});
            this.type.Location = new System.Drawing.Point(70, 12);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(121, 21);
            this.type.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тип";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Язык";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(70, 140);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(121, 20);
            this.name.TabIndex = 6;
            // 
            // version
            // 
            this.version.Location = new System.Drawing.Point(70, 173);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(121, 20);
            this.version.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Название";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Версия";
            // 
            // langs
            // 
            this.langs.FormattingEnabled = true;
            this.langs.Items.AddRange(new object[] {
            "CSharp",
            "CPlusPlus",
            "Java",
            "Python",
            "Pascal",
            "Sal"});
            this.langs.Location = new System.Drawing.Point(70, 39);
            this.langs.Name = "langs";
            this.langs.Size = new System.Drawing.Size(120, 94);
            this.langs.TabIndex = 10;
            // 
            // Add_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 264);
            this.Controls.Add(this.langs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.version);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.type);
            this.Controls.Add(this.add_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Add_frm";
            this.Text = "Добавить";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox version;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox langs;
    }
}