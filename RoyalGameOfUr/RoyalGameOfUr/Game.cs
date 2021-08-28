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
        private int winning_count = 7;
        public bool turn = true; // true = player1's turn, false = player2's turn
        
        // game phases: 
        // 0 = draw board, let player throw dice, 
        // 1 = waiting for player to throw dice,
        // 2 = player threw dice, redraw board
        // 3 = letting player choose token
        // 4 = moving token, deciding who plays next
        public int phase;

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

        public IPlayer WhoIsPlaying() 
        {
            if (turn)
            {
                return player1;
            }
            else 
            {
                return player2;
            }
        }

        public void CreateBoard()
        {
            this.board = new Board();
        }

        public IPlayer CheckWinner() 
        {
            foreach (Tile tile in board.tiles) 
            {
                if (tile is EndTile) 
                {
                    if (((EndTile)tile).count == winning_count) 
                    {
                        return ((EndTile)tile).belongsTo;
                    }
                }
            }
            return null;
        }
    }
}
