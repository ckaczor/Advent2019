using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day13
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day13\input.txt");

            var game = new IntcodeComputer(lines[0], 0);

            var tiles = new Dictionary<Tuple<long, long>, long>();

            while (!game.Halted)
            {
                var x = game.Execute(0);
                var y = game.Execute(0);
                var tile = game.Execute(0);

                tiles[new Tuple<long, long>(x, y)] = tile;
            }

            Console.WriteLine(tiles.Values.Count(t => t == 2));
        }
    }
}
