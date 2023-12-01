using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges
{
    public static class Trebuchet
    {
        private static List<(char digit, string word)> digits = new List<(char, string)>()
        {
            ('1',"one"),
            ('2',"two"),
            ('3', "three"),
            ('4', "four"),
            ('5', "five"),
            ('6',  "six"),
            ('7', "seven"),
            ('8',  "eight"),
            ('9', "nine"),
            ('0', "zero")
        };

        public static long Solve()
        {
            var lines = File.ReadAllLines(@"input1.txt");
            long sum = 0;

            foreach (var line in lines)
            {
                sum += Calibration(line);
            }

            return sum;
        }

        private static int Calibration(string input)
        {
            var result1 = IndexOfLeftWordDigit(input);

            var value = 0;
            var indexLeft = input.IndexOfAny(digits.Select(x => x.digit).ToArray());

            if (indexLeft < result1.index)
            {
                value = (input.ToCharArray()[indexLeft] - 48) *10;
            }
            else
            {
                value = result1.value * 10;
            }

            var indexRight = input.LastIndexOfAny(digits.Select(x => x.digit).ToArray());
            var result2 = IndexOfLastWordDigit(input);

            if (indexRight < result2.index)
            {
                value += result2.value;
            }
            else
            {
                value += input.ToCharArray()[indexRight] - 48;
            }

            return value;
        }

        private static (int index, int value) IndexOfLeftWordDigit(string input)
        {
            var minIndex = input.Length;
            int value = 0;
            foreach (var digit in digits)
            {
                var index = input.IndexOf(digit.word);
                if (index >= 0 && index <= minIndex)
                {
                    value = digit.digit - 48;
                    minIndex = index;
                }
            }

            return (minIndex, value);
        }

        private static (int index, int value) IndexOfLastWordDigit(string input)
        {
            var maxIndex = 0;
            int value = 0;
            foreach (var digit in digits)
            {
                var index = input.LastIndexOf(digit.word);
                if (index >= 0 && index > maxIndex)
                {
                    value = digit.digit - 48;
                    maxIndex = index;
                }
            }

            return (maxIndex, value);
        }
    }
}
