using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace _17_u3_kassensystem
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private static Invoice Cart = new Invoice();


    public List<Product> Stock = new List<Product>();


    public MainWindow()
    {
      InitializeComponent();
      Product bananas = new Product(1)
      {
        Name = "Bananen",
        Desc = "Bananen aus Brasilien (1 kg)",
        Price = 1.99m
      };
      Product apples = new Product(2)
      {
        Name = "Äpfel",
        Desc = "Äpfel aus Österreich (1 kg)",
        Price = 2.99m
      };
      Product potatoes = new Product(3)
      {
        Name = "Kartoffeln",
        Desc = "Kartoffeln aus der Pfalz (1kg)",
        Price = 1.49m
      };
      Product cheese = new Product(4)
      {
        Name = "Käse",
        Desc = "Käse aus der Schweiz (1kg)",
        Price = 9.49m
      };

      Stock.Add(bananas);
      Stock.Add(cheese);
      Stock.Add(potatoes);

      Sortiment.DataContext = Stock;

      //Warenkorb.ItemsSource = Cart.Items;
      MainGrid.DataContext = Cart;
    }



    private void Sortiment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      bool present = false;
      var selected = Sortiment.SelectedItem;
      if (selected != null)
      {
        Product selectedProduct = (Product)selected;
        foreach (InvoiceItem i in Cart.Items)
        {
          if (selectedProduct == i.Product)
          {
            i.Quantity++;
            present = true;
          }
        }
        if (!present)
        {
          InvoiceItem newi = new InvoiceItem(selectedProduct, 1);
          Cart.Items.Add(newi);
        }
      }
    }


    private void Warenkorb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Warenkorb.SelectedIndex >= 0 && Warenkorb.SelectedIndex < Cart.Items.Count)
      {
        if (Cart.Items[Warenkorb.SelectedIndex].Quantity == 1)
        {
          Cart.Items.RemoveAt(Warenkorb.SelectedIndex);
        }
        else
        {
          Cart.Items[Warenkorb.SelectedIndex].Quantity--;
        }
      }
    }

    private void Neuberechnung_Click(object sender, RoutedEventArgs e)
    {
      Binding TotalBinding = new Binding("")
      {
        Source = Cart.Total
      };
      FinalTotal.SetBinding(Label.ContentProperty, TotalBinding);
    }

    //private void Warenkorb_SourceUpdated()
    //{
    //  Warenkorb.ItemsSource = null;
    //  Warenkorb.ItemsSource = Cart.Items;
    //}
  }
}
