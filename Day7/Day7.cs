using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    public static class Day7
    {
        public static void Execute()
        {
            var program = "3,8,1001,8,10,8,105,1,0,0,21,34,43,64,85,98,179,260,341,422,99999,3,9,1001,9,3,9,102,3,9,9,4,9,99,3,9,102,5,9,9,4,9,99,3,9,1001,9,2,9,1002,9,4,9,1001,9,3,9,1002,9,4,9,4,9,99,3,9,1001,9,3,9,102,3,9,9,101,4,9,9,102,3,9,9,4,9,99,3,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";
            var phaseList = GetPhaseList();

            //var program = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            //var phaseList = new List<int[]> { "9,7,8,5,6".Split(',').Select(c => int.Parse(c.ToString())).ToArray() };

            long? max = 0;

            foreach (var phases in phaseList)
            {
                var value = 0L;

                var amp1 = new IntcodeComputer(program, phases[0]);
                var amp2 = new IntcodeComputer(program, phases[1]);
                var amp3 = new IntcodeComputer(program, phases[2]);
                var amp4 = new IntcodeComputer(program, phases[3]);
                var amp5 = new IntcodeComputer(program, phases[4]);

                while (!amp5.Halted)
                {
                    value = amp1.Execute(value);
                    value = amp2.Execute(value);
                    value = amp3.Execute(value);
                    value = amp4.Execute(value);
                    value = amp5.Execute(value);

                    if (value > max)
                        max = value;
                }
            }

            Console.WriteLine(max);
        }

        private static IEnumerable<long[]> GetPhaseList()
        {
            // This is a stupid brute force way to do it but I'm lazy and it is fast so good enough

            var phaseList = new List<long[]>();

            for (var i = 0; i <= 99999; i++)
            {
                var phases = i.ToString("00000").ToCharArray().Select(c => long.Parse(c.ToString())).ToArray();

                if (phases.All(p => p >= 5) && phases.Distinct().Count() == 5)
                    phaseList.Add(phases);
            }

            return phaseList;
        }
    }
}
