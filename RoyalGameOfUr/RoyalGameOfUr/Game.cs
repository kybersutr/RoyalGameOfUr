using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Game
    {
        public Player player1;
        public Player player2;
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
                this.player1 = new RealPlayer(new List<Token>() 
                {
                    new Token(3, Program.gameForm.white0),
                    new Token(3, Program.gameForm.white1), 
                    new Token(3, Program.gameForm.white2),
                    new Token(3, Program.gameForm.white3),
                    new Token(3, Program.gameForm.white4),
                    new Token(3, Program.gameForm.white5),
                    new Token(3, Program.gameForm.white6)
                }
                );
                this.player2 = new RealPlayer(new List<Token>()
                {
                    new Token(3, Program.gameForm.black0),
                    new Token(3, Program.gameForm.black1),
                    new Token(3, Program.gameForm.black2),
                    new Token(3, Program.gameForm.black3),
                    new Token(3, Program.gameForm.black4),
                    new Token(3, Program.gameForm.black5),
                    new Token(3, Program.gameForm.black6)
                }
                );
            }
            else
            {
                this.player1 = new RealPlayer(new List<Token>()
                {
                    new Token(3, Program.gameForm.white0),
                    new Token(3, Program.gameForm.white1),
                    new Token(3, Program.gameForm.white2),
                    new Token(3, Program.gameForm.white3),
                    new Token(3, Program.gameForm.white4),
                    new Token(3, Program.gameForm.white5),
                    new Token(3, Program.gameForm.white6)
                }
                );
                this.player2 = new AIPlayer(difficulty, new List<Token>()
                {
                    new Token(3, Program.gameForm.black0),
                    new Token(3, Program.gameForm.black1),
                    new Token(3, Program.gameForm.black2),
                    new Token(3, Program.gameForm.black3),
                    new Token(3, Program.gameForm.black4),
                    new Token(3, Program.gameForm.black5),
                    new Token(3, Program.gameForm.black6)
                }
                );
            }

        }

        public Player WhoIsPlaying() 
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

        public Player CheckWinner() 
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
