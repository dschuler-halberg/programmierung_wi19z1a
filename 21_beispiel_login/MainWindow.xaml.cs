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

namespace _21_beispiel_login
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    //private string _username;

    //public string Username { get { return _username; } set {
    //    _username = value;
    //    LbMain.Content = $"Hallo {value}";
    //  }
    //}

    public string Username { get; set; }

    public MainWindow(string username)
    {
      InitializeComponent();
      Username = username;
      LbMain.Content = $"Hallo {Username}!";
    }
  }
}
