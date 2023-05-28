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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public string LbL
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Cars = new List<dynamic>();
            Cars = db.SelectTestDrive();
            dataGridView1.Columns.Add("Column1", "ФИО Клиента");
            dataGridView1.Columns.Add("Column2", "ФИО Сотрудника");
            dataGridView1.Columns.Add("Column3", "Модель");
            dataGridView1.Columns.Add("Column4", "Телефон");
            dataGridView1.Columns.Add("Column5", "Статус");
            foreach (var i in Cars)
            {
                dataGridView1.Rows.Add(i.FIO, i.employee, i.car_model, i.telephone, i.status);
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
    }
}
