using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoyalGameOfUr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.multiplayer = false;
            this.Hide();
            Program.gameForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            {
                Program.difficulty = 0;
            }
            else if (radioButton2.Checked) 
            {
                Program.difficulty = 1;
            }
            else if (radioButton3.Checked)
            {
                Program.difficulty = 2;
            }
            else 
            {
                MessageBox.Show("Please choose a difficulty.");
                return;
            }

            Program.multiplayer = true;
            this.Hide();
            Program.gameForm.Show();
        }
    }
}
