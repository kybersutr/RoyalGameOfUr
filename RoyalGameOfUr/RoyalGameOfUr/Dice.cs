using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Dice
    {
        public bool dot = false;
        private Random random = new Random();

        public void Throw() 
        {
            if (random.NextDouble() >= 0.5)
            {
                this.dot = true;
            }
            else 
            {
                this.dot = false;
            }
        }
    }
}
