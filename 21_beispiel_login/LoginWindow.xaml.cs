using System;
using System.Security.Cryptography;
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
using System.Windows.Shapes;

namespace _21_beispiel_login
{
  /// <summary>
  /// Interaction logic for LoginWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {

    private static readonly int numberOfIterations = 10000;

    public LoginWindow()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (CheckPassword())
      {
        MainWindow mainWindow = new MainWindow( TbUsername.Text);
       // mainWindow.Username =  TbUsername.Text;
        mainWindow.Show();
        this.Close();
      }
      else
      {
        LbMessage.Content = "Login fehlgeschlagen";
      }
    }

    private bool CheckPassword()
    {
      string username = TbUsername.Text;
      string password = PbPassword.Password;

      return CheckPassword(username, password);
    }


    private static bool CheckPassword(string userName, string password)
    {
      // Salt-Wert aus Datenbank auslesen
      string salt = GetSaltFromDB(userName);
      
      //Nutzername nicht gefunden
      if(salt == null)
      {
        return false;
      }

      // Umwandeln des Salt in byte-Array
      byte[] saltBytes = Convert.FromBase64String(salt);

      // Bestimmen des Passwort-Hash-Wert für eingegebenes Passwort
      Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes);
      // Werte müssen identisch zu den Werten beim Generieren des Passwortes sein
      rfc2898DeriveBytes.IterationCount = numberOfIterations;
      byte[] enteredHash = rfc2898DeriveBytes.GetBytes(20);
      // Umwandeln von byte-Array in String
      string str = Convert.ToBase64String(enteredHash);
      // Erwarteten Hash-Wert aus Datenbank auslesen
      string expectedHash = GetHashFromDB(userName);

      // Vergleichen der Hash-Werte (evtl. Sicherheitsrisiko)
      bool hashesMatch = str.Equals(expectedHash);
      // Testausgabe
      //Console.WriteLine($"Salt (aus DB):       {salt}");
      //Console.WriteLine($"Hash (aus DB):       {expectedHash}");
      //Console.WriteLine($"Hash (aus Eingabe):  {str}");
      //Console.WriteLine($"Hash Werte gleich:   {hashesMatch}");
      return hashesMatch;
    }

    private static string GetHashFromDB(string userName)
    {
      LoginContext ctx = new LoginContext();
      Login l = ctx.Logins.FirstOrDefault(x => x.Username == userName);
      if (l != null)
      {
        return l.Hash;
      }
      return null;
    }

    private static string GetSaltFromDB(string userName)
    {
      LoginContext ctx = new LoginContext();
      Login l = ctx.Logins.FirstOrDefault(x => x.Username == userName);
      if (l != null)
      {
        return l.Salt;
      }
      return null;
    }

  }
}
