using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class AIPlayer:Player
    {
        private int difficulty;

        public AIPlayer(int difficulty)
        {
            this.difficulty = difficulty;
        }

        public void ThrowDice() { }
        public void ChooseToken() { }
    }
}
