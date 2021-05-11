using Balbarak.WeasyPrint;
using iText.Html2pdf;
using System.IO;

namespace _23_pdf_html_drucken
{
  class HtmlToPdf
  {

    public static string GetHtmlDocument(string name)
    {
      return $"<!DOCTYPE html><html><body><h1>Hallo {name}! </h1></body></html>";
    }

    public static void HtmlToPdfItext()
    {
      using (FileStream pdfDest = File.Open("html2pdf_itext.pdf", FileMode.OpenOrCreate))
      {
        string html = GetHtmlDocument("Bob");
        ConverterProperties converterProperties = new ConverterProperties();
        HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
      }
    }

    public static void HtmlToPdfWeasyPrint()
    {
      using (WeasyPrintClient client = new WeasyPrintClient())
      {
        string html = GetHtmlDocument("Alice");
        byte[] binaryPdf = client.GeneratePdf(html);
        File.WriteAllBytes("html2pdf_weasyprint.pdf", binaryPdf);
      }
    }
 
  }
}
