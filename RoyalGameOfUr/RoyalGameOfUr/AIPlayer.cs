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
                foreach (Token token in tokens)
                {
                    if (Program.game.CanMove(token))
                    {
                        return token;
                    }
                }
                return null;
            }
            else 
            {
                return tokens[0];
            }
        }
    }
}
