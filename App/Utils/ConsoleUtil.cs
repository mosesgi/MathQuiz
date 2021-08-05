using System;

namespace MathQuiz
{
    public class ConsoleUtil
    {
        public static bool ReadInt(out int result)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        result = 0;
                        return false;
                    }
                    result = Convert.ToInt32(input);
                    return true;
                }
                catch
                {
                    Console.WriteLine("输入有误，请重新输入");
                }
            }
        }
    }
}
