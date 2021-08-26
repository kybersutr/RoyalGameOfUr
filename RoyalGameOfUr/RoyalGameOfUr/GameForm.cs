using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RoyalGameOfUr
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            DrawGame();
        }

        public void DrawGame() 
        {
            DrawTiles();
            DrawDice();
            DrawPlayers();
            WriteScores();
        }

        private void DrawTiles() { }
        private void DrawDice() { }
        private void DrawPlayers() { }
        private void WriteScores() { }
    }
}
