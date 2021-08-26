using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Game
    {
        private Player player1;
        private Player player2;
        private Board board;

        public Game(bool multiplayer, int difficulty) 
        {
            this.board = new Board(1,5);

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
    }
}
