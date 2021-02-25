using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace _18_uebung2_kategorien_produkte
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      AdventureWorksContext ctx = new AdventureWorksContext();
      ctx.ProductCategory.Load();
      ParentGrid.DataContext = ctx.ProductCategory.Local;
    }

  }
}
