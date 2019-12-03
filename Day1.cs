using System;
using System.IO;

namespace Advent
{
    public static class Day1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@"C:\Users\chris\OneDrive\Desktop\input.txt");

            var total = 0.0;

            foreach (var line in lines)
            {
                var mass = double.Parse(line);

                var fuel = CalculateFuel(mass);

                total += fuel;
            }

            Console.WriteLine(total);
        }

        private static double CalculateFuel(double mass)
        {
            var totalFuel = 0.0;

            var fuel = Math.Floor(mass / 3) - 2;

            totalFuel += fuel;

            while (fuel > 2)
            {
                fuel = Math.Floor(fuel / 3) - 2;

                if (fuel < 0)
                    fuel = 0;

                totalFuel += fuel;
            }

            return totalFuel;
        }
    }
}
