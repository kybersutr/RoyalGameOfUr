using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class EmptyTile:Tile
    {
        public Player belongsTo;

        public EmptyTile(Player belongsTo)
        {
            this.belongsTo = belongsTo;
        }
    }
}
