using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design.Behavior;

namespace AoC2025
{
    internal class ProductIdValidator
    {
        public long Validate(string file = @"C:\Users\colin.overton\Documents\AoC2025\day2input.txt")
        {
            //var productIds = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
            var productIds = File.ReadLines(file).First();

            var prodRanges = productIds.Split(',');
            long total = 0;

            foreach (var prodRange in prodRanges)
            {
                var rng = prodRange.Split('-');
                var min = long.Parse(rng[0]);
                var max = long.Parse(rng[1]);

                long minFirstHalf = rng[0].Length % 2 == 0 ? long.Parse(rng[0].Substring(0, rng[0].Length / 2))
                                                           : (long)Math.Pow(10, rng[0].Length / 2);

                Debug.WriteLine(prodRange + " => " + minFirstHalf);

                long currHalf = minFirstHalf, testVal;
                while ((testVal = MakeLong(currHalf)) < min)
                {
                    currHalf++;
                }
                while ((testVal = MakeLong(currHalf)) <= max)
                {
                    total += testVal;
                    currHalf++;
                }
            }

            return total;
        }

        public long ValidatePart2(string file = @"C:\Users\colin.overton\Documents\AoC2025\day2input.txt")
        {
            //var productIds = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
            var productIds = File.ReadLines(file).First();

            var prodRanges = productIds.Split(',');
            long total = 0;

            foreach (var prodRange in prodRanges)
            {
                var rng = prodRange.Split('-');
                var min = long.Parse(rng[0]);
                var max = long.Parse(rng[1]);

                //check for repeated ids, e.g. 222222 can be 2, 22, 222 repeated
                var foundIds = new HashSet<long>();

                for (int i = 1; ; i++)
                {
                    if (MakeLong(i) > max)
                    {
                        break;
                    }

                    for (int reps = 1; ; reps++)
                    {
                        var currLong = MakeLong(i, reps);
                        if (currLong < min)
                        {
                            //onwards
                        }
                        else if (currLong > max)
                        {
                            break;
                        }
                        else
                        {
                            if (!foundIds.Contains(currLong))
                            {
                                total += currLong;
                                foundIds.Add(currLong);
                            }
                        }
                    }
                }
            }

            Debug.WriteLine("Day2:" + total + " total");

            return total;
        }

        private long MakeLong(long currHalf)
        {
            return long.Parse(currHalf.ToString() + currHalf.ToString());
        }

        private long MakeLong(long currPart, int reps)
        {
            return long.Parse(string.Concat(Enumerable.Repeat(currPart.ToString(), reps)));
        }
    }
}
