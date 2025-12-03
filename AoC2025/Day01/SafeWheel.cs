using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class SafeWheel
    {
        public int Position = 50;
        public int Size = 100;
        public int ZeroCount;

        public void Solve(string file = @"C:\Users\colin.overton\Documents\AoC2025\day1input.txt")
        {
            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var dir = line[0] == 'L' ? -1 : 1;
                var sz = int.Parse(line.Substring(1));

                //stoopid reference version
                //for (int i = 0; i < sz; i++)
                //{
                //    Position += dir;
                //    if (Position == Size)
                //    {
                //        Position = 0;
                //    }
                //    else if (Position == -1)
                //    {
                //        Position = Size - 1;
                //    }

                //    if (Position == 0)
                //    {
                //        ZeroCount++;
                //    }
                //}


                var fullRotations = sz / Size;
                var movement = sz % Size;

                var startPos = Position;

                ZeroCount += fullRotations;

                Position += movement * dir;

                if (Position < 0 || Position >= Size)
                {
                    //get back into range
                    Position += Size * -dir;

                    //if we didn't go over the zero
                    //we will count it as the end point
                    if (startPos != 0 && Position != 0)
                    {
                        //we did go over, it counts
                        ZeroCount++;
                    }
                }

                if (Position == 0)
                {
                    ZeroCount++;
                }
            }
        }
    }
}
