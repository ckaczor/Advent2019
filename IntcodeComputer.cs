using System;
using System.Linq;

namespace Advent
{
    public class IntcodeComputer
    {
        private enum ParameterMode
        {
            Position,
            Immediate,
            Relative
        }

        private readonly long[] _memory;
        private readonly long _phase;

        private bool _setPhase;
        private long _instructionPointer;
        private long _output;
        private long _relativeBase;

        public bool Halted { get; private set; }

        public IntcodeComputer(string memoryString, long phase)
        {
            _memory = new long[5000];

            memoryString.Split(',').Select(long.Parse).ToArray().CopyTo(_memory, 0);

            _phase = phase;
        }

        private long GetValue(long parameter, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Position:
                    return _memory[parameter];
                case ParameterMode.Immediate:
                    return parameter;
                case ParameterMode.Relative:
                    return _memory[_relativeBase + parameter];
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private void SetValue(long value, long location, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Position:
                    _memory[location] = value;
                    break;
                case ParameterMode.Relative:
                    _memory[_relativeBase + location] = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public long Execute(long input)
        {
            while (!Halted)
            {
                var (mode1, mode2, mode3, opCode) = ParseOpCode(_memory[_instructionPointer]);

                switch (opCode)
                {
                    case 1:
                        {
                            var newValue = GetValue(_memory[_instructionPointer + 1], mode1) + GetValue(_memory[_instructionPointer + 2], mode2);

                            SetValue(newValue, _memory[_instructionPointer + 3], mode3);

                            _instructionPointer += 4;

                            break;
                        }

                    case 2:
                        {
                            var newValue = GetValue(_memory[_instructionPointer + 1], mode1) * GetValue(_memory[_instructionPointer + 2], mode2);

                            SetValue(newValue, _memory[_instructionPointer + 3], mode3);

                            _instructionPointer += 4;

                            break;
                        }

                    case 3:
                        var inputValue = _setPhase ? input : _phase;

                        _setPhase = true;

                        var writeValue = _memory[_instructionPointer + 1];

                        SetValue(inputValue, _memory[_instructionPointer + 3], mode3);

                        _instructionPointer += 2;

                        break;

                    case 4:
                        _output = GetValue(_memory[_instructionPointer + 1], mode1);

                        _instructionPointer += 2;

                        return _output;

                    case 5:
                        {
                            var value = GetValue(_memory[_instructionPointer + 1], mode1);

                            if (value != 0)
                                _instructionPointer = GetValue(_memory[_instructionPointer + 2], mode2);
                            else
                                _instructionPointer += 3;
                        }

                        break;

                    case 6:
                        {
                            var value = GetValue(_memory[_instructionPointer + 1], mode1);

                            if (value == 0)
                                _instructionPointer = GetValue(_memory[_instructionPointer + 2], mode2);
                            else
                                _instructionPointer += 3;
                        }

                        break;

                    case 7:
                        {
                            var value1 = GetValue(_memory[_instructionPointer + 1], mode1);
                            var value2 = GetValue(_memory[_instructionPointer + 2], mode2);

                            if (value1 < value2)
                                SetValue(1, _memory[_instructionPointer + 3], mode3);
                            else
                                SetValue(0, _memory[_instructionPointer + 3], mode3);

                            _instructionPointer += 4;
                        }

                        break;

                    case 8:
                        {
                            var value1 = GetValue(_memory[_instructionPointer + 1], mode1);
                            var value2 = GetValue(_memory[_instructionPointer + 2], mode2);

                            if (value1 == value2)
                                SetValue(1, _memory[_instructionPointer + 3], mode3);
                            else
                                SetValue(0, _memory[_instructionPointer + 3], mode3);

                            _instructionPointer += 4;
                        }

                        break;

                    case 9:
                        _relativeBase += GetValue(_memory[_instructionPointer + 1], mode1);

                        _instructionPointer += 2;

                        break;

                    case 99:
                        Halted = true;

                        return _output;
                }
            }

            return _output;
        }

        private static (ParameterMode mode1, ParameterMode mode2, ParameterMode mode3, int opCode) ParseOpCode(long fullOpCode)
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
