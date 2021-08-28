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
            Menu();
        }

        private void Menu() 
        {
            this.Hide();
            Program.menu.Show();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DrawGame();
        }

        public void DrawGame() 
        {
            DrawTiles();
            DrawDice();
            DrawTokens();
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

        private void FillCircle(Graphics g, Brush b, int x, int y, int radius) 
        {
            g.FillEllipse(b, new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius));
        }
        private void DrawTile(Tile tile, int i, int size) 
        {
            using (Graphics g = this.CreateGraphics()) 
            {
                int x = ClientSize.Width - 3 * size - (i % 8) * size;
                int y = ((i / 8) + 1) * size;
                Rectangle rect = new Rectangle(x, y, size, size);
                if (!(tile is StartTile | tile is EndTile))
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

                    if (tile is RosetteTile)
                    {
                        g.DrawImage(rosette, rect);
                    }
                }
            }
        }

        private void DrawSingleDice(Dice dice, int i, int size) 
        {
            using (Graphics g = this.CreateGraphics())
            {
                int top = (9 * size)/2;
                int bot = top + (int)((Math.Sqrt(3) / 2) * size);
                int left = ClientSize.Width / 2 - 2 * size + i * size;
                int right = ClientSize.Width / 2 - size + i * size;
                int mid = (left + right) / 2;
                Point[] triangle =
                {
                    new Point(mid, top),
                    new Point(left, bot),
                    new Point(right, bot),
                };
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    g.FillPolygon(brush, triangle);
                }
                if (dice.dot)
                {
                    int radius = size / 8;
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        FillCircle(g, brush, mid, top + 2*(bot - top)/3, radius);
                    }
                }
            }
        }
        private void DrawDice() 
        {
            for (int i = 0; i < Program.game.board.dice.Count; ++i) 
            {
                int size = ClientSize.Width / 12;
                DrawSingleDice(Program.game.board.dice[i], i, size);
            }
            
        }

        private void DrawToken() { }
        private void DrawTokens() { }
        private void WriteScores() { }

        private void button2_Click(object sender, EventArgs e)
        {
            // Throw dice button
            foreach (Dice dice in Program.game.board.dice)
            {
                dice.Throw();
            }
            button2.Visible = false;
            Program.game.phase += 1;
        }

        private void Win(IPlayer winner) 
        {
            timer1.Stop();
            MessageBox.Show(winner + " is the winner!");
            Menu();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IPlayer winner = Program.game.CheckWinner();
            if (!(winner is null))
            {
                Win(winner);
            }


            if (Program.game.phase == 0)
            {
                DrawGame();
                Program.game.WhoIsPlaying().ThrowDice();
                Program.game.phase += 1;
            }
            else if (Program.game.phase == 2) 
            {
                DrawGame();
            }

        }
    }
}
