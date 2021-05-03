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

namespace _22_fragen
{

  public class User
  {

    public int ID { get; set; }

    public string Name { get; set; }

  }
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      User alice = new User() { ID = 1, Name = "Alice" };
      User bob = new User() { ID = 2, Name = "Bob" };
      List<User> users = new List<User>
      {
        alice,
        bob
      };
      DgMain.DataContext = users;
    }

    private void BtSelect_Click(object sender, RoutedEventArgs e)
    {
      object o = DgMain.SelectedItem;
      if(o != null)
      {
        User user = (User)o;
        LbMessage.Content = $"UserID: {user.ID}";

      }
    }
  }
}
