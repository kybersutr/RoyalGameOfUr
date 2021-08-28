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
        }
        public override Token ChooseToken() 
        {
            return tokens[0];
        }
    }
}
