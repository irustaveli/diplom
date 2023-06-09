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
    public partial class Form5 : MaterialForm
    {
        public Form5()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal800, Primary.Teal900, Primary.Indigo500, Accent.Lime400, TextShade.WHITE);
        }

        public string LbL
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        DataTable dtData = new DataTable();
        private void Form5_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Cars = new List<dynamic>();
            Cars = db.SelectTestDrive();
            dataGridView1.Columns.Add("Column1", "Клиент");
            dtData.Columns.Add("Column1", typeof(string));
            dataGridView1.Columns.Add("Column2", "Сотрудник");
            dtData.Columns.Add("Column2", typeof(string));
            dataGridView1.Columns.Add("Column3", "Модель");
            dtData.Columns.Add("Column3", typeof(string));
            dataGridView1.Columns.Add("Column4", "Телефон");
            dtData.Columns.Add("Column4", typeof(string));
            dataGridView1.Columns.Add("Column5", "Статус");
            dtData.Columns.Add("Column5", typeof(string));
            foreach (var i in Cars)
            {
                dataGridView1.Rows.Add(i.FIO, i.employee, i.car_model, i.telephone, i.status);
                dtData.Rows.Add(i.FIO, i.employee, i.car_model, i.telephone, i.status);
            }
            // Set your desired AutoSize Mode:
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView1.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView1.Columns[i].Width = colw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();


            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                var employee = label1.Text;
                string cellValue = Convert.ToString(selectedRow.Cells["Column1"].Value);
                db.InsertTestDriverStatus("В работе", cellValue, employee);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();


            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                var employee = label1.Text;
                string cellValue = Convert.ToString(selectedRow.Cells["Column1"].Value);
                db.InsertTestDriverStatus("В работе", cellValue, employee);
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                var employee = label1.Text;
                string cellValue = Convert.ToString(selectedRow.Cells["Column1"].Value);
                db.InsertTestDriverStatus("Завершено", cellValue, employee);
            }    
            
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView2.DataSource = dtData;
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Клиент";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Сотрудник";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Телефон";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Статус";
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Клиент LIKE '%{materialTextBox1.Text}%'";
            // Set your desired AutoSize Mode:
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i <= dataGridView2.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView2.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView2.Columns[i].Width = colw;
            }
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
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Клиент";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Сотрудник";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Телефон";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Статус";
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Сотрудник LIKE '%{materialTextBox2.Text}%'";
            // Set your desired AutoSize Mode:
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i <= dataGridView2.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView2.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView2.Columns[i].Width = colw;
            }
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
            (dataGridView2.DataSource as DataTable).Columns[0].ColumnName = "Клиент";
            (dataGridView2.DataSource as DataTable).Columns[1].ColumnName = "Сотрудник";
            (dataGridView2.DataSource as DataTable).Columns[2].ColumnName = "Модель";
            (dataGridView2.DataSource as DataTable).Columns[3].ColumnName = "Телефон";
            (dataGridView2.DataSource as DataTable).Columns[4].ColumnName = "Статус";
            // Set your desired AutoSize Mode:
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i <= dataGridView2.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView2.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView2.Columns[i].Width = colw;
            }
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Статус LIKE '%{materialTextBox3.Text}%'";
            
            if (materialTextBox3.Text == "")
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
            }
        }
    }
}
