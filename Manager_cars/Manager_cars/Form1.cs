﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Manager_cars
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }

        public string LbL
        {
            get { return label10.Text; }
            set { label10.Text = value; }
        }
        public string LbL7
        {
            get { return label7.Text; }
        }
        public Image ThePicture
        {
            get { return this.pictureBox1.Image; }
        }
        DataTable dtData = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Cars = new List<dynamic>();
            Cars = db.SelectCars();
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
            string count_booking = db.countBooking();
            string countTestDrive = db.countTestDrive();
            label1.Text = count_booking.ToString();
            label2.Text = countTestDrive.ToString();
            List<dynamic> User = new List<dynamic>();
            User = db.SelectUser(label10.Text);
            label7.Text = User[0].FIO;
            label9.Text = User[0].info;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label8.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            label6.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            label8.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Text = "Просмотр информации автомобиля";
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            List<dynamic> sb = new List<dynamic>();
            byte[] byteArr;
            for (int i = 1; i <= dataGridView1.ColumnCount; i++)
            {
                sb.Add(selectedRow.Cells[$@"Column{i}"].Value);
            }
            f7.text_box5 = sb[0].ToString();
            f7.text_box1 = sb[1].ToString();
            f7.text_box6 = sb[2].ToString();
            f7.text_box2 = sb[3].ToString();
            f7.text_box3 = sb[4].ToString();
            f7.text_box7 = sb[5].ToString();
            f7.text_box4 = sb[7].ToString();
            f7.rich_text_box1 = sb[6].ToString();
            byteArr = sb[8];
            pictureBox1.Image = ByteToImage(byteArr);
            f7.ThePicture = pictureBox1.Image;
            f7.Show();
            this.Hide(); // закрытие текущий формы
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Модель LIKE '%{materialTextBox1.Text}%'";
            if (materialTextBox1.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }

        private void materialTextBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Модель LIKE '%{materialTextBox2.Text}%'";
            if (materialTextBox2.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }

        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Марка";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Количество";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Информация по модели";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Цена"; ;
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Модель LIKE '%{materialTextBox3.Text}%'";
            if (materialTextBox3.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }
    }
}
