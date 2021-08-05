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
                    new PageSize() { Width = 11907U, Height = 16839U, Orient = PageOrientationValues.Portrait }
                ));
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
