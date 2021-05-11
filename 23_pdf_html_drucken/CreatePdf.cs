using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace _23_pdf_html_drucken
{
  class CreatePdf
  {

    /// <summary>
    /// Page name: File:Macaca nigra self-portrait full body.jpg
    /// Author: Wikimedia Commons contributors
    /// Publisher: Wikimedia Commons, the free media repository.
    /// Date of last revision: 9 May 2021 23:32 UTC
    /// Date retrieved: 11 May 2021 08:53 UTC
    /// Permanent URL: https://commons.wikimedia.org/w/index.php?title=File:Macaca_nigra_self-portrait_full_body.jpg&oldid=559251780
    /// Page Version ID: 559251780
    /// </summary>
    public static string ImageFilename { get; } = "Macaca_nigra_self-portrait_full_body.jpg";


    public static void CreatePDF(string pdfFilename)
    {
      PdfWriter writer = new PdfWriter(pdfFilename);
      PdfDocument pdf = new PdfDocument(writer);
      Document document = new Document(pdf);
      int i = 121342 * 234;

      PdfFont font = PdfFontFactory.CreateFont(StandardFonts.SYMBOL);
      Paragraph p1 = new Paragraph($"Hallo Welt {i}!");

      Image monkey = new Image(ImageDataFactory.Create(ImageFilename));
      monkey.SetHeight(100);
      p1.SetFont(font);
      p1.SetFontSize(32);
      p1.SetTextAlignment(TextAlignment.RIGHT);
      p1.Add(monkey);
      document.Add(p1);
      document.Add(new Paragraph($"Hallo Welt {i}!"));
      document.Close();
    }
  }
}

