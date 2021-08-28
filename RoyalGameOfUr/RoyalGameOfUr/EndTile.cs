using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class EndTile : EmptyTile
    {
        public EndTile(IPlayer p) : base(p) { }
        public int count = 0;
    }
}
