using System;
using System.Collections.Generic;

namespace MathQuiz
{
    public class Program
    {
        const int DefaultBigSmallNum = 12;
        const int DefaultPlusMinusNum = 28;

        public static void Main(string[] args)
        {
            if (DocxHelper.CheckDocOpen())
            {
                Console.Out.Write("文件已打开，请关闭后再次运行。");
                Console.In.Read();
                return;
            }
            Console.Out.WriteLine("一页共有40题");
            Console.Out.Write("请输入大于小于题目数（默认12题）:");
            if (!ConsoleUtil.ReadInt(out int bigSmallNum))
            {
                bigSmallNum = DefaultBigSmallNum;
            }
            Console.Out.Write("请输入加减法题目数（默认28题）:");
            if (!ConsoleUtil.ReadInt(out int plusMinusNum))
            {
                plusMinusNum = DefaultPlusMinusNum;
            }

            List<string> result = new List<string>();
            result.AddRange(new BigSmallGenerator().Generate(bigSmallNum));
            result.AddRange(new PlusMinusGenerator().Generate(plusMinusNum));

            DocxHelper.OutputToDoc(result);
        }

    }
}
