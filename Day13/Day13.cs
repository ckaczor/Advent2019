using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day13
    {
        private static Dictionary<Tuple<long, long>, long> _tiles;

        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day13\input2.txt");

            var game = new IntcodeComputer(lines[0], 0, OnInput);

            _tiles = new Dictionary<Tuple<long, long>, long>();

            while (!game.Halted)
            {
                var x = game.Execute(0);
                var y = game.Execute(0);
                var tile = game.Execute(0);

                _tiles[new Tuple<long, long>(x, y)] = tile;
            }

            var score = _tiles[new Tuple<long, long>(-1, 0)];

            Console.WriteLine(score);

            //Console.WriteLine(_tiles.Values.Count(t => t == 2));
        }

        private static long OnInput()
        {
            //DrawScreen(_tiles);

            var ballPos = _tiles.First(t => t.Value == 4);
            var paddlePos = _tiles.First(t => t.Value == 3);

            if (ballPos.Key.Item1 > paddlePos.Key.Item1)
                return 1;

            if (ballPos.Key.Item1 < paddlePos.Key.Item1)
                return -1;

            return 0;

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.Write("Input: ");

            //var key = Console.ReadKey();

            //if (key.Key == ConsoleKey.LeftArrow)
            //    return -1;

            //if (key.Key == ConsoleKey.RightArrow)
            //    return 1;

            //return 0;
        }

        private static void DrawScreen(Dictionary<Tuple<long, long>, long> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile.Key.Item1 == -1 && tile.Key.Item2 == 0)
                {
                    Debug.WriteLine($"Score: {tile.Value}");
                    continue;
                }

                Console.SetCursorPosition((int)tile.Key.Item1, (int)tile.Key.Item2);

                var c = ' ';

                switch (tile.Value)
                {
                    case 1:
                        c = 'W';
                        break;
                    case 2:
                        c = 'B';
                        break;
                    case 3:
                        c = 'P';
                        break;
                    case 4:
                        c = 'O';
                        break;
                }

                Console.Write(c);
            }
        }
    }
}
