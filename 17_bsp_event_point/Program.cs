using System;
using System.ComponentModel;

namespace _17_bsp_event_point
{
  class Program
  {
    static void Main(string[] args)
    {
      MyPoint p = new MyPoint() { X = 3, Y = 1 };
      p.PropertyChanged += HandlePointChanged;
      p.X = 5;
      p.Y = 2;
      Console.ReadLine();
    }

    public static void HandlePointChanged(object sender, EventArgs eventArgs)
    {
      PropertyChangedEventArgs pcea = (PropertyChangedEventArgs)eventArgs;
      Console.WriteLine($"Der {pcea.PropertyName}-Wert des Punkts hat sich geändert");
      Console.WriteLine("Argumente:");
      Console.WriteLine($"\tsender ");
      Console.WriteLine($"\t\tTyp: {sender.GetType()}");
      Console.WriteLine($"\t\tToString: {sender}");
      Console.WriteLine($"\teventArgs");
      Console.WriteLine($"\t\tToString: {eventArgs}");
      Console.WriteLine($"\t\tPropertyName: {pcea.PropertyName}");
    }
  }
}
