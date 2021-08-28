﻿using System;
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
        public int phase = 0;

        public Game(bool multiplayer, int difficulty)
        {

            if (multiplayer)
            {
                this.player1 = new RealPlayer("White", new List<Token>() 
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
                this.player2 = new RealPlayer("Black", new List<Token>()
                {
                    new Token(19, Program.gameForm.black0),
                    new Token(19, Program.gameForm.black1),
                    new Token(19, Program.gameForm.black2),
                    new Token(19, Program.gameForm.black3),
                    new Token(19, Program.gameForm.black4),
                    new Token(19, Program.gameForm.black5),
                    new Token(19, Program.gameForm.black6)
                }
                );
            }
            else
            {
                this.player1 = new RealPlayer("White", new List<Token>()
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
                this.player2 = new AIPlayer("Black", difficulty, new List<Token>()
                {
                    new Token(19, Program.gameForm.black0),
                    new Token(19, Program.gameForm.black1),
                    new Token(19, Program.gameForm.black2),
                    new Token(19, Program.gameForm.black3),
                    new Token(19, Program.gameForm.black4),
                    new Token(19, Program.gameForm.black5),
                    new Token(19, Program.gameForm.black6)
                }
                );
            }

        }

        public bool CanPlay() 
        {
            Player player = WhoIsPlaying();
            bool canPlay = false;
            foreach (Token token in player.tokens) 
            {
                if (CanMove(token)) 
                {
                    canPlay = true;
                    break;
                }
            }
            return canPlay;
        }
        public bool CanMove(Token token) 
        {
            int count = Count();
            if (player1.tokens.Contains(token))
            {
                if (token.tile == 15 | token.tile < 3)
                {
                    if ((token.tile + count) % 16 > 2)
                    {
                        return false;
                    }
                }
                else if (board.tiles[(token.tile + count) % 16].occupiedBy == player1)
                {
                    return false;
                }
                else if ((board.tiles[(token.tile + count) % 16].occupiedBy == player2) & (board.tiles[(token.tile + count) % 15] is RosetteTile)) 
                {
                    return false;
                }
            }

            else 
            {
                if ((token.tile < 18) & (token.tile + count > 18)) 
                {
                    return false;
                }
                int move_to;
                if (token.tile + count < 24)
                {
                    move_to = token.tile + count;
                }
                else 
                {
                    move_to = (token.tile + count) % 24 + 8;
                }
                if (board.tiles[move_to].occupiedBy == player2)
                {
                    return false;
                }
                if ((board.tiles[move_to].occupiedBy == player1) & (board.tiles[move_to] is RosetteTile))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Move(Token token) 
        {
            // True if player plays again (lands on rosette)
            int count = Program.game.Count();

            board.tiles[token.tile].occupiedBy = null;

            if (player1.tokens.Contains(token))
            {
                token.tile = (token.tile + count) % 16;
                
                if (board.tiles[token.tile] is EndTile)
                {
                    ((EndTile)board.tiles[token.tile]).count += 1;
                    token.image.Visible = false;
                }
                else 
                {
                    if (board.tiles[token.tile].occupiedBy == player2) 
                    {
                        foreach (Token otherToken in player2.tokens) 
                        {
                            if (otherToken.tile == token.tile) 
                            {
                                otherToken.tile = 19;
                            }
                        }
                    }
                    board.tiles[token.tile].occupiedBy = player1;
                }
            }
            else 
            {
                if (token.tile + count <= 23)
                {
                    token.tile += count;
                }
                else 
                {
                    token.tile = ((token.tile + count) % 24) + 8; 
                }
                if (board.tiles[token.tile] is EndTile)
                {
                    ((EndTile)board.tiles[token.tile]).count += 1;
                    token.image.Visible = false;
                }
                else 
                {
                    if (board.tiles[token.tile].occupiedBy == player1)
                    {
                        foreach (Token otherToken in player1.tokens)
                        {
                            if (otherToken.tile == token.tile)
                            {
                                otherToken.tile = 3;
                            }
                        }
                    }
                    board.tiles[token.tile].occupiedBy = player2;
                }
            }
            if (board.tiles[token.tile] is RosetteTile)
            {
                return true;
            }
            else 
            {
                return false;
            }

        }
        public int Count() 
        {
            int count = 0;
            foreach (Dice dice in board.dice) 
            {
                if (dice.dot) 
                {
                    count += 1;
                }
            }
            return count;
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
