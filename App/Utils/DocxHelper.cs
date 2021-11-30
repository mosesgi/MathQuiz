using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathQuiz
{
    public class DocxHelper
    {
        const string DocName = "exercise.docx";
        public static void OutputToDoc(List<string> equations)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DocName);
            using WordprocessingDocument wordDoc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);

            MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body docBody = mainPart.Document.AppendChild(new Body());
            docBody.AppendChild(
                new SectionProperties(
                    new PageSize() { Width = 11907U, Height = 16839U, Orient = PageOrientationValues.Portrait },
                    new PageMargin() { Top = 1008, Right = 1008U, Bottom = 1008, Left = 1008U, Header = 720U, Footer = 720U, Gutter = 0U }
                ));
            for (int i = 0; i < equations.Count;)
            {
                var para = docBody.AppendChild(new Paragraph());
                var pp = new ParagraphProperties(new SpacingBetweenLines { Line = "380", LineRule = LineSpacingRuleValues.Auto, After = "0" });
                //var pp = new ParagraphProperties(new SpacingBetweenLines { Line = "240", LineRule = LineSpacingRuleValues.Auto, After = "0" });
                para.Append(pp);
                for (int j = 0; j < 4 && i < equations.Count; j++)
                {
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new RunProperties(
                        new RunFonts() { Ascii = "黑体", HighAnsi = "黑体", EastAsia = "黑体" },
                        new FontSize() { Val = "28" }
                        )
                    );
                    run.AppendChild(new Text(equations[i++]) { Space = SpaceProcessingModeValues.Preserve });
                    if (j < 3)
                    {
                        run.AppendChild(new Text("        ") { Space = SpaceProcessingModeValues.Preserve });
                    }
                }
            }
        }

        public static bool CheckDocOpen()
        {
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DocName);
            if (!File.Exists(fullPath))
            {
                return false;
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }
}
