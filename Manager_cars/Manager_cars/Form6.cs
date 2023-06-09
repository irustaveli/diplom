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
    public partial class Form6 : MaterialForm
    {
        public Form6()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> User = new List<dynamic>();
            List<dynamic> UserGet = new List<dynamic>();
            User.Add(materialTextBox1.Text);
            User.Add(materialMaskedTextBox1.Text);
            UserGet = db.SelectLoginPassword(User);
            if (UserGet[0] == "success")
            {
                if (UserGet[1] == "manager")
                {
                    this.Hide(); // закрытие текущий формы
                    Form1 f = new Form1();
                    // Записать значение в label через созданное нами свойство LbL
                    f.LbL = materialTextBox1.Text;
                    // Считать значение в s типа string через созданное нами свойство LbL
                    string s = f.LbL;
                    f.ShowDialog();
                    Form1 examp = new Form1();
                    examp.Show();
                }
                else if (UserGet[1] == "admin")
                {
                    this.Hide(); // закрытие текущий формы
                    Form2 f = new Form2();
                    // Записать значение в label через созданное нами свойство LbL
                    f.ShowDialog();

                }

            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Error"); // Выводим сообщение об ошибке
            }
        }
    }
}
