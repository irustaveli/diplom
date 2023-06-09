﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Manager_cars
{
    public partial class Form3 : MaterialForm
    {
        public Form3()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Users = new List<dynamic>();
            Users = db.SelectUsers();
            dataGridView1.Columns.Add("Column1", "ФИО Сотрудника");
            dataGridView1.Columns.Add("Column2", "Login");
            dataGridView1.Columns.Add("Column3", "Пароль");
            dataGridView1.Columns.Add("Column4", "Роль");
            foreach (var i in Users)
            {
                dataGridView1.Rows.Add(i.FIO, i.login, i.password, i.role);
            }
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 250;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
            f8.Text = "Добавить";
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            List<dynamic> sb = new List<dynamic>();
            for (int i = 1; i <= dataGridView1.ColumnCount; i++)
            {
                sb.Add(selectedRow.Cells[$@"Column{i}"].Value);
            }
            f.text_box1 = sb[0].ToString();
            f.text_box2 = sb[1].ToString();
            f.text_box3 = sb[2].ToString();
            f.text_box4 = sb[3].ToString();
            f.Text = "Редактировать";
            f.Show();
            this.Hide(); // закрытие текущий формы
        }
    }
}
