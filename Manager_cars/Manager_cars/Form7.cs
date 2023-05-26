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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public string text_box1
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string text_box2
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string text_box3
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public string text_box4
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        
           public string rich_text_box1
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // закрытие текущий формы
        }
    }
}
