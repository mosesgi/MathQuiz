using System;
using System.Collections.Generic;
using System.Text;

namespace MathQuiz
{
    public class PlusMinus10Generator
    {
        //const int Deduct20 = 2;
        public List<string> Generate(int size)
        {
            Random r = new Random();
            List<string> result = new List<string>();
            HashSet<string> set = new HashSet<string>();

            int i = 0;
            while (i < size)
            {
                int first = r.Next(11);
                int second = r.Next(11);
                int plusOrMinus = r.Next(2);
                bool plus = plusOrMinus == 0;
                if (plus && (first == 10 || second == 10 || first + second > 10 ))
                {
                    continue;
                }
                if (!plus && (first - second < 0))
                {
                    continue;
                }

                string key;
                if (plus && first > second)
                {
                    key = $"{second}_{plusOrMinus}_{first}";
                }
                else
                {
                    key = $"{first}_{plusOrMinus}_{second}";
                }
                if (set.Contains(key))
                {
                    continue;
                }
                set.Add(key);
                result.Add($"{first,2} {(plus ? "+" : "-")} {second,2} =");
                i++;
            }
            return result;
        }
    }
}
