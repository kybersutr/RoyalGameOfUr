using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RoyalGameOfUr
{
    class Token
    {
        public int tile;
        public PictureBox image;

        public Token(int tile, PictureBox image)
        {
            this.tile = tile;
            this.image = image;
        }
    }
}
