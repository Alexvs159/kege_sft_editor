using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections;

namespace _kege_sft_form
{
    public partial class ch_kege_sft_frm : Form
    {
        public static List<RegisteredSoftware> programs = new List<RegisteredSoftware>();
        static List<RegisteredSoftware> selected_programs = new List<RegisteredSoftware>();
        RegisteredSoftware current_programm = new RegisteredSoftware();
        public static List<string> list_groups = new List<string>();
        public List<string> list_group_del = new List<string>();
        string fileText;
        string decode_file;
        string selected_item_id;


        string save_path;

        public ch_kege_sft_frm()
        {
            InitializeComponent();
            
            openFileDialog1.Filter = "SFT files(*.sft)|*.sft";
            openFileDialog1.FileName = "";
            
        }

        private void openBTN_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            programs.Clear();
            selected_programs.Clear();
            list_groups.Clear();
            list_group_del.Clear();
            fileText = "";
            decode_file = "";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл
            fileText = File.ReadAllText(filename);
            textBox1.Text = filename;
            textBox2.Text = $"SOFT_KEGE_NEW_{DateTime.Now:yyyyMMdd}.sft";
            save_path = Path.GetDirectoryName(openFileDialog1.FileName);
            save_path = save_path + textBox2.Text;

            // декодируем в биты
            var decode_file_bit = Convert.FromBase64String(fileText);

            // переводим биты в текст
            decode_file = Encoding.UTF8.GetString(decode_file_bit);

            // сохраняем XML по пути корня программы с именем temp.xml
            FileStream Swr = new FileStream("temp.xml", FileMode.Create, FileAccess.Write);
            byte[] arr = Encoding.Unicode.GetBytes(decode_file);
            Swr.Write(arr, 0, arr.Length);
            Swr.Close();
            Swr.Dispose();

            textBox2.Text = save_path;

            Work_XML();
        }

        private void Work_XML()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Environment.CurrentDirectory + @"\temp.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    current_programm = new RegisteredSoftware();
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел - id
                        if (childnode.Name == "Id")
                        {
                            current_programm.Id = childnode.InnerText;
                        }
                        // если узел RegisterType
                        if (childnode.Name == "RegisterType")
                        {
                            current_programm.RegisterType = childnode.InnerText;
                        }

                        if (childnode.Name == "SoftwareType")
                        {
                            current_programm.SoftwareType = childnode.InnerText;
                        }

                        if (childnode.Name == "Name")
                        {
                            current_programm.Name = childnode.InnerText;
                        }

                        if (childnode.Name == "Version")
                        {
                            current_programm.Version = childnode.InnerText;
                        }

                        if (childnode.Name == "ProgrammingLanguage")
                        {
                            current_programm.ProgrammingLanguage = childnode.InnerText;
                        }
                    }
                    programs.Add(current_programm);
                    list_groups.AddRange(new[] { current_programm.SoftwareType });
                    
                }
            }
            UpdateID();
            UpdateLV();
        }

        public void UpdateLV()
        //Обновление списка ПО в интерфейсе

        {
            list_group_del = list_groups.Distinct().ToList();
            listView1.Items.Clear();
            listView1.View = View.Details;
            // Ищем все группы и добавляем в лист итем
            foreach (string grp in list_group_del)
            {
                listView1.Groups.Add(new ListViewGroup(grp.ToString()));
            }
            // добавляем элементы в лист итем
            foreach (RegisteredSoftware program in programs)
            {
                // Создаем новый элемент ListViewItem с двумя колонками
                ListViewItem item = new ListViewItem(new string[] { program.Name, program.Version });
                // Определяем группу
                for (int i = 0; i < listView1.Groups.Count; i++)
                {
                    if (program.SoftwareType == listView1.Groups[i].Header) { item.Group = listView1.Groups[i]; }
                }
                // Добавляем элемент в ListView
                item.Checked = true;
                item.Tag = program.Id;
                listView1.Items.Add(item);
            }

            if (programs.Count > 0)
            {
                if (string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    save_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\SOFT_KEGE_NEW_{DateTime.Now:yyyyMMdd}.sft"; ;
                }
                else
                {
                    save_path = Path.GetDirectoryName(openFileDialog1.FileName) + $"SOFT_KEGE_NEW_{DateTime.Now:yyyyMMdd}.sft";
                }

                textBox2.Text = save_path;
                saveBTN.Enabled = true;
            }
        }

        public void UpdateID()
        {
            int k = 1;
            foreach (RegisteredSoftware prog in programs)
            {
                prog.Id = k.ToString();
                ++k;
            }
        }
        private void saveBTN_Click(object sender, EventArgs e)
        {
            save_path = textBox2.Text;
            var selectedLV = listView1.CheckedItems;

            for (int i = 0; i < programs.Count; i++)
            {
                for (int j = 0; j < selectedLV.Count; j++)
                {
                    if (programs[i].Name == selectedLV[j].Text)
                    {
                        selected_programs.Add(programs[i]);
                    }
                }
            }

            SaveXML();
            selected_programs.Clear();
            programs.Clear();
        }

        private void SaveXML()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.GetEncoding("utf-16");
            xmlWriterSettings.Indent = true;

            XmlWriter xW = XmlWriter.Create("new.xml", xmlWriterSettings);

            xW.WriteStartDocument();
            xW.WriteStartElement("ArrayOfRegisteredSoftware");
            xW.WriteAttributeString(@"xmlnsxsd", @"http://www.w3.org/2001/XMLSchema");
            xW.WriteAttributeString(@"xmlnsxsi", @"http://www.w3.org/2001/XMLSchema-instance");

            for (int i = 0; i < selected_programs.Count; i++)
            {
                xW.WriteStartElement("RegisteredSoftware");

                xW.WriteStartElement("Id");
                xW.WriteString(selected_programs[i].Id);
                xW.WriteEndElement();

                xW.WriteStartElement("RegisterType");
                xW.WriteString(selected_programs[i].RegisterType);
                xW.WriteEndElement();

                xW.WriteStartElement("SoftwareType");
                xW.WriteString(selected_programs[i].SoftwareType);
                xW.WriteEndElement();

                xW.WriteStartElement("Name");
                xW.WriteString(selected_programs[i].Name);
                xW.WriteEndElement();

                xW.WriteStartElement("Version");
                xW.WriteString(selected_programs[i].Version);
                xW.WriteEndElement();

                xW.WriteStartElement("ProgrammingLanguage");
                xW.WriteString(selected_programs[i].ProgrammingLanguage);
                xW.WriteEndElement();

                xW.WriteEndElement();
            }
            xW.WriteEndDocument();
            xW.Close();

            toBase64();
        }

        private void toBase64()
        {
            string new_xml_file_text = File.ReadAllText("new.xml");
            byte[] code_data = Encoding.UTF8.GetBytes(new_xml_file_text);
            var code_file_to64 = Convert.ToBase64String(code_data);
            code_data = Encoding.UTF8.GetBytes(code_file_to64);

            FileStream fstream = null;
            try
            {
                if (File.Exists(save_path)) { File.Delete(save_path); }
                fstream = new FileStream(save_path, FileMode.OpenOrCreate);
                fstream.Write(code_data, 0, code_data.Length);
                fstream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка записи", MessageBoxButtons.OK);
            }
            finally
            {
                //bool csf = ;

                if (check_save_file())
                {
                    MessageBox.Show("Файл сохранен по пути: " + save_path, "Файл сохранен", MessageBoxButtons.OK);
                }
            }
        }

        bool check_save_file()
        {
            try
            {
                fileText = File.ReadAllText(save_path);
                // декодируем в биты
                var decode_file_bit = Convert.FromBase64String(fileText);
                return true;
            }
            catch { MessageBox.Show("Ошибка", "Файл не сохранен", MessageBoxButtons.OK); return false; }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Edit_frm edit_frm = new Edit_frm(this.UpdateLV) { Owner = this };
            if (listView1.SelectedItems.Count == 1)
            {
                using (var edit_form = new Edit_frm(this.UpdateLV))
                {
                    //определяем редактируемый элемент
                    selected_item_id = listView1.SelectedItems[0].Tag.ToString();
                    for (int i = 0; i < programs.Count; i++) 
                    {
                        if (listView1.SelectedItems[0].Tag.ToString() == programs[i].Id) 
                        { 
                            //передаем редактируемый элемент в форму редактирования
                            edit_form.editable_item = programs[i];
                            edit_form.index = i;
                            break; 
                        }
                    } 
                    //показываем форму
                    edit_form.ShowDialog();
                    EditBtn.Enabled = false; 
                    RemoveBtn.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Ошибка", "Выберите один элемент", MessageBoxButtons.OK);
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        //пока не выбран элемент, кнопки удалить и редактировать недоступны
        {
            if (listView1.SelectedItems.Count != 0)
            {
                EditBtn.Enabled = true;
                RemoveBtn.Enabled = true;
            }
            else { EditBtn.Enabled = false; RemoveBtn.Enabled = false;}
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {

            Add_frm addfrm = new Add_frm(this.UpdateLV) { Owner = this };
            using (var edit_form = new Edit_frm(this.UpdateLV))
                addfrm.ShowDialog();
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            List<RegisteredSoftware> progs_todel = new List<RegisteredSoftware>();
            List<Int32> indexes_todel = new List<int> { };
            foreach (ListViewItem todel in listView1.SelectedItems)
            {
                for (int i = 0; i < programs.Count; i++)
                {
                    if (programs[i].Id == todel.Tag.ToString())
                    {
                        indexes_todel.Add(i);
                    }
                }
                
             }
            indexes_todel.Sort((a, b) => b.CompareTo(a));
            foreach (int index in indexes_todel)
            {
                programs.RemoveAt(index);
            }
            UpdateID();
            UpdateLV();
            EditBtn.Enabled = false;
            RemoveBtn.Enabled = false;
        }

    }
 
        }