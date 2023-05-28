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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var UserGet = false;
            SQLlite_setting.SQLlite db = new SQLlite_setting.SQLlite();
            List<dynamic> User = new List<dynamic>();
            User.Add(textBox1.Text);
            User.Add(textBox2.Text);
            UserGet = db.SelectLoginPassword(User);
            if (UserGet == true ) 
            {
                Form1 f = new Form1();
                // Записать значение в label через созданное нами свойство LbL
                f.LbL = textBox1.Text;
                // Считать значение в s типа string через созданное нами свойство LbL
                string s = f.LbL;
                f.ShowDialog();
                Form1 examp = new Form1();
                examp.Show();
                this.Hide(); // закрытие текущий формы

            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Error"); // Выводим сообщение об ошибке
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }
    }
}
