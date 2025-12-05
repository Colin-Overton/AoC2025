using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class IngredientFinder
    {
        public long CountEm(string file = @"C:\Users\colin.overton\Documents\AoC2025\day5input.txt")
        {
            IEnumerable<string> lines;

            var lineStr = @"3-5
                            10-14
                            16-20
                            12-18

                            1
                            5
                            8
                            11
                            17
                            32";

            //lines = Utils.GetLines(lineStr);
            lines = File.ReadLines(file);

            bool inRanges = true;
            var ranges = new List<Range>();
            long count = 0;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    break;
                }
                else if (inRanges)
                {
                    var rng = line.Split('-').Select(Int64.Parse).ToArray();
                    ranges.Add(new Range
                    {
                        Start = rng[0],
                        End = rng[1],
                    });
                }
            }

            while (Merge(ranges)) { }

            count = ranges.Select(r => r.End - r.Start + 1).Sum();

            Debug.WriteLine("Day5:" + count + " count");

            return count;
        }

        private bool Merge(List<Range> ranges)
        {
            bool merged = false;
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = 0; j < ranges.Count && i < ranges.Count; j++)
                {
                    if (i == j) continue;
                    if (ranges[i].Overlaps(ranges[j]))
                    {
                        ranges[i].Merge(ranges[j]);
                        ranges.RemoveAt(j);
                        j--;
                        merged = true;
                    }
                }
            }
            return merged;
        }

        [DebuggerDisplay("{Start}-{End}")]
        private class Range
        {
            public long Start;
            public long End;

            public void Merge(Range other)
            {
                Start = Math.Min(Start, other.Start);
                End = Math.Max(End, other.End);
            }

            public bool Overlaps(Range other)
            {
                return Between(other.Start) ||
                       Between(other.End) ||
                       Covers(other) ||
                       other.Covers(this);
            }

            private bool Between(long i)
            {
                return i >= Start && i <= End;
            }
            private bool Covers(Range other)
            {
                return other.Start < Start && other.End > End;
            }
        }
    }
}
