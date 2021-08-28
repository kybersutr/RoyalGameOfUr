using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class RealPlayer : Player
    {
        public bool waitingForClick = false;
        public Token chosenToken;
        public RealPlayer(string name, List<Token> tokens)
        {
            this.tokens = tokens;
            this.name = name;
        }

        public override void ThrowDice()
        {
            Program.gameForm.button2.Visible = true;

        }
        public override Token ChooseToken() {
            if (!waitingForClick)
            {
                waitingForClick = true;
                return null;
            }
            else 
            {
                if (!(chosenToken is null))
                {
                    Token toReturn = chosenToken;
                    chosenToken = null;
                    waitingForClick = false;
                    return toReturn;
                }
                else 
                {
                    return null;
                }
            }
        }
    }
}
