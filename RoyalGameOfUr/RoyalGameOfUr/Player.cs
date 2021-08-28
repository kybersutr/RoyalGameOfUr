using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    abstract class Player
    {
        public List<Token> tokens;
        abstract public void ThrowDice();
        abstract public Token ChooseToken();
    }
}
