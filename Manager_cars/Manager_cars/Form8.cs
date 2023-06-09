﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Manager_cars
{
    public partial class Form8 : MaterialForm
    {
        public string text_box1
        {
            get { return materialTextBox1.Text; }
            set { materialTextBox1.Text = value; }
        }
        public string text_box2
        {
            get { return materialTextBox2.Text; }
            set { materialTextBox2.Text = value; }
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
        public Form8()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            var result = db.UpdateUsers(materialTextBox1.Text, materialTextBox2.Text, materialTextBox3.Text, materialTextBox4.Text);
            if (result == 1)
            {
                MessageBox.Show("Данные успешно обновились", "Сообщение");
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (this.Text == "Редактировать")
            {
                Form3 f = new Form3();
                f.Show();
            }
            else if (this.Text == "Добавить")
            {
                SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
                var result = db.InsertUsers(materialTextBox1.Text, materialTextBox2.Text, materialTextBox3.Text, materialTextBox4.Text);
                if (result == 1)
                {
                    MessageBox.Show("Данные успешно добавлены", "Сообщение");
                }
            }
            this.Hide(); // закрытие текущий формы
        }
    }
}
