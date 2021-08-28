using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class RealPlayer : Player
    {

        public RealPlayer(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public override void ThrowDice()
        {
            Program.gameForm.button2.Visible = true;

        }
        public override Token ChooseToken() {
            return tokens[0];
        }
    }
}
