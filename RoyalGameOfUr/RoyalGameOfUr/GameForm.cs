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
            this.ResizeEnd += Resized;
        }

        public void CalculateTilePositions()
        {
            for (int i = 0; i < Program.game.board.tiles.Count; ++i)
            {
                Program.game.board.tiles[i].CalculatePosition(ClientSize.Width, i);
            }
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
            DrawTokens();
            DrawTiles();
            DrawDice();
            WriteTurn();
        }

        private void DrawTiles() 
        {
            for (int i = 0; i < Program.game.board.tiles.Count; ++i) 
            {
                int size = ClientSize.Width / 12;
                DrawTile(Program.game.board.tiles[i]);
            }
        
        }

        private void FillCircle(Graphics g, Brush b, int x, int y, int radius) 
        {
            g.FillEllipse(b, new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius));
        }
        private void DrawTile(Tile tile) 
        {
            using (Graphics g = this.CreateGraphics()) 
            {
                
                if (!(tile is StartTile | tile is EndTile))
                {
                    using (Brush brush = new SolidBrush(Color.Bisque))
                    {
                        // inside color
                        g.FillRectangle(brush, tile.fillRect);
                    }
                    using (Pen pen = new Pen(Color.Black))
                    {
                        // edges
                        g.DrawRectangle(pen, tile.rect);
                    }

                    Image rosette = Image.FromFile("rosette2.png");
                    if (tile is RosetteTile)
                    {
                        g.DrawImage(rosette, tile.fillRect);
                    }
                }
                
                if (tile is EndTile) 
                {
                    string text1 = ((EndTile)tile).count.ToString();
                    using (Font font1 = new Font("Arial", tile.fillRect.Width/2, FontStyle.Bold, GraphicsUnit.Point))
                    {
                        using (Brush brush = new SolidBrush(Control.DefaultBackColor))
                        {
                            g.FillRectangle(brush, tile.fillRect);
                        }
                        using (Brush brush = new SolidBrush(Color.DarkGray)) 
                        {
                            StringFormat format = new StringFormat();
                            format.Alignment = StringAlignment.Center;
                            g.DrawString(text1, font1, brush, tile.fillRect, format);
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
                
                using (Pen pen = new Pen(Color.Gray)) 
                {
                    g.DrawLine(pen, new Point(mid, top + (2 * (bot - top) / 3)), triangle[0]);
                    g.DrawLine(pen, new Point(mid, top + (2 * (bot - top) / 3)), triangle[1]);
                    g.DrawLine(pen, new Point(mid, top + (2 * (bot - top) / 3)), triangle[2]);
                }
                
                if (dice.dot)
                {
                    int radius = size / 11;
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        FillCircle(g, brush, mid, top + 2 * (bot - top) / 3, radius);
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
            Tile tile = Program.game.board.tiles[token.tile];
            token.image.Size = tile.fillRect.Size;
            token.image.Location = tile.fillRect.Location;
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
        private void WriteTurn() 
        {
            label2.Text = "It's " + Program.game.WhoIsPlaying().name + "'s turn.";
        }

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
            MessageBox.Show(winner.name + " is the winner!");
            Menu();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player winner = Program.game.CheckWinner();
            if (!(winner is null))
            {
                DrawGame();
                Win(winner);
            }
            
            switch (Program.game.phase)
            {
                case 0:
                    // Let player throw dice
                    DrawGame();
                    Program.game.Phase0();
                    break;
                case 1:
                    // Waiting for player to throw dice
                    break;
                case 2:
                    for (int i = 0; i < 50; ++i) 
                    {
                        // Dice throw animation
                        foreach (Dice dice in Program.game.board.dice) 
                        {
                            dice.Throw();
                        }
                        DrawDice();
                    }
                    // Check if throw wasn't zero
                    Program.game.Phase2();
                    break;
                case 3:
                    //let player choose token
                    Program.game.Phase3(Program.game.DiceCount());
                    break;

                case 4:
                    DrawTokens();
                    DrawTiles();
                    // Return to phase 0
                    Program.game.Phase4();
                    break;
            }

        }

        private void Resized(object sender, EventArgs e)
        {
            CalculateTilePositions();
            DrawGame();
        }

        private void TokenClicked(Token token, Player player) 
        {
            // If player is waitingForClick, pass the token as the chosenToken
            if (player is RealPlayer) 
            {
                if (((RealPlayer)player).waitingForClick) 
                {
                    ((RealPlayer)player).chosenToken = token;
                }
            }
        }

        private void black0_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[0], Program.game.player2);
        }
        private void black1_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[1], Program.game.player2);
        }

        private void black2_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[2], Program.game.player2);
        }

        private void black3_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[3], Program.game.player2);
        }

        private void black4_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[4], Program.game.player2);
        }

        private void black5_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[5], Program.game.player2);
        }

        private void black6_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player2.tokens[6], Program.game.player2);
        }

        private void white0_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[0], Program.game.player1);
        }

        private void white6_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[6], Program.game.player1);
        }

        private void white5_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[5], Program.game.player1);
        }

        private void white1_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[1], Program.game.player1);
        }

        private void white2_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[2], Program.game.player1);
        }

        private void white3_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[3], Program.game.player1);
        }

        private void white4_Click(object sender, EventArgs e)
        {
            TokenClicked(Program.game.player1.tokens[4], Program.game.player1);
        }


    }
}
