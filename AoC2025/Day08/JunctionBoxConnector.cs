using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class JunctionBoxConnector
    {
        [DebuggerDisplay("{x},{y},{z}")]
        class Point
        {
            public long x;
            public long y;
            public long z;
            public Point(long x, long y, long z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        static double Distance(Point p1, Point p2)
        {
            var x = p1.x - p2.x;
            var y = p1.y - p2.y;
            var z = p1.z - p2.z;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public long Electrify(string file = @"C:\Users\colin.overton\Documents\AoC2025\day8input.txt")
        {
            IEnumerable<string> lines;

            lines = Utils.GetLines(@"162,817,812
                                     57,618,57
                                     906,360,560
                                     592,479,940
                                     352,342,300
                                     466,668,158
                                     542,29,236
                                     431,825,988
                                     739,650,466
                                     52,470,668
                                     216,146,977
                                     819,987,18
                                     117,168,530
                                     805,96,715
                                     346,949,466
                                     970,615,88
                                     941,993,340
                                     862,61,35
                                     984,92,344
                                     425,690,689");

            lines = File.ReadLines(file);

            var points = lines.Select(l =>
            {
                var parts = l.Split(',');
                return new Point(Int32.Parse(parts[0]), Int32.Parse(parts[1]), Int32.Parse(parts[2]));
            }).ToArray();

            var pointsWithDist = from idx1 in Enumerable.Range(0, points.Length)
                                 from idx2 in Enumerable.Range(idx1 + 1, points.Length - (idx1 + 1))
                                 select (p1: points[idx1], p2: points[idx2], dist: Distance(points[idx1], points[idx2]));

            //int count = 10;
            //int count = 1000;

            var orderedPoints = pointsWithDist.OrderBy(tup => tup.dist);
                                              //.Take(count);

            var clusters = new List<HashSet<Point>>();
            foreach (var (pt1, pt2, _) in orderedPoints)
            {
                var c1 = clusters.FirstOrDefault(c => c.Contains(pt1));
                var c2 = clusters.FirstOrDefault(c => c.Contains(pt2));

                if (c1 == null && c2 == null)
                {
                    clusters.Add(new HashSet<Point>(new[] { pt1, pt2 }));
                }
                else if (c1 == c2)
                {
                    //already connected
                }
                else if (c1 != null && c2 != null)
                {
                    c1.UnionWith(c2);
                    clusters.Remove(c2);

                    if (clusters.Count == 1 && c1.Count == points.Length)
                    {
                        var finalX = pt1.x * pt2.x;
                        Debug.WriteLine("Day8:" + finalX + " finalX");
                        return finalX;
                    }
                }
                else if (c1 == null)
                {
                    c2.Add(pt1);
                }
                else
                {
                    c1.Add(pt2);
                }
            }

            var total = clusters.OrderByDescending(c => c.Count)
                                .Take(3)
                                .Aggregate(1L, (a, c) => a * c.Count);
            
            Debug.WriteLine("Day8:" + total + " total");

            return total;
        }
    }
}
