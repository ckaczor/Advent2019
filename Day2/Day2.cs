using System;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day2
    {
        public static void Execute()
        {
            for (var noun = 0; noun <= 99; noun++)
                for (var verb = 0; verb <= 99; verb++)
                {
                    var result = Calculate(noun, verb);

                    if (result == 19690720)
                        Console.WriteLine(100 * noun + verb);
                }
        }

        private static int Calculate(int noun, int verb)
        {
            var lines = File.ReadAllLines(@".\Day2\input.txt");

            var codes = lines[0].Split(',').Select(int.Parse).ToArray();

            codes[1] = noun;
            codes[2] = verb;

            var position = 0;
            var done = false;

            while (!done)
            {
                switch (codes[position])
                {
                    case 1:
                        codes[codes[position + 3]] = codes[codes[position + 1]] + codes[codes[position + 2]];

                        position += 4;

                        break;

                    case 2:
                        codes[codes[position + 3]] = codes[codes[position + 1]] * codes[codes[position + 2]];

                        position += 4;

                        break;

                    case 99:
                        done = true;

                        break;
                }
            }

            return codes[0];
        }
    }
}
