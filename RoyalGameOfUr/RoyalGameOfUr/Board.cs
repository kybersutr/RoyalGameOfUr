using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Board
    {
        public int width;
        public int height;
        public List<Dice> dice = new List<Dice>() { new Dice(), new Dice(), new Dice(), new Dice() };
        public List<Tile> tiles = new List<Tile>()
        {
            // tiles are numbered in order <- first row, -> second row, <- third row
            new Tile(1),
            new Tile(2),
            new Tile(0),
            new Tile(0),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(2),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(2),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(2),
            new Tile(0),
            new Tile(0),
            new Tile(1),
            new Tile(1),
            new Tile(1),
            new Tile(2),
        };

        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
        }


    }
}
