using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPF_Databinding_Events
{
  public partial class MainWindow : Window
  {

    private Invoice ExampleInvoice;

    private Product Product1;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      ExampleInvoice = Invoice.GetExampleInvoice();
      Product1 = ExampleInvoice.Items[0].Product;

      GridAutoUpdate.DataContext = Product1;

      GridChangeInCode.DataContext = ExampleInvoice;

      GridNestedObjects.DataContext = ExampleInvoice;

      GridObjectEnumerable.DataContext = ExampleInvoice;

      GridInvoice.DataContext = ExampleInvoice;
      List<Product> allProducts = ExampleInvoice.Items.Select(x => x.Product).ToList();
      DgProducts.DataContext = allProducts;
    }

    private void DgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (DgProducts.SelectedItem != null)
      {
        Product p = (Product) DgProducts.SelectedItem;
        ExampleInvoice.AddProduct(p);
      }
    }

    private void BtRandomId_Click(object sender, RoutedEventArgs e)
    {
      ExampleInvoice.InvoiceID = new Random().Next(1, 100000);
      ExampleInvoice.Date = DateTime.Now.AddDays(new Random().Next(-1000, 1000));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show($"Gesamtpreis: {ExampleInvoice.Total}");
    }
  }
}
