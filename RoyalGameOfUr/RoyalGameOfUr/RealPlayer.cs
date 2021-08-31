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
            // Makes the "Throw" button visible; game then waits for the user to press the button
            Program.gameForm.button2.Visible = true;
        }
        public override Token ChooseToken() {
            // Changes state to waitingForClick, 
            // then waits until player clicks on an available token which is saved to "chosenToken"
            // if "chosenToken" contains a token, it can be returned
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
