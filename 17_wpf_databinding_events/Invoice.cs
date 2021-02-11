using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace WPF_Databinding_Events
{
  public class Invoice : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public int _invoiceID;

    public int InvoiceID
    {
      get { return _invoiceID; }
      set
      {
        _invoiceID = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InvoiceID"));
      }
    }
    public DateTime Date { get; set; }

    public Customer Customer { get; set; }

    public IList<InvoiceItem> Items { get; set; }

    public decimal Total
    {
      get
      {
        return Items.Sum(x => x.Total);
      }
    }


    public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Total"));

      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (InvoiceItem item in e.OldItems)
        {
          item.PropertyChanged -= InvoiceItemPropertyChanged;
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (InvoiceItem item in e.NewItems)
        {
          item.PropertyChanged += InvoiceItemPropertyChanged;
        }
      }
    }

    public void InvoiceItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
    }

    public Invoice()
    {
      ObservableCollection<InvoiceItem> observableItems = new ObservableCollection<InvoiceItem>();
      observableItems.CollectionChanged += OnCollectionChanged;
      //Items = new List<InvoiceItem>();
      Items = observableItems;
    }

    public void AddProduct(Product product)
    {
      InvoiceItem ii = new InvoiceItem(product, 1);
      ii.PropertyChanged += InvoiceItemPropertyChanged;
      Items.Add(ii);
    }

    public void RemoveProduct(InvoiceItem ii)
    {
      ii.PropertyChanged -= InvoiceItemPropertyChanged;
      Items.Remove(ii);
    }

    public static Invoice GetExampleInvoice()
    {
      Address a = new Address()
      {
        Street = "Mainzer Str. 123",
        City = "Saarbrücken"
      };

      Customer c = new Customer()
      {
        Name = "Alice",
        Address = a
      };

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
      InvoiceItem i1 = new InvoiceItem(bananas, 4);
      InvoiceItem i2 = new InvoiceItem(apples, 2);
      InvoiceItem i3 = new InvoiceItem(potatoes, 4);

      Invoice ExampleInvoice = new Invoice()
      {
        InvoiceID = 1123,
        Customer = c,
        Date = DateTime.Now,
      };

      //i1.PropertyChanged += ExampleInvoice.InvoiceItemPropertyChanged;
      //i2.PropertyChanged += ExampleInvoice.InvoiceItemPropertyChanged;
      //i3.PropertyChanged += ExampleInvoice.InvoiceItemPropertyChanged;

      ExampleInvoice.Items.Add(i1);
      ExampleInvoice.Items.Add(i2);
      ExampleInvoice.Items.Add(i3);
      return ExampleInvoice;
    }

  }
}
