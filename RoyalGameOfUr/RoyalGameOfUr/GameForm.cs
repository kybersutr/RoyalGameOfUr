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
            DrawGame();
            MessageBox.Show("cus");
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

        private void DrawTiles() 
        {
            for (int i = 0; i < Program.game.board.tiles.Count; ++i) 
            {
                int size = ClientSize.Width / 12;
                DrawTile(Program.game.board.tiles[i], i, size);
            }
        
        }

        private void DrawTile(Tile tile, int i, int size) 
        {
            using (Graphics g = this.CreateGraphics()) 
            {
                int x = ClientSize.Width - 3 * size - (i % 8) * size;
                int y = ((i / 8) + 1) * size;
                Rectangle rect = new Rectangle(x, y, size, size);
                if (tile.type != 0)
                {
                    using (Brush brush = new SolidBrush(Color.Bisque))
                    {
                        g.FillRectangle(brush, rect);
                    }
                    using (Pen pen = new Pen(Color.Black))
                    {
                        g.DrawRectangle(pen, rect);
                    }
                    Image rosette = Image.FromFile("rosette.png");

                    if (tile.type == 2)
                    {
                        g.DrawImage(rosette, rect);
                    }
                }
            }
        }
        private void DrawDice() { }
        private void DrawPlayers() { }
        private void WriteScores() { }
    }
}
