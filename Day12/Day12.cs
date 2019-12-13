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

            public string GetState()
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

            //Console.WriteLine(lcm_of_array_elements(new long[] { 113028, 167624, 231614 }));
            //return;

            var previousStates = new Dictionary<string, int>();

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

                var states = new List<string>();

                foreach (var moon in moons)
                    states.Add(moon.GetState());

                var totalState = string.Join(':', states.ToArray());

                if (previousStates.ContainsKey(totalState))
                {
                    Console.WriteLine(index - previousStates[totalState]);
                    break;
                }
                else
                {
                    previousStates[totalState] = index++;
                }
            }

            //var totalEnergy = 0;

            //foreach (var moon in moons)
            //{
            //    var potentialEnergy = Math.Abs(moon.Position.X) + Math.Abs(moon.Position.Y) + Math.Abs(moon.Position.Z);
            //    var kineticEnergy = Math.Abs(moon.Velocity.X) + Math.Abs(moon.Velocity.Y) + Math.Abs(moon.Velocity.Z);

            //    totalEnergy += (potentialEnergy * kineticEnergy);
            //}

            //Console.WriteLine(totalEnergy);
        }

        private static long lcm_of_array_elements(long[] element_array)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0. 
                    // For negative number we convert into 
                    // positive and calculate lcm_of_array_elements. 
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete 
                    // division i.e. without remainder then replace 
                    // number with quotient; used for find next factor 
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number 
                // from array multiply with lcm_of_array_elements 
                // and store into lcm_of_array_elements and continue 
                // to same divisor for next factor finding. 
                // else increment divisor 
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate  
                // we found all factors and terminate while loop. 
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }
    }
}
