using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class RealPlayer:IPlayer
    {
        public void ThrowDice() 
        {
            while (true) 
            {
                //wait until player clicks the dice
                break;
            }
            foreach (Dice dice in Program.game.board.dice) 
            {
                dice.Throw();
            }
        }
        public void ChooseToken() { }
    }
}
