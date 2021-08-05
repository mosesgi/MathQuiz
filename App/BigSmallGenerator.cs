using System;
using System.Collections.Generic;
using System.Text;

namespace MathQuiz
{
    public class BigSmallGenerator
    {
        private Random r = new Random();
        public List<string> Generate(int size)
        {
            List<string> result = new List<string>();
            GenerateSet(2, 10, size, result);
            GenerateSet(11, 20, size, result);
            return result;
        }

        private void GenerateSet(int low, int high, int size, List<string> result)
        {
            HashSet<string> set = new HashSet<string>();
            for (int i = 0; i < size / 2; )
            {
                int first = r.Next(low, high);
                int second = r.Next(low, high);
                if (first == 0 || second == 0 || first == second)
                {
                    continue;
                }
                if (Math.Abs(first - second) < 3)
                {
                    continue;
                }

                string key = $"{first}__{second}";
                if (set.Contains(key))
                {
                    continue;
                }
                set.Add(key);
                result.Add($"{first,2} ___ {second,2}\t\t\t\t\t\t");
                i++;
            }
        }
    }
}
