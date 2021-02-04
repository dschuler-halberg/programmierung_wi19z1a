using bsp_ef_wpf;
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

namespace _16_u3_nutzerverwaltung
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private ICollectionView CollectionView;

    private MessengerContext Context = new MessengerContext();

    public MainWindow()
    {
      InitializeComponent();

      Context.Nutzer.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Nutzer.Local);
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
  }
}
