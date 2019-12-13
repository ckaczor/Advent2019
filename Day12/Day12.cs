using System;
using System.Collections.Generic;

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
        }

        public static void Execute()
        {
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

            for (var count = 1; count <= 1000; count++)
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
            }

            var totalEnergy = 0;

            foreach (var moon in moons)
            {
                var potentialEnergy = Math.Abs(moon.Position.X) + Math.Abs(moon.Position.Y) + Math.Abs(moon.Position.Z);
                var kineticEnergy = Math.Abs(moon.Velocity.X) + Math.Abs(moon.Velocity.Y) + Math.Abs(moon.Velocity.Z);

                totalEnergy += (potentialEnergy * kineticEnergy);
            }

            Console.WriteLine(totalEnergy);
        }
    }
}
