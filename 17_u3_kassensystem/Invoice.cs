using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;


namespace _17_u3_kassensystem
{

  public class Product
  {
    public int ID { get; }

    public string Name { get; set; }

    public string Desc { get; set; }

    public decimal Price { get; set; }

    public Product(int id)
    {
      ID = id;
    }

    public override string ToString()
    {
      return $"{ID} - {Name} - {Desc} - {Price}";
    }
  }


  public class InvoiceItem : INotifyPropertyChanged
  {
    public Product Product { get; set; }

    private int _quantity;
    public int Quantity
    {
      get
      { return _quantity; }
      set
      {
        _quantity = value;
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Total"));
        //oder

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
      }
    }

    public decimal Total
    {
      get
      {
        return Product.Price * Quantity;
      }
      set { }
    }

    public InvoiceItem(Product product, int quantity)
    {
      Product = product;
      Quantity = quantity;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public override string ToString()
    {
      return $"{Quantity} x {Product.Name} Preis: {Total} ";
    }
  }

  public class Address
  {
    public string Street { get; set; }
    public string City { get; set; }

  }

  public class Customer
  {
    public string Name { get; set; }

    public Address Address { get; set; }
  }



  public class Invoice : INotifyPropertyChanged
  {
    public int InvoiceID { get; set; }

    public DateTime Date { get; set; }

    public Customer Customer { get; set; }

    public ObservableCollection<InvoiceItem> Items { get; set; } = new ObservableCollection<InvoiceItem>();

    public decimal Total
    {
      get
      {
        return Items.Sum(x => x.Total);
      }
      set { }
    }

    public event PropertyChangedEventHandler PropertyChanged;


    public Invoice()
    {
      Items.CollectionChanged += OnCollectionChanged;
    }


    public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {

      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Total"));

      if (e.Action == NotifyCollectionChangedAction.Add)
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
        InvoiceID = 111231223,
        Customer = c,
        Date = DateTime.Now,
      };
      ExampleInvoice.Items.Add(i1);
      ExampleInvoice.Items.Add(i2);
      ExampleInvoice.Items.Add(i3);
      return ExampleInvoice;
    }


  }

}
