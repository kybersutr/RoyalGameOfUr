using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class RealPlayer : IPlayer
    {
        public void ThrowDice()
        {
            Program.gameForm.button2.Visible = true;

        }
        public Token ChooseToken() {
            return new Token();
        }
    }
}
