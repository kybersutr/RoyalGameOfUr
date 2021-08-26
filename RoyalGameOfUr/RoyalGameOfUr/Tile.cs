using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Tile
    {
        public int type;
        // 0: placeholder empty tile
        // 1: normal tile
        // 2: rosette tile\

        public Tile(int type)
        {
            this.type = type;
        }
    }
}
