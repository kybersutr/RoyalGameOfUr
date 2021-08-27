using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class EmptyTile:Tile
    {
        public IPlayer belongsTo;

        public EmptyTile(IPlayer belongsTo)
        {
            this.belongsTo = belongsTo;
        }
    }
}
