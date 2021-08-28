using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class AIPlayer:IPlayer
    {
        private int difficulty;

        public AIPlayer(int difficulty)
        {
            this.difficulty = difficulty;
        }

        public void ThrowDice() 
        {
            foreach (Dice dice in Program.game.board.dice) 
            {
                dice.Throw();
            }
        }
        public Token ChooseToken() 
        {
            return new Token();
        }
    }
}
