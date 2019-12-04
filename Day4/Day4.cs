using System;
using System.Linq;

namespace Advent
{
    public static class Day4
    {
        public static void Execute()
        {
            //Console.WriteLine(IsValidPassword("112233"));
            //Console.WriteLine(IsValidPassword("123444"));
            //Console.WriteLine(IsValidPassword("111122"));

            var count = 0;

            for (var i = 109165; i <= 576723; i++)
            {
                if (IsValidPassword(i.ToString()))
                    count++;
            }

            Console.WriteLine(count);
        }

        private static bool IsValidPassword(string password)
        {
            var digits = password.ToCharArray();

            var sortedDigits = password.ToCharArray().OrderBy(c => c);

            if (!digits.SequenceEqual(sortedDigits))
                return false;

            var hasDouble = false;
            var index = 0;
            var letter = digits[0];

            while (index < 6)
            {
                var count = 1;

                while (index < 5 && digits[index + 1] == letter)
                {
                    count++;
                    index++;
                }

                if (count == 2)
                {
                    hasDouble = true;
                    break;
                }

                index++;

                if (index < 6)
                    letter = digits[index];
            }

            return hasDouble;
        }
    }
}
