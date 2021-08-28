﻿using System;
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
            this.ResizeEnd += Resized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu();
        }

        private void Menu() 
        {
            timer1.Stop();
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
                Rectangle fillRect = new Rectangle(x + 1, y + 1, size - 1, size - 1);
                if (!(tile is StartTile | tile is EndTile))
                {
                    using (Brush brush = new SolidBrush(Color.Bisque))
                    {
                        g.FillRectangle(brush, fillRect);
                    }
                    using (Pen pen = new Pen(Color.Black))
                    {
                        g.DrawRectangle(pen, rect);
                    }
                    Image rosette = Image.FromFile("rosette.png");

                    if (tile is RosetteTile)
                    {
                        g.DrawImage(rosette, fillRect);
                    }
                }
                if (tile is EndTile) 
                {
                    string text1 = ((EndTile)tile).count.ToString();
                    using (Font font1 = new Font("Arial", size/2, FontStyle.Bold, GraphicsUnit.Point))
                    {
                        using (Brush brush = new SolidBrush(Control.DefaultBackColor))
                        {
                            g.FillRectangle(brush, fillRect);
                        }
                        using (Brush brush = new SolidBrush(Color.DarkGray)) 
                        {
                            StringFormat format = new StringFormat();
                            format.Alignment = StringAlignment.Center;
                            g.DrawString(text1, font1, brush, fillRect, format);
                        }
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

        private void DrawToken(Token token) 
        {
            if (Program.game.board.tiles[token.tile] is StartTile)
            {
                token.image.BackColor = DefaultBackColor;
            }
            else 
            {
                token.image.BackColor = Color.Bisque;
            }
            int size = ClientSize.Width / 12;
            token.image.Size = new Size((size) - 2, (size) - 2);
            token.image.Location = new Point(
                ClientSize.Width - 3 * size - (token.tile % 8) * size + 1,
                ((token.tile / 8) + 1) * size + 1
                );
        }
        private void DrawTokens() 
        {
            foreach (Token token in Program.game.player1.tokens) 
            {
                DrawToken(token);
            }
            foreach (Token token in Program.game.player2.tokens)
            {
                DrawToken(token);
            }
        }
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

        private void Win(Player winner) 
        {
            timer1.Stop();
            MessageBox.Show(winner + " is the winner!");
            Menu();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player winner = Program.game.CheckWinner();
            if (!(winner is null))
            {
                Win(winner);
            }

            switch (Program.game.phase)
            {
                case 0:
                    DrawGame();
                    Program.game.WhoIsPlaying().ThrowDice();
                    Program.game.phase += 1;
                    break;
                case 1:
                    // Waiting for player to throw dice
                    break;
                case 2:
                    DrawDice();
                    Program.game.phase += 1;
                    break;
                case 3:
                    //let player choose token
                    Token token = Program.game.WhoIsPlaying().ChooseToken();
                    if (!(token is null)) 
                    {
                        Program.game.phase += 1;
                    }
                    break;

                case 4:
                    DrawTiles();
                    DrawTokens();
                    Program.game.phase = 0;
                    break;
            }

        }

        private void Resized(object sender, EventArgs e)
        {
            DrawGame();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("7 clicked.");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("6 clicked.");
        }
    }
}
