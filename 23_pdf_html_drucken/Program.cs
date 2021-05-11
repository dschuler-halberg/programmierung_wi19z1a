using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23_pdf_html_drucken
{
  class Program
  {
    static void Main(string[] args)
    {
      // PDF mit itext erstellen
      string filename = "create_pdf_itext.pdf";
      CreatePdf.CreatePDF(filename);

      // HTML zu PDF
      HtmlToPdf.HtmlToPdfItext();
      HtmlToPdf.HtmlToPdfWeasyPrint();

      // Drucken
      Print.DisplayPrinterNames();
      string printer = "Microsoft Print to PDF";
      Print.PrintPDF(printer, filename);

    }
  }
}
