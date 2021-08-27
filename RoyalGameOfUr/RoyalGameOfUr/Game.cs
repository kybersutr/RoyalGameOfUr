using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Game
    {
        public IPlayer player1;
        public IPlayer player2;
        public Board board;

        public Game(bool multiplayer, int difficulty) 
        {

            if (multiplayer)
            {
                this.player1 = new RealPlayer();
                this.player2 = new RealPlayer();
            }
            else 
            {
                this.player1 = new RealPlayer();
                this.player2 = new AIPlayer(difficulty);
            }

        }

        public void CreateBoard() 
        {
            this.board = new Board();
        }

        public void Loop() 
        {
            Program.gameForm.DrawGame();
            player1.ThrowDice();
            Program.gameForm.DrawGame();

        }
    }
}
