using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RoyalGameOfUr
{
    class Tile
    {
        public Player occupiedBy;
        public Rectangle rect;
        public Rectangle fillRect;

        public void CalculatePosition(int width, int i) 
        {
            int row = i / 8;
            int x;
            int size = width / 12;
            if (row == 1)
            {
                x = width - 3 * size - (7 - (i % 8)) * size;
            }
            else
            {
                x = width - 3 * size - (i % 8) * size;
            }
            int y = ((i / 8) + 1) * size;
            rect = new Rectangle(x, y, size, size);
            fillRect = new Rectangle(x + 1, y + 1, size - 1, size - 1);
        }
    }
}
