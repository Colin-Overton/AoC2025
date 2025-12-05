using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025
{
    internal class Utils
    {
        public static IEnumerable<string> GetLines(string str)
        {
            return str.Split('\n')
                      .Select(s => s.Trim());    
        }
    }
}
