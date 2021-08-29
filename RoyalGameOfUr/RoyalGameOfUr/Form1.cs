using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

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
            StartGame(true, -1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            int difficulty;

            if (radioButton1.Checked) 
            {
                difficulty = 0;
            }
            else if (radioButton2.Checked) 
            {
                difficulty = 1;
            }
            else if (radioButton3.Checked)
            {
                difficulty = 2;
            }
            else 
            {
                MessageBox.Show("Please choose a difficulty.");
                return;
            }

            StartGame(false, difficulty);
        }

        private void StartGame(bool multiplayer, int difficulty) 
        {
            Program.gameForm = new GameForm();
            Program.game = new Game(multiplayer, difficulty);
            Program.game.CreateBoard();
            this.Hide();
            Program.gameForm.CalculateTilePositions();
            Program.gameForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
