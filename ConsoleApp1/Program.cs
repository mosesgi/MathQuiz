using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MathQuiz
{
    public class Program
    {
        const int PageSize = 1;
        const int BigSmallPerPage = 12;
        const int PlusMinusPerPage = 28;
        const int Deduct20 = 2;
        public static void Main(string[] args)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "exercise.docx");
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(
                    new SectionProperties(
                        new PageSize() { Width = 11907U, Height = 16839U, Orient = PageOrientationValues.Portrait }
                    ));

                List<string> bigSmalls = GenerateBigSmall(PageSize * BigSmallPerPage);

                List<string> equations = GenerateEquations(PageSize * PlusMinusPerPage);
                OutputToDoc(docBody, bigSmalls);
                OutputToDoc(docBody, equations);
            }
        }

        private static void OutputToDoc(Body docBody, List<string> equations)
        {
            for (int i = 0; i < equations.Count;)
            {
                Paragraph line = docBody.AppendChild(new Paragraph());
                line.AppendChild(new ParagraphProperties(new SpacingBetweenLines { Before = "200", After = "200" }));
                for (int j = 0; j < 2 && i < equations.Count; j++)
                {
                    Run run = line.AppendChild(new Run());
                    run.AppendChild(new RunProperties(
                        new RunFonts() { Ascii = "黑体", HighAnsi = "黑体", EastAsia = "黑体" },
                        new FontSize() { Val = "36" }
                        )
                    );
                    run.AppendChild(new Text(equations[i++]) { Space = SpaceProcessingModeValues.Preserve });
                }
            }
        }

        private static List<string> GenerateBigSmall(int size)
        {
            List<string> result = new List<string>();
            HashSet<string> set = new HashSet<string>();
            Random r = new Random();

            int i = 0;
            while (i < size/2)
            {
                int first = r.Next(2, 10);
                int second = r.Next(2, 10);
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

            while (i < size)
            {
                int first = r.Next(11, 20);
                int second = r.Next(11, 20);
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
            return result;
        }

        static List<string> GenerateEquations(int size)
        {
            List<string> result = new List<string>();
            HashSet<string> set = new HashSet<string>();
            Random r = new Random();
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
