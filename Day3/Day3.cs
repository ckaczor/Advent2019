using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day3
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day3\input.txt");

            //lines[0] = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            //lines[1] = "U62,R66,U55,R34,D71,R55,D58,R83";

            //lines[0] = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            //lines[1] = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

            var spaces0 = GetWireSpaces(lines[0]);
            var spaces1 = GetWireSpaces(lines[1]);

            var intersections = spaces0.Keys.Intersect(spaces1.Keys).ToList();

            var best = int.MaxValue;

            foreach (var xy in intersections)
            {
                var distance = spaces0[xy] + spaces1[xy];

                if (distance < best)
                    best = distance;
            }

            Console.WriteLine($"{best}");
        }

        private static Dictionary<string, int> GetWireSpaces(string fullPath)
        {
            var x = 0;
            var y = 0;
            var steps = 1;

            var usedSpaces = new Dictionary<string, int>();

            foreach (var pathSegment in fullPath.Split(','))
            {
                var direction = pathSegment[0];
                var count = int.Parse(pathSegment[1..]);

                for (var i = 0; i < count; i++)
                {
                    switch (direction)
                    {
                        case 'U':
                            y++;
                            break;

                        case 'D':
                            y--;
                            break;

                        case 'L':
                            x--;
                            break;

                        case 'R':
                            x++;
                            break;
                    }

                    usedSpaces[$"{x},{y}"] = steps++;
                }
            }

            return usedSpaces;
        }
    }
}
