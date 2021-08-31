using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class AIPlayer:Player
    {
        private int difficulty;

        public AIPlayer(string name, int difficulty, List<Token> tokens)
        {
            this.difficulty = difficulty;
            this.tokens = tokens;
            this.name = name;
        }

        public override void ThrowDice() 
        {
            foreach (Dice dice in Program.game.board.dice) 
            {
                dice.Throw();
            }
            Program.game.phase += 1;
        }
        public override Token ChooseToken() 
        {
            if (difficulty == 0)
            {
                return Easy();
            }
            else if (difficulty == 1)
            {
                return Medium();
            }
            else 
            {
                return Hard();
            }
        }

        private Token Easy() 
        {
            // Returns the first possible token
            foreach (Token token in tokens)
            {
                if (Program.game.CanMove(token, Program.game.DiceCount()))
                {
                    return token;
                }
            }
            return null;
        }

        private Token Medium() 
        {
            // Try to move each token and see where it landed. Choose the token with the best result.
            // Best results: lands on a rosette, gets off the board, overthrows other player
            foreach (Token token in tokens) 
            {
                if (!Program.game.CanMove(token, Program.game.DiceCount())) 
                {
                    continue;
                }
                int newTile = Program.game.GetNewTile(token, Program.game.DiceCount());
                if (Program.game.board.tiles[newTile] is RosetteTile)
                {
                    return token;
                }
                else if (Program.game.board.tiles[newTile] is EndTile) 
                {
                    return token;
                }
                if ((Program.game.board.tiles[newTile].occupiedBy != this)
                    & !(Program.game.board.tiles[newTile].occupiedBy is null)) 
                {
                    return token;
                }

            }
            return Easy();
        }

        private Token Hard() 
        {
            // Uses Expectiminimax and rates situation depth+1 moves in the future
            int depth = 2;
            (Token bestToken, _) = Expectiminimax(depth, this, Program.game.DiceCount());
            return bestToken;
        }

        private (Token, double) Expectiminimax(int depth, Player player, int diceCount) 
        {
            Token bestToken = Medium();
            double bestValue;
            Player opponent;

            if (player == Program.game.player1)
            {
                opponent = Program.game.player2;
                bestValue = double.NegativeInfinity;
            }
            else 
            {
                opponent = Program.game.player1;
                bestValue = double.PositiveInfinity;
            }

            if (!(Program.game.CanPlay(player, diceCount))) 
            {
                //shouldn't happen, CanPlay is checked before
                return (null, Program.game.RatePosition());
            }

            if (depth == 0)
            {
                // Tries to play every token and rates the new position with RatePosition() Game method
                foreach (Token token in player.tokens) 
                {
                    if (!(Program.game.CanMove(token, diceCount))) 
                    {
                        continue;
                    }
                    (int[] previousP1, int[] previousP2) = Program.game.ReversibleMove(token, diceCount);
                    double currentValue = Program.game.RatePosition();
                    Program.game.Reverse(previousP1, previousP2);

                    // Chooses minimal/maximal value
                    if (player == Program.game.player1)
                    {
                        if (currentValue > bestValue)
                        {
                            bestValue = currentValue;
                            bestToken = token;
                        }
                    }
                    else 
                    {
                        if (currentValue < bestValue) 
                        {
                            bestValue = currentValue;
                            bestToken = token;
                        }
                    }
                }
            }

            else 
            {
                double currentValue = 0;

                // make a move then run Expectiminimax on each possible dice throw and take the weighted average of the results
                foreach (Token token in player.tokens) 
                {
                    if (!(Program.game.CanMove(token, diceCount))) 
                    {
                        continue;
                    }
                    (int[] previousP1, int[]previousP2) = Program.game.ReversibleMove(token, diceCount);
                    if (Program.game.board.tiles[token.tile] is RosetteTile)
                    {
                        (_, double zeroThrow) = Expectiminimax(depth - 1, player, 0);
                        (_, double oneThrow) = Expectiminimax(depth - 1, player, 1);
                        (_, double twoThrow) = Expectiminimax(depth - 1, player, 2);
                        (_, double threeThrow) = Expectiminimax(depth - 1, player, 3);
                        (_, double fourThrow) = Expectiminimax(depth - 1, player, 4);
                        currentValue += 
                              (1 / 16.0) * zeroThrow 
                            + (4 / 16.0) * oneThrow 
                            + (6 / 16.0) * twoThrow 
                            + (4 / 16.0) * threeThrow 
                            + (1 / 16.0) * fourThrow;
                    }
                    else 
                    {
                        (_, double zeroThrow) = Expectiminimax(depth - 1, opponent, 0);
                        (_, double oneThrow) = Expectiminimax(depth - 1, opponent, 1);
                        (_, double twoThrow) = Expectiminimax(depth - 1, opponent, 2);
                        (_, double threeThrow) = Expectiminimax(depth - 1, opponent, 3);
                        (_, double fourThrow) = Expectiminimax(depth - 1, opponent, 4);
                        currentValue +=
                              (1 / 16.0) * zeroThrow
                            + (4 / 16.0) * oneThrow
                            + (6 / 16.0) * twoThrow
                            + (4 / 16.0) * threeThrow
                            + (1 / 16.0) * fourThrow;
                    }
                    Program.game.Reverse(previousP1, previousP2);

                    // save minimal/maximal value
                    if (player == Program.game.player1)
                    {
                        if (currentValue > bestValue)
                        {
                            bestValue = currentValue;
                            bestToken = token;
                        }
                    }
                    else 
                    {
                        if (currentValue < bestValue) 
                        {
                            bestValue = currentValue;
                            bestToken = token;
                        }
                    }
                }
            }
            return (bestToken, bestValue);

        }
    }
}
