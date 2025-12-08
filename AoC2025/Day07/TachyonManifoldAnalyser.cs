using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class TachyonManifoldAnalyser
    {
        public int Analyse(string file = @"C:\Users\colin.overton\Documents\AoC2025\day7input.txt")
        {
            IEnumerable<string> lines;

            lines = Utils.GetLines(@".......S.......
                                     ...............
                                     .......^.......
                                     ...............
                                     ......^.^......
                                     ...............
                                     .....^.^.^.....
                                     ...............
                                     ....^.^...^....
                                     ...............
                                     ...^.^...^.^...
                                     ...............
                                     ..^...^.....^..
                                     ...............
                                     .^.^.^.^.^...^.
                                     ...............");

            lines = File.ReadLines(file);

            int splits = 0;
            var prevLine = new HashSet<int>();

            foreach (var line in lines)
            {
                var currLine = new HashSet<int>();

                for (int i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case 'S':
                        {
                            currLine.Add(i);
                            break;
                        }
                        case '^':
                        {
                            if (prevLine.Contains(i))
                            {
                                currLine.Add(i - 1);
                                currLine.Add(i + 1);
                                splits++;
                            }
                            break;
                        }
                        case '.':
                        {
                            if (prevLine.Contains(i))
                            {
                                currLine.Add(i);
                            }
                            break;
                        }
                    }
                }

                prevLine = currLine;
            }

            Debug.WriteLine("Day7:" + splits + " splits");

            return splits;
        }

        public long Analyse2(string file = @"C:\Users\colin.overton\Documents\AoC2025\day7input.txt")
        {
            IEnumerable<string> lines;

            lines = Utils.GetLines(@".......S.......
                                     ...............
                                     .......^.......
                                     ...............
                                     ......^.^......
                                     ...............
                                     .....^.^.^.....
                                     ...............
                                     ....^.^...^....
                                     ...............
                                     ...^.^...^.^...
                                     ...............
                                     ..^...^.....^..
                                     ...............
                                     .^.^.^.^.^...^.
                                     ...............");

            lines = File.ReadLines(file);

            AllLines = lines.Where(line => line.Any(c => c != '.')).ToArray();
            KnownTimeLines = AllLines.Select(line => Enumerable.Repeat(-1L, line.Length).ToArray()).ToArray();

            var start = AllLines[0].IndexOf('S');

            var timelines = FollowBeam(1, start);

            Debug.WriteLine("Day7:" + timelines + " timelines");

            return timelines;
        }

        private string[] AllLines;
        private long[][] KnownTimeLines;

        private long FollowBeam(int depth, int position)
        {
            long timelines = KnownTimeLines[depth][position];
            if (timelines != -1)
            {
                return timelines;
            }

            var atBottom = depth == AllLines.Length - 1;

            var line = AllLines[depth];

            if (line[position] == '^')
            {
                if (atBottom)
                {
                    timelines = 2;
                }
                else
                {
                    var left = FollowBeam(depth + 1, position - 1);
                    var right = FollowBeam(depth + 1, position + 1);
                    timelines = left + right;
                }
            }
            else
            {
                if (atBottom)
                {
                    timelines = 1;
                }
                else
                {
                    timelines = FollowBeam(depth + 1, position);
                }
            }

            KnownTimeLines[depth][position] = timelines;
            return timelines;
        }

        public long Analyse3(string file = @"C:\Users\colin.overton\Documents\AoC2025\day7input.txt")
        {
            IEnumerable<string> lines;

            lines = Utils.GetLines(@".......S.......
                                     ...............
                                     .......^.......
                                     ...............
                                     ......^.^......
                                     ...............
                                     .....^.^.^.....
                                     ...............
                                     ....^.^...^....
                                     ...............
                                     ...^.^...^.^...
                                     ...............
                                     ..^...^.....^..
                                     ...............
                                     .^.^.^.^.^...^.
                                     ...............");

            lines = File.ReadLines(file);

            var allLines = lines.Where(line => line.Any(c => c != '.'));

            long[] prevLine = null;

            foreach (var line in allLines)
            {
                long[] currLine;
                if (prevLine == null)
                {
                    currLine = new long[line.Length];
                    var idx = line.IndexOf('S');
                    currLine[idx] = 1;
                }
                else
                {
                    currLine = new long[prevLine.Length];

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '^')
                        {
                            currLine[i - 1] += prevLine[i];
                            currLine[i + 1] += prevLine[i];
                        }
                        else
                        {
                            currLine[i] += prevLine[i];
                        }
                    }
                }

                prevLine = currLine;
            }

            var total = prevLine.Sum();

            Debug.WriteLine("Day7:" + total + " total");

            return total;
        }
    }
}
