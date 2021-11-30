using System;
using System.Collections.Generic;

namespace MathQuiz
{
    public class Program
    {
        const int DefaultBigSmallNum = 0;
        const int DefaultPlusMinusNum = 50;
        const int DefaultPlusMinus10Num = 50;

        public static void Main(string[] args)
        {
            if (DocxHelper.CheckDocOpen())
            {
                Console.Out.Write("文件已打开，请关闭后再次运行。");
                Console.In.Read();
                return;
            }
            Console.Out.WriteLine("一页共有100题");
            Console.Out.Write("请输入大于小于题目数（默认0题）:");
            if (!ConsoleUtil.ReadInt(out int bigSmallNum))
            {
                bigSmallNum = DefaultBigSmallNum;
            }
            Console.Out.Write("请输入20以内加减法题目数（默认50题）:");
            if (!ConsoleUtil.ReadInt(out int num20))
            {
                num20 = DefaultPlusMinusNum;
            }
            Console.Out.Write("请输入10以内加减法题目数（默认50题）:");
            if (!ConsoleUtil.ReadInt(out int num10))
            {
                num10 = DefaultPlusMinus10Num;
            }

            List<string> result = new List<string>();
            result.AddRange(new BigSmallGenerator().Generate(bigSmallNum));
            result.AddRange(new PlusMinus20Generator().Generate(num20));
            result.AddRange(new PlusMinus10Generator().Generate(num10));

            DocxHelper.OutputToDoc(result);
        }

    }
}
