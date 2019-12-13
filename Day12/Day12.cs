using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    public static class Day12
    {
        private class Coordinates
        {
            public Coordinates(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        private class Moon
        {
            public Moon(int x, int y, int z)
            {
                Position = new Coordinates(x, y, z);
                Velocity = new Coordinates(0, 0, 0);
            }

            public Coordinates Position { get; }
            public Coordinates Velocity { get; }

            public string GetStateX()
            {
                return $"{Position.X},{Velocity.X}";
            }
            public string GetStateY()
            {
                return $"{Position.Y},{Velocity.Y}";
            }

            public string GetStateZ()
            {
                return $"{Position.Z},{Velocity.Z}";
            }
        }

        public static void Execute()
        {
            /*
                <x=-1, y=0, z=2>
                <x=2, y=-10, z=-7>
                <x=4, y=-8, z=8>
                <x=3, y=5, z=-1>
            */

            //var moons = new List<Moon>
            //{
            //    new Moon(-1, 0, 2) ,
            //    new Moon(2, -10, -7),
            //    new Moon(4,  -8, 8 ),
            //    new Moon(3, 5,  -1 )
            //};

            /*
                <x=-4, y=-9, z=-3>
                <x=-13, y=-11, z=0>
                <x=-17, y=-7, z=15>
                <x=-16, y=4, z=2>
            */

            var moons = new List<Moon>
            {
                new Moon(-4, -9, -3) ,
                new Moon(-13, -11, 0),
                new Moon(-17,  -7, 15 ),
                new Moon(-16, 4,  2 )
            };

            var previousStatesX = new Dictionary<string, int>();
            var previousStatesY = new Dictionary<string, int>();
            var previousStatesZ = new Dictionary<string, int>();

            var xRepeat = 0;
            var yRepeat = 0;
            var zRepeat = 0;

            var index = 1;

            while (true)
            {
                for (var a = 0; a <= 3; a++)
                {
                    for (var b = a + 1; b <= 3; b++)
                    {
                        var moonA = moons[a];
                        var moonB = moons[b];

                        if (moonA.Position.X > moonB.Position.X)
                        {
                            moonB.Velocity.X++;
                            moonA.Velocity.X--;
                        }
                        else if (moonA.Position.X < moonB.Position.X)
                        {
                            moonA.Velocity.X++;
                            moonB.Velocity.X--;
                        }

                        if (moonA.Position.Y > moonB.Position.Y)
                        {
                            moonB.Velocity.Y++;
                            moonA.Velocity.Y--;
                        }
                        else if (moonA.Position.Y < moonB.Position.Y)
                        {
                            moonA.Velocity.Y++;
                            moonB.Velocity.Y--;
                        }

                        if (moonA.Position.Z > moonB.Position.Z)
                        {
                            moonB.Velocity.Z++;
                            moonA.Velocity.Z--;
                        }
                        else if (moonA.Position.Z < moonB.Position.Z)
                        {
                            moonA.Velocity.Z++;
                            moonB.Velocity.Z--;
                        }
                    }
                }

                foreach (var moon in moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }

                if (xRepeat == 0)
                {
                    var statesX = new List<string>();

                    foreach (var moon in moons)
                        statesX.Add(moon.GetStateX());

                    var totalState = string.Join(':', statesX.ToArray());

                    if (previousStatesX.ContainsKey(totalState))
                    {
                        xRepeat = index - previousStatesX[totalState];
                        Console.WriteLine($"X: {xRepeat}");
                    }
                    else
                    {
                        previousStatesX[totalState] = index;
                    }
                }

                if (yRepeat == 0)
                {
                    var statesY = new List<string>();

                    foreach (var moon in moons)
                        statesY.Add(moon.GetStateY());

                    var totalState = string.Join(':', statesY.ToArray());

                    if (previousStatesY.ContainsKey(totalState))
                    {
                        yRepeat = index - previousStatesY[totalState];
                        Console.WriteLine($"Y: {yRepeat}");
                    }
                    else
                    {
                        previousStatesY[totalState] = index;
                    }
                }

                if (zRepeat == 0)
                {
                    var statesZ = new List<string>();

                    foreach (var moon in moons)
                        statesZ.Add(moon.GetStateZ());

                    var totalState = string.Join(':', statesZ.ToArray());

                    if (previousStatesZ.ContainsKey(totalState))
                    {
                        zRepeat = index - previousStatesZ[totalState];
                        Console.WriteLine($"Z: {zRepeat}");
                    }
                    else
                    {
                        previousStatesZ[totalState] = index;
                    }
                }

                index++;

                if (xRepeat > 0 && yRepeat > 0 && zRepeat > 0)
                    break;
            }

            //var totalEnergy = 0;

            //foreach (var moon in moons)
            //{
            //    var potentialEnergy = Math.Abs(moon.Position.X) + Math.Abs(moon.Position.Y) + Math.Abs(moon.Position.Z);
            //    var kineticEnergy = Math.Abs(moon.Velocity.X) + Math.Abs(moon.Velocity.Y) + Math.Abs(moon.Velocity.Z);

            //    totalEnergy += (potentialEnergy * kineticEnergy);
            //}

            //Console.WriteLine(totalEnergy);

            Console.WriteLine(CalculateLcm(new long[] { xRepeat, yRepeat, zRepeat }));
        }

        private static long CalculateLcm(IEnumerable<long> numbers)
        {
            return numbers.Aggregate(CalculateLcm);
        }
        private static long CalculateLcm(long a, long b)
        {
            return Math.Abs(a * b) / CalculateGcd(a, b);
        }

        private static long CalculateGcd(long a, long b)
        {
            while (true)
            {
                if (b == 0)
                    return a;

                var a1 = a;
                a = b;
                b = a1 % b;
            }
        }
    }
}
