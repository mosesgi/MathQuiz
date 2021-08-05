using System;
using System.Collections.Generic;
using System.Text;

namespace MathQuiz
{
    public class PlusMinusGenerator
    {
        const int Deduct20 = 2;
        public List<string> Generate(int size)
        {
            Random r = new Random();
            List<string> result = new List<string>();
            HashSet<string> set = new HashSet<string>();
            int deduct20 = 0;

            int i = 0;
            while (i < size)
            {
                int first = r.Next(21);
                int second = r.Next(21);
                int plusOrMinus = r.Next(2);
                bool plus = plusOrMinus == 0;
                if (first == 0 || second == 0)
                {
                    continue;
                }
                if (plus && (first == 10 || second == 10 || first + second <= 10 || first + second > 20 || first < 3 || second < 3))
                {
                    continue;
                }
                if (!plus && (first <= 10 || first - second <= 1 || first - second == 10 || second == 10 || second < 2))
                {
                    continue;
                }
                if (!plus && first == 20)
                {
                    if (deduct20 >= Deduct20)
                    {
                        continue;
                    }
                    deduct20++;
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
                result.Add($"{first,2} {(plus ? "+" : "-")} {second,2} =\t\t\t\t\t\t");
                i++;
            }
            return result;
        }
    }
}
