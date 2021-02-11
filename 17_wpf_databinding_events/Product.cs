using System.ComponentModel;

namespace WPF_Databinding_Events
{
  public class Product : INotifyPropertyChanged
  {
    public int ID { get; }

    private string _Name;

    public string Name
    {
      get
      {
        return _Name;
      }
      set
      {
        _Name = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
      }
    }
    private string _Desc;

    public string Desc
    {
      get
      {
        return _Desc;
      }
      set
      {
        _Desc = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Desc"));
      }

    }

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
