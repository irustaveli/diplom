﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager_cars
{
    public partial class Form7 : MaterialForm
    {
        public Form7()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }
        public string text_box1
        {
            get { return materialTextBox1.Text; }
            set { materialTextBox1.Text = value; }
        }
        public string text_box5
        {
            get { return textBox5.Text; }
            set { textBox5.Text = value; }
        }

        public string text_box2
        {
            get { return materialTextBox2.Text; }
            set { materialTextBox2.Text = value; }
        }
        public string text_box6
        {
            get { return textBox6.Text; }
            set { textBox6.Text = value; }
        }
        public string text_box3
        {
            get { return materialTextBox3.Text; }
            set { materialTextBox3.Text = value; }
        }

        public string text_box4
        {
            get { return materialTextBox4.Text; }
            set { materialTextBox4.Text = value; }
        }
        public string text_box7
        {
            get { return textBox7.Text; }
            set { textBox7.Text = value; }
        }
        public string rich_text_box1
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

        private Image _thePicture;
        public Image ThePicture
        {
            set { this._thePicture = value; }
            get { return this._thePicture; }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (this.Text == "Просмотр информации автомобиля")
            {
                materialButton2.Location = new Point(12, 660);
                materialButton2.Size = new Size(677, 64);
            }
            if (this.ThePicture != null)
            {
                pictureBox1.Image = this.ThePicture;
            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog res = new OpenFileDialog();

            //Filter
            res.Filter = "Image Files|*.jpg;*.jpeg;";

            //When the user select the file
            if (res.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = res.FileName;
                //Do something
                textBox8.Text = filePath;
                pictureBox1.Image = new Bitmap(filePath);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            if (this.Text == "Редактировать")
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                var result = db.UpdateData(materialTextBox1.Text, materialTextBox1.Text, textBox6.Text, materialTextBox2.Text, materialTextBox3.Text, textBox7.Text, richTextBox1.Text, materialTextBox4.Text, textBox8.Text);
                if (result == 1)
                {
                    MessageBox.Show("Данные успешно обновились", "Сообщение");
                }
                pictureBox1.Image = new Bitmap(textBox8.Text);
            }
            else if (this.Text == "Добавить")
            {
                var result = db.InsertData(materialTextBox1.Text, materialTextBox2.Text, materialTextBox3.Text, richTextBox1.Text, materialTextBox4.Text, textBox8.Text);
                if (result == 1)
                {
                    MessageBox.Show("Данные успешно добавлены", "Сообщение");
                }
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (this.Text == "Просмотр информации автомобиля")
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide(); // закрытие текущий формы
            }
            else
            {
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide(); // закрытие текущий формы
            }
        }
    }
}
