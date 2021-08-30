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
                if (Program.game.CanMove(token))
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
                if (!Program.game.CanMove(token)) 
                {
                    continue;
                }
                int newTile = Program.game.GetNewTile(token);
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
            return null;
        }
    }
}
