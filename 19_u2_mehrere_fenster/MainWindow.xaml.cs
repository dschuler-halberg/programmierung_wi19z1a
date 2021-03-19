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
using System.Diagnostics;

namespace _19_u2_mehrere_fenster
{
  /// <summary>
  /// Interaktionslogik für MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private Window w2 = new Window();

    public MainWindow()
    {
      InitializeComponent();
    }


    private void ProgBeenden_Click(object sender, RoutedEventArgs e)
    {
      Environment.Exit(0);
    }

    private void Rechner_Click(object sender, RoutedEventArgs e)
    {
      Process p = Process.Start("calc.exe");
    }

    private void NotePad_Click(object sender, RoutedEventArgs e)
    {
      Process p = Process.Start("notepad.exe");
    }

    private void SQLSMS_Click(object sender, RoutedEventArgs e)
    {
      Process p = Process.Start(@"C:\Program Files (x86)\Microsoft SQL Server\120\Tools\Binn\ManagementStudio\Ssms.exe" );
    }

    private void Fenster1_Click(object sender, RoutedEventArgs e)
    {
      Label l = new Label();
      l.Content = FensterName.Text;

      Window w = new Window();
      w.Title = FensterName.Text;
      w.Content = l;
      w.Height = 300;
      w.Width = 400;
      w.Show();
    }

    private void Dialog1_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show(FensterName.Text, FensterName.Text);
    }

    private void Fenster2_Click(object sender, RoutedEventArgs e)
    {
      if (!w2.IsVisible)
      {
        Label l = new Label();
        l.Content = "Zwei";
        w2.Content = l;
        w2.Height = 200;
        w2.Width = 300;
        w2.Show();
      }
      else
      {
        w2.Focus();
      }
    }

    private void Blau_Click(object sender, RoutedEventArgs e)
    {
      if (this.Background != Brushes.Blue)
      {
        this.Background = Brushes.Blue;
      }
      else
      {
        this.Background = Brushes.White;
      }
    }

    private void Button300_Click(object sender, RoutedEventArgs e)
    {
      this.Width = 300;
      this.Height = 300;
    }

    private void Ueberraschung_Click(object sender, RoutedEventArgs e)
    {
      Fenster1.Foreground = Brushes.DarkCyan;
      Dialog1.Foreground = Brushes.Orange;
      Fenster2.Foreground = Brushes.MediumPurple;
      Blau.Foreground = Brushes.Blue;
      Button300.Foreground = Brushes.Green;
      Ueberraschung.Foreground = Brushes.HotPink;
    }
  }
}