using System.Collections.Generic;
using System.Windows;

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
      else
      {
        LbMessage.Content = "Kein Nutzer ausgewählt";
      }
    }
  }
}
