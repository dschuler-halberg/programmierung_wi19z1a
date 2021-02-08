using System.ComponentModel;

namespace _17_bsp_event_point
{
  public class MyPoint : INotifyPropertyChanged
  {
    private double _x;
    public double X
    {
      get { return _x; }
      set
      {
        _x = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
      }
    }

    private double _y;
    public double Y
    {
      get { return _y; }
      set
      {
        _y = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public override string ToString()
    {
      return $"({X}|{Y})";
    }
  }
}