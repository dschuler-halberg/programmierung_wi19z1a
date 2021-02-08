using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace _16_u4_Produktverwaltung
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>

  public partial class MainWindow : Window
  {

    private ICollectionView CollectionView;

    private AdventureWorksContext Context = new AdventureWorksContext();

    public MainWindow()
    {
      InitializeComponent();

      Context.Products.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Products.Local);
      ParentGrid.DataContext = CollectionView;

    }

    private void BtZurueck_Click(object sender, RoutedEventArgs e)
    {
      CollectionView.MoveCurrentToPrevious();
      if (CollectionView.IsCurrentBeforeFirst)
      {
        //CollectionView.MoveCurrentToFirst();
        CollectionView.MoveCurrentToLast();
      }

    }

    private void BtVor_Click(object sender, RoutedEventArgs e)
    {
      // Navigieren zum nächsten Element
      CollectionView.MoveCurrentToNext();
      // Falls das Ende überschritten wurde, wird zurück auf das letzte Element gewechselt.
      if (CollectionView.IsCurrentAfterLast)
      {
        //CollectionView.MoveCurrentToLast();
        CollectionView.MoveCurrentToFirst();
      }

    }

    private void BtSave_Click(object sender, RoutedEventArgs e)
    {
      Context.SaveChanges();
    }

    private void TbID_KeyUp(object sender, KeyEventArgs e)
    {
      if(e.Key == Key.Tab)
      {
        return;
      }
      string searchstring = TbID.Text;
      var list = CollectionView.Cast<Product>();
      Product p = list.FirstOrDefault(x => x.ProductID.ToString().Contains(searchstring));
      CollectionView.MoveCurrentTo(p);
    }

    private void TbPosition_KeyUp(object sender, KeyEventArgs e)
    {
      string searchstring = TbPosition.Text;
      bool isNumber = Int32.TryParse(searchstring, out int pos);
      if (isNumber)
      {
        // Da Indexierung bei 0 beginnt:
        pos -= 1;
        if (pos < Context.Products.Count() && pos >= 0)
        {
          CollectionView.MoveCurrentToPosition(pos);
        }
      }
    }
  }




}
