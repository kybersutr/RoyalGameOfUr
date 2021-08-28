using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalGameOfUr
{
    class Board
    {
        public List<Dice> dice = new List<Dice>() { new Dice(), new Dice(), new Dice(), new Dice() };
        public List<Tile> tiles = new List<Tile>()
        {
            // tiles are numbered in order <- first row, -> second row, <- third row
            new Tile(),
            new RosetteTile(),
            new EndTile(Program.game.player1),
            new StartTile(Program.game.player1),
            new Tile(),
            new Tile(),
            new Tile(),
            new RosetteTile(),
            new Tile(),
            new Tile(),
            new Tile(),
            new RosetteTile(),
            new Tile(),
            new Tile(),
            new Tile(),
            new Tile(),
            new Tile(),
            new RosetteTile(),
            new EndTile(Program.game.player2),
            new StartTile(Program.game.player2),
            new Tile(),
            new Tile(),
            new Tile(),
            new RosetteTile(),
        };

    }
}
