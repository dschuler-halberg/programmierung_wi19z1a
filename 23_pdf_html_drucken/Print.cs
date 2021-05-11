using PdfiumViewer;
using System;
using System.Drawing.Printing;

namespace _23_pdf_html_drucken 
{
  public class Print
  {

    /// <summary>
    /// Display names for all printers that are installed on the system.
    /// </summary>
    public static void DisplayPrinterNames()
    {
      foreach (string printer in PrinterSettings.InstalledPrinters)
      {
        Console.WriteLine(printer);
      }
    }

    /// <summary>
    /// Prints a PDF document on the specified printer.
    /// </summary>
    /// <param name="printer">Name of the printer </param>
    /// <param name="filename">Location of the PDF document to print</param>
    /// <returns>True, if printing was successful and false else. </returns>
    public static bool PrintPDF(string printer, string filename)
    {
      short copies = 1;
      string paperName = "A4";
      try
      {
        // Create the printer settings for our printer
        var printerSettings = new PrinterSettings
        {
          PrinterName = printer,
          Copies = (short)copies,
        };

        // Create our page settings for the paper size selected
        var pageSettings = new PageSettings(printerSettings)
        {
          Margins = new Margins(0, 0, 0, 0),
        };
        foreach (PaperSize paperSize in printerSettings.PaperSizes)
        {
          if (paperSize.PaperName == paperName)
          {
            pageSettings.PaperSize = paperSize;
            break;
          }
        }

        // Now print the PDF document
        using (PdfDocument document = PdfDocument.Load(filename))
        {
          using (PrintDocument printDocument = document.CreatePrintDocument())
          {
            printDocument.PrinterSettings = printerSettings;
            printDocument.DefaultPageSettings = pageSettings;
            printDocument.PrintController = new StandardPrintController();
            printDocument.Print();
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

  }
}
