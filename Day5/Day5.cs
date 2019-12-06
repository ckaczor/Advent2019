using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    public static class Day5
    {
        private enum ParameterMode
        {
            Position,
            Immediate
        }

        public static void Execute()
        {
            //Calculate(8, "3,3,1107,-1,8,3,4,3,99");

            Calculate(5, "3,225,1,225,6,6,1100,1,238,225,104,0,1102,27,28,225,1,113,14,224,1001,224,-34,224,4,224,102,8,223,223,101,7,224,224,1,224,223,223,1102,52,34,224,101,-1768,224,224,4,224,1002,223,8,223,101,6,224,224,1,223,224,223,1002,187,14,224,1001,224,-126,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,1102,54,74,225,1101,75,66,225,101,20,161,224,101,-54,224,224,4,224,1002,223,8,223,1001,224,7,224,1,224,223,223,1101,6,30,225,2,88,84,224,101,-4884,224,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,1001,214,55,224,1001,224,-89,224,4,224,102,8,223,223,1001,224,4,224,1,224,223,223,1101,34,69,225,1101,45,67,224,101,-112,224,224,4,224,102,8,223,223,1001,224,2,224,1,223,224,223,1102,9,81,225,102,81,218,224,101,-7290,224,224,4,224,1002,223,8,223,101,5,224,224,1,223,224,223,1101,84,34,225,1102,94,90,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1007,677,677,224,102,2,223,223,1005,224,329,101,1,223,223,1108,226,677,224,1002,223,2,223,1005,224,344,101,1,223,223,1008,677,677,224,102,2,223,223,1005,224,359,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,374,101,1,223,223,108,226,677,224,1002,223,2,223,1006,224,389,1001,223,1,223,1107,226,677,224,102,2,223,223,1005,224,404,1001,223,1,223,7,226,677,224,1002,223,2,223,1005,224,419,101,1,223,223,1107,677,226,224,102,2,223,223,1006,224,434,1001,223,1,223,1107,226,226,224,1002,223,2,223,1006,224,449,101,1,223,223,1108,226,226,224,1002,223,2,223,1005,224,464,101,1,223,223,8,677,226,224,102,2,223,223,1005,224,479,101,1,223,223,8,226,226,224,1002,223,2,223,1006,224,494,1001,223,1,223,1007,226,677,224,1002,223,2,223,1006,224,509,1001,223,1,223,108,226,226,224,1002,223,2,223,1006,224,524,1001,223,1,223,1108,677,226,224,102,2,223,223,1006,224,539,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,554,101,1,223,223,107,226,677,224,1002,223,2,223,1006,224,569,101,1,223,223,107,677,677,224,102,2,223,223,1006,224,584,101,1,223,223,7,677,226,224,102,2,223,223,1005,224,599,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,614,1001,223,1,223,107,226,226,224,1002,223,2,223,1005,224,629,101,1,223,223,7,226,226,224,102,2,223,223,1006,224,644,1001,223,1,223,1007,226,226,224,102,2,223,223,1006,224,659,101,1,223,223,108,677,677,224,102,2,223,223,1005,224,674,1001,223,1,223,4,223,99,226");
        }

        private static int GetValue(IReadOnlyList<int> codes, int parameter, ParameterMode mode)
        {
            if (mode == ParameterMode.Immediate)
                return parameter;

            return codes[parameter];
        }

        private static void Calculate(int input, string commands)
        {
            var codes = commands.Split(',').Select(int.Parse).ToArray();

            var position = 0;
            var done = false;

            while (!done)
            {
                var (mode1, mode2, mode3, opCode) = ParseOpCode(codes[position]);

                switch (opCode)
                {
                    case 1:
                        codes[codes[position + 3]] = GetValue(codes, codes[position + 1], mode1) + GetValue(codes, codes[position + 2], mode2);

                        position += 4;

                        break;

                    case 2:
                        codes[codes[position + 3]] = GetValue(codes, codes[position + 1], mode1) * GetValue(codes, codes[position + 2], mode2);

                        position += 4;

                        break;

                    case 3:
                        codes[codes[position + 1]] = input;

                        position += 2;

                        break;

                    case 4:
                        var output = GetValue(codes, codes[position + 1], ParameterMode.Position);

                        Console.Write(output);

                        position += 2;

                        break;

                    case 5:
                        {
                            var value = GetValue(codes, codes[position + 1], mode1);

                            if (value != 0)
                                position = GetValue(codes, codes[position + 2], mode2);
                            else
                                position += 3;
                        }

                        break;

                    case 6:
                        {
                            var value = GetValue(codes, codes[position + 1], mode1);

                            if (value == 0)
                                position = GetValue(codes, codes[position + 2], mode2);
                            else
                                position += 3;
                        }

                        break;

                    case 7:
                        {
                            var value1 = GetValue(codes, codes[position + 1], mode1);
                            var value2 = GetValue(codes, codes[position + 2], mode2);

                            if (value1 < value2)
                                codes[codes[position + 3]] = 1;
                            else
                                codes[codes[position + 3]] = 0;

                            position += 4;
                        }

                        break;

                    case 8:
                        {
                            var value1 = GetValue(codes, codes[position + 1], mode1);
                            var value2 = GetValue(codes, codes[position + 2], mode2);

                            if (value1 == value2)
                                codes[codes[position + 3]] = 1;
                            else
                                codes[codes[position + 3]] = 0;

                            position += 4;
                        }

                        break;

                    case 99:
                        done = true;

                        break;
                }
            }
        }

        private static (ParameterMode mode1, ParameterMode mode2, ParameterMode mode3, int opCode) ParseOpCode(int fullOpCode)
        {
            var codeString = fullOpCode.ToString("00000").ToCharArray();

            var mode1 = Enum.Parse<ParameterMode>(codeString[2].ToString());
            var mode2 = Enum.Parse<ParameterMode>(codeString[1].ToString());
            var mode3 = Enum.Parse<ParameterMode>(codeString[0].ToString());

            var opCode = int.Parse(codeString[3].ToString() + codeString[4]);

            return (mode1, mode2, mode3, opCode);
        }
    }
}
