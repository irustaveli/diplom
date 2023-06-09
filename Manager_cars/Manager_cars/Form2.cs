﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Manager_cars
{
    public partial class Form2 : MaterialForm
    {
        public Form2()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }
        public Image ThePicture
        {
            get { return this.pictureBox1.Image; }
        }
        DataTable dtData = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            string str = "Редактировать";
            LoadData(str);
            List<dynamic> User = new List<dynamic>();
            User = db.SelectUser(label10.Text);
            label7.Text = User[0].FIO;
            label9.Text = User[0].info;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
        }
        public void LoadData(string str)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Cars = new List<dynamic>();
            Cars = db.SelectCars();
            if (str == "Редактировать")
            {
                dataGridView1.Columns.Add("Column1", "id_car_name");
                dataGridView1.Columns.Add("Column2", "Наименования автомобиля");
                dtData.Columns.Add("Column2", typeof(string));
                dataGridView1.Columns.Add("Column3", "id_models");
                dataGridView1.Columns.Add("Column4", "Модель");
                dtData.Columns.Add("Column4", typeof(string));
                dataGridView1.Columns.Add("Column5", "Количество");
                dtData.Columns.Add("Column5", typeof(string));
                dataGridView1.Columns.Add("Column6", "id_car_info");
                dataGridView1.Columns.Add("Column7", "Информация по модели");
                dtData.Columns.Add("Column7", typeof(string));
                dataGridView1.Columns.Add("Column8", "Цена");
                dtData.Columns.Add("Column8", typeof(string));
                dataGridView1.Columns.Add("Column9", "ImageData");
                dataGridView1.Columns["Column1"].Visible = false;
                dataGridView1.Columns["Column2"].Visible = false;
                dataGridView1.Columns["Column3"].Visible = false;
                dataGridView1.Columns["Column6"].Visible = false;
                dataGridView1.Columns["Column9"].Visible = false;
                foreach (var i in Cars)
                {
                    dataGridView1.Rows.Add(i.id_car_name, i.car_name, i.id_modelse, i.models, i.quantity, i.id_info_models, i.info, i.price, i.Data);
                    dtData.Rows.Add(i.car_name, i.models, i.quantity, i.info, i.price);
                }
                dataGridView1.Columns[3].Width = 130;
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[6].Width = 650;
                dataGridView1.Columns[7].Width = 100;
            }
            else if (str == "Обновить")
            {
                dataGridView1.Rows.Clear();
                foreach (var i in Cars)
                {
                    dataGridView1.Rows.Add(i.id_car_name, i.car_name, i.id_modelse, i.models, i.quantity, i.id_info_models, i.info, i.price, i.Data);
                }
                dataGridView1.Columns[3].Width = 130;
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[6].Width = 650;
                dataGridView1.Columns[7].Width = 100;
            }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
            f.Text = "Добавить";
            this.Hide(); // закрытие текущий формы
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            List<dynamic> sb = new List<dynamic>();
            byte[] byteArr;
            for (int i = 1; i <= dataGridView1.ColumnCount; i++)
            {
                sb.Add(selectedRow.Cells[$@"Column{i}"].Value);
            }
            byteArr = sb[8];
            if (byteArr != null)
            {
                pictureBox1.Image = ByteToImage(byteArr);
            }
            f7.text_box5 = sb[0].ToString();
            f7.text_box1 = sb[1].ToString();
            f7.text_box6 = sb[2].ToString();
            f7.text_box2 = sb[3].ToString();
            f7.text_box3 = sb[4].ToString();
            f7.text_box7 = sb[5].ToString();
            f7.text_box4 = sb[7].ToString();
            f7.ThePicture = pictureBox1.Image;
            f7.rich_text_box1 = sb[6].ToString();
            f7.Text = "Редактировать";
            f7.Show();
            this.Hide(); // закрытие текущий формы
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string id_car = Convert.ToString(selectedRow.Cells["Column1"].Value);
            string id_models = Convert.ToString(selectedRow.Cells["Column3"].Value);
            string id_info = Convert.ToString(selectedRow.Cells["Column6"].Value);
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            var result = db.DelData(id_car, id_models, id_info);
            if (result == 1)
            {
                MessageBox.Show("Данные удалились", "Сообщение");
            }
            string str = "Обновить";
            this.LoadData(str);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }

        private void materialTextBox21_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Марка LIKE '%{materialTextBox21.Text}%'";
            if (materialTextBox21.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }

        }

        private void materialTextBox22_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Модель LIKE '%{materialTextBox22.Text}%'";
            if (materialTextBox22.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }

        private void materialTextBox23_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Цена LIKE '%{materialTextBox23.Text}%'";
            if (materialTextBox23.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }
    }
}
