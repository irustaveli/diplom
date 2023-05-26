using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager_cars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> Cars = new List<dynamic>();
            Cars = db.SelectCars();
            dataGridView1.Columns.Add("Column1", "id_car_name");
            dataGridView1.Columns.Add("Column2", "Наименования автомобиля");
            dataGridView1.Columns.Add("Column3", "id_models");
            dataGridView1.Columns.Add("Column4", "Модель");
            dataGridView1.Columns.Add("Column5", "Количество");
            dataGridView1.Columns.Add("Column6", "id_car_info");
            dataGridView1.Columns.Add("Column7", "Информация по модели");
            dataGridView1.Columns.Add("Column8", "Цена");
            dataGridView1.Columns["Column1"].Visible = false;
            dataGridView1.Columns["Column2"].Visible = false;
            dataGridView1.Columns["Column3"].Visible = false;
            dataGridView1.Columns["Column6"].Visible = false;
            foreach (var i in Cars)
            {
                dataGridView1.Rows.Add(i.id_car_name, i.car_name, i.id_modelse, i.models, i.quantity, i.id_info_models, i.info, i.price);
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
            string count_booking = db.countBooking();
            string countTestDrive = db.countTestDrive();
            label1.Text = count_booking.ToString();
            label2.Text = countTestDrive.ToString();
            List<dynamic> User = new List<dynamic>();
            User = db.SelectUser(label10.Text);
            label7.Text = User[0].FIO;
            label9.Text = User[0].info;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            // Записать значение в label через созданное нами свойство LbL
            f.LbL = label7.Text;
            // Считать значение в s типа string через созданное нами свойство LbL
            string s = f.LbL;
            f.Show();
        }
    }
}
