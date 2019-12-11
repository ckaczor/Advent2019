using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var bestAngles = new Dictionary<double, Tuple<int, int>>();

            foreach (var asteroid in asteroids)
            {
                if (asteroid.Equals(bestLocation))
                    continue;

                var deltaY = asteroid.Item1 - bestLocation.Item1;
                var deltaX = asteroid.Item2 - bestLocation.Item2;

                var angle = Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI;

                if (!bestAngles.ContainsKey(angle))
                    bestAngles[angle] = asteroid;
                else
                {
                    var bestDistance = Math.Sqrt(Math.Pow(bestAngles[angle].Item2 - bestLocation.Item2, 2) + Math.Pow(bestAngles[angle].Item1 - bestLocation.Item1, 2));
                    var currentDistance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

                    if (currentDistance < bestDistance)
                        bestAngles[angle] = asteroid;
                }
            }

            var sortedAngles = bestAngles.Where(b => b.Key >= 0).OrderByDescending(b => b.Key).ToList();

            sortedAngles.AddRange(bestAngles.Where(b => b.Key < 0).OrderByDescending(b => b.Key).ToList());

            var betAngle = sortedAngles[199];

            Console.WriteLine(betAngle.Value.Item1 * 100 + betAngle.Value.Item2);
        }
    }
}
