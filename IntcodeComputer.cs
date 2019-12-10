using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    public class IntcodeComputer
    {
        private enum ParameterMode
        {
            Position,
            Immediate
        }

        private static int GetValue(IReadOnlyList<int> codes, int parameter, ParameterMode mode)
        {
            return mode == ParameterMode.Immediate ? parameter : codes[parameter];
        }

        public int? Execute(int[] inputs, string commands)
        {
            var codes = commands.Split(',').Select(int.Parse).ToArray();

            var position = 0;
            var done = false;
            var inputIndex = 0;
            int? output = null;

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
                        codes[codes[position + 1]] = inputs[inputIndex++];

                        position += 2;

                        break;

                    case 4:
                        output = GetValue(codes, codes[position + 1], ParameterMode.Position);

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

            return output;
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
