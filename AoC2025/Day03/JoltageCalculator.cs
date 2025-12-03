using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class JoltageCalculator
    {
        public long Calculate(string file = @"C:\Users\colin.overton\Documents\AoC2025\day3input.txt")
        {
            IEnumerable<string> lines;

            //lines = @"987654321111111,811111111111119,234234234234278,818181911112111".Split(',');
            lines = File.ReadAllLines(file);

            long total = 0;
            const int numdigits = 12;

            foreach (var line in lines)
            {
                var digitPos = new int[numdigits];
                for (int d = 0; d < numdigits; d++)
                {
                    var start = d == 0 ? 0 : digitPos[d - 1] + 1;
                    var pos = FindBigDigit(line, start, numdigits, d);
                    digitPos[d] = pos;
                }
                var num = GetValue(line, digitPos);
                total += num;
            }

            Debug.WriteLine("Day3:" + total + " jolts");
            
            return total;
        }

        int FindBigDigit(string line, int startPos, int numdigits, int currdigit)
        {
            var lastIdx = line.Length - 1;
            lastIdx -= (numdigits - currdigit) - 1;

            for (int i = 9; i > 0; i--)
            {
                var idx = line.IndexOf(i.ToString()[0], startPos);
                if (idx > -1 && idx <= lastIdx)
                {
                    return idx;
                }
            }

            throw new InvalidOperationException();
        }

        long GetValue(string line, int[] digitPos)
        {
            long joltage = 0;

            for (int i = 0; i < digitPos.Length; i++)
            {
                var num = Int32.Parse(line[digitPos[i]].ToString());
                joltage += num * (long)Math.Pow(10, digitPos.Length - i - 1);
            }
            
            return joltage;
        }
    }
}
