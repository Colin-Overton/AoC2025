using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class RollLocator
    {

        const int lookAround = 1;
        const int maxMeighbours = 4;
        const bool removeRolls = true;

        char[][] lines;
        int lineLen => lines[0].Length;

        public int FindEm(string fileName = @"C:\Users\colin.overton\Documents\AoC2025\day4input.txt")
        {
            string[] lineStrings;

//              test data
//            lineStrings = @"..@@.@@@@.
//@@@.@.@.@@
//@@@@@.@.@@
//@.@@@@..@.
//@@.@@@@.@@
//.@@@@@@@.@
//.@.@.@.@@@
//@.@@@.@@@@
//.@@@@@@@@.
//@.@.@@@.@.".Split('\n')
// .Select(s => s.Trim())
// .Where(s => !String.IsNullOrEmpty(s))
// .ToArray();

            lineStrings = File.ReadAllLines(fileName);

            lines = lineStrings.Select(s => s.ToArray()).ToArray();

            int count = 0;

            int removed;
            do
            {
                removed = CountAvailableRolls();
                count += removed;
            } while (removeRolls && removed > 0);
           

            Debug.WriteLine("Day4:" + count + " count");
            return count;
        }

        private int CountAvailableRolls()
        {
            int count = 0;

            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < lineLen; c++)
                {
                    if (lines[r][c] == '@')
                    {
                        var n = CountHeighbours(c, r);
                        if (n < maxMeighbours)
                        {
                            count++;
                            if (removeRolls)
                            {
                                lines[r][c] = '.';
                            }
                        }
                    }
                }
            }

            return count;
        }

        private int CountHeighbours(int col, int row)
        {
            int count = 0;
            for (int r = row - lookAround; r <= row + lookAround; r++)
            {
                for (int c = col - lookAround; c <= col + lookAround; c++)
                {
                    if (r == row && c == col)
                    {
                        //found ourselves
                    }
                    else if (r < 0 || r >= lines.Length ||
                             c < 0 || c >= lineLen)
                    {
                        //outside bounds, so nothing there
                    }
                    else if (lines[r][c] == '@')
                    {
                        //roll near us
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
