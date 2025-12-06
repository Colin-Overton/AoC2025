using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class CephalopodMaths
    {
        public long DoHomework(string file = @"C:\Users\colin.overton\Documents\AoC2025\day6input.txt")
        {
            IEnumerable<string> lines;

            var lineStr = @"123 328  51 64 
                            45 64  387 23 
                             6 98  215 314
                            *   +   *   +  ";

            //lines = Utils.GetLines(lineStr);
            lines = File.ReadLines(file);

            string[] ops = null;
            List<long[]> nums = new List<long[]>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (parts[0] == "*" || parts[0] == "+")
                {
                    ops = parts;
                }
                else
                {
                    nums.Add(parts.Select(Int64.Parse).ToArray());
                }
            }

            var total = Enumerable.Range(0, ops.Length)
                                  .Select(i =>
                                  {
                                      var problemNums = nums.Select(n => n[i]);
                                      if (ops[i] == "+")
                                      {
                                          return problemNums.Sum();
                                      }
                                      else
                                      {
                                          return problemNums.Aggregate(1L, (a, x) => a * x);
                                      }
                                  })
                                  .Sum();


            Debug.WriteLine("Day5:" + total + " count");

            return total;
        }

        public long DoHomework2(string file = @"C:\Users\colin.overton\Documents\AoC2025\day6input.txt")
        {
            IEnumerable<string> lines;

            var lineStr = @"123 328  51 64 , 45 64  387 23 ,  6 98  215 314,*   +   *   +  ";

            //lines = lineStr.Split(",");
            lines = File.ReadLines(file);

            var chars = lines.Select(l => l.ToArray()).ToArray();

            IEnumerable<int> gaps = FindGaps(chars);
            gaps = gaps.Concat(new[] { chars[0].Length });

            long total = 0;
            int start = 0;
            foreach (var gap in gaps)
            {
                int end = gap - 1;
                int width = end - start + 1;

                var nums = Enumerable.Range(start, width)
                                     .Select(i => new string(chars.Take(chars.Length - 1).Select(line => line[i]).ToArray()))
                                     .Select(Int64.Parse)
                                     .ToArray();

                var op = chars.Last().Skip(start).Take(width).First(c => c != ' ');

                var solution = op == '+' ? nums.Sum() :
                                           nums.Aggregate(1L, (a, x) => a * x);


                total += solution;
                start = gap + 1;
            }

            Debug.WriteLine("Day5:" + total + " count");

            return total;
        }

        private int[] FindGaps(char[][] chars)
        {
            var width = chars[0].Length;
            return Enumerable.Range(0, width)
                             .Where(i => chars.All(line => line[i] == ' '))
                             .ToArray();
        }
    }
}
