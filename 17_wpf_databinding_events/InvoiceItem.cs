using System;
using System.ComponentModel;

namespace WPF_Databinding_Events
{
  public class InvoiceItem : INotifyPropertyChanged

  {
    public Product Product { get; set; }
   
    public int Quantity { get; set; }

    public decimal Total {
      get {
        return Product.Price * Quantity;
      }
    }

    public InvoiceItem(Product p, int quantity)
    {
      Product = p;
      p.PropertyChanged += ProductChanged;
      Quantity = quantity;
    }

    private void ProductChanged(object sender, PropertyChangedEventArgs e)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public override string ToString()
    {
      return $"{Quantity} x {Product.Name} Preis: {Total} ";
    }
  }
}
