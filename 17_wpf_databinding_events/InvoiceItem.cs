using System;
using System.ComponentModel;

namespace WPF_Databinding_Events
{
  public class InvoiceItem : INotifyPropertyChanged

  {
    public Product Product { get; set; }


    public int _quantity;
    public int Quantity
    {
      get
      {
        return _quantity;
      }
      set
      {
        _quantity = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
        //Alternativ:
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Total"));
      }
    }
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
