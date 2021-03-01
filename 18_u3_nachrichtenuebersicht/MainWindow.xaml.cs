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

namespace _18_u3_nachrichtenuebersicht
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>


  public partial class MainWindow : Window
  {
    private ICollectionView CollectionView;

    private MessengerContext Context = new MessengerContext();

    private List<Nachricht> SearchResults;



    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Context.Nachrichten.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Nachrichten.Local);
      ParentGrid.DataContext = CollectionView;
    }


    public MainWindow()
    {
      InitializeComponent();


      //  Nachricht current = (Nachricht)CollectionView.CurrentItem;
      //  List<Nachricht> nachrichten = Context.Nachrichten.ToList();
      //  int indexOfCurrent = nachrichten.IndexOf(current);
      //  string searchString = "";
      ////  List<Nachricht> searchResults = Context.Nachrichten.Where(x => x.Text.Contains(searchString)).ToList();
      //  List<Nachricht> searchResults = Context.Nachrichten.Skip(indexOfCurrent).Where(x => x.Text.Contains(searchString)).ToList();
      //  //CollectionView.MoveCurrentTo();
    }

    private void weiter_Click(object sender, RoutedEventArgs e)
    {
      MoveToNextSearchResult();
    }

    private void MoveToNextSearchResult()
    {
      Nachricht current = (Nachricht)CollectionView.CurrentItem;
      int indexOfCurrent = SearchResults.IndexOf(current);
      var message = SearchResults.Skip(indexOfCurrent + 1).FirstOrDefault();
      if (message != null)
      {
        CollectionView.MoveCurrentTo(message);
      }
      else
      {
        CollectionView.MoveCurrentTo(SearchResults[0]);
      }
    }

    private void suche_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        MoveToNextSearchResult();
      }
      else
      {
        string searchString = suche.Text;
        SearchResults = Context.Nachrichten.Where(x => x.Text.Contains(searchString)).ToList();
        LbNumResults.Content = SearchResults.Count;
        if (SearchResults.Count > 0)
        {
          CollectionView.MoveCurrentTo(SearchResults[0]);
        }
      }
    }
  }
}
