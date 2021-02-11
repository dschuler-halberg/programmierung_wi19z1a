using System.ComponentModel;

namespace WPF_Databinding_Events
{
  public class Product : INotifyPropertyChanged
  {
    public int ID { get; }

    public string Name { get; set; }

    public string Desc { get; set; }

    private decimal _price;

    public decimal Price
    {
      get
      {
        return _price;
      }
      set
      {
        _price = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
      }
    }

    public Product(int id)
    {
      ID = id;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public override string ToString()
    {
      return $"{ID} - {Name} - {Desc} - {Price}";
    }
  }
}
