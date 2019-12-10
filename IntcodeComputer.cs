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

        private readonly int[] _memory;
        private readonly int _phase;

        private bool _setPhase;
        private int _instructionPointer;
        private int _output;

        public bool Halted { get; private set; }

        public IntcodeComputer(string memoryString, int phase)
        {
            _memory = memoryString.Split(',').Select(int.Parse).ToArray();
            _phase = phase;
        }

        private static int GetValue(IReadOnlyList<int> codes, int parameter, ParameterMode mode)
        {
            return mode == ParameterMode.Immediate ? parameter : codes[parameter];
        }

        public int Execute(int input)
        {
            while (!Halted)
            {
                var (mode1, mode2, mode3, opCode) = ParseOpCode(_memory[_instructionPointer]);

                switch (opCode)
                {
                    case 1:
                        _memory[_memory[_instructionPointer + 3]] = GetValue(_memory, _memory[_instructionPointer + 1], mode1) + GetValue(_memory, _memory[_instructionPointer + 2], mode2);

                        _instructionPointer += 4;

                        break;

                    case 2:
                        _memory[_memory[_instructionPointer + 3]] = GetValue(_memory, _memory[_instructionPointer + 1], mode1) * GetValue(_memory, _memory[_instructionPointer + 2], mode2);

                        _instructionPointer += 4;

                        break;

                    case 3:
                        var inputValue = _setPhase ? input : _phase;

                        _setPhase = true;

                        _memory[_memory[_instructionPointer + 1]] = inputValue;

                        _instructionPointer += 2;

                        break;

                    case 4:
                        _output = GetValue(_memory, _memory[_instructionPointer + 1], ParameterMode.Position);

                        _instructionPointer += 2;

                        return _output;

                    case 5:
                        {
                            var value = GetValue(_memory, _memory[_instructionPointer + 1], mode1);

                            if (value != 0)
                                _instructionPointer = GetValue(_memory, _memory[_instructionPointer + 2], mode2);
                            else
                                _instructionPointer += 3;
                        }

                        break;

                    case 6:
                        {
                            var value = GetValue(_memory, _memory[_instructionPointer + 1], mode1);

                            if (value == 0)
                                _instructionPointer = GetValue(_memory, _memory[_instructionPointer + 2], mode2);
                            else
                                _instructionPointer += 3;
                        }

                        break;

                    case 7:
                        {
                            var value1 = GetValue(_memory, _memory[_instructionPointer + 1], mode1);
                            var value2 = GetValue(_memory, _memory[_instructionPointer + 2], mode2);

                            if (value1 < value2)
                                _memory[_memory[_instructionPointer + 3]] = 1;
                            else
                                _memory[_memory[_instructionPointer + 3]] = 0;

                            _instructionPointer += 4;
                        }

                        break;

                    case 8:
                        {
                            var value1 = GetValue(_memory, _memory[_instructionPointer + 1], mode1);
                            var value2 = GetValue(_memory, _memory[_instructionPointer + 2], mode2);

                            if (value1 == value2)
                                _memory[_memory[_instructionPointer + 3]] = 1;
                            else
                                _memory[_memory[_instructionPointer + 3]] = 0;

                            _instructionPointer += 4;
                        }

                        break;

                    case 99:
                        Halted = true;

                        return _output;
                }
            }

            return _output;
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
