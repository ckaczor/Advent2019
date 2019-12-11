using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    public static class Day10
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day10\input.txt");

            var y = 0;

            var asteroids = new List<Tuple<int, int>>();

            foreach (var line in lines)
            {
                var x = 0;

                foreach (var cell in line.ToCharArray())
                {
                    if (cell == '#')
                        asteroids.Add(new Tuple<int, int>(x, y));

                    x++;
                }

                y++;
            }

            var bestLocation = new Tuple<int, int>(-1, -1);
            var bestCount = 0;

            foreach (var potentialAsteroid in asteroids)
            {
                var angles = new HashSet<double>();

                foreach (var asteroid in asteroids)
                {
                    if (asteroid.Equals(potentialAsteroid))
                        continue;

                    var angle = Math.Atan2(asteroid.Item2 - potentialAsteroid.Item2, asteroid.Item1 - potentialAsteroid.Item1);

                    if (!angles.Contains(angle))
                        angles.Add(angle);
                }

                if (angles.Count > bestCount)
                {
                    bestCount = angles.Count;
                    bestLocation = potentialAsteroid;
                }
            }

            Console.WriteLine($"{bestLocation.Item1},{bestLocation.Item2} = {bestCount}");
        }
    }
}
