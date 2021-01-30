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

using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Data.Entity;
using System.ComponentModel;


namespace bsp_ef_wpf
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
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Context.Nachrichten.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Nachrichten.Local);
      int count = 1;
      foreach (var i in CollectionView)
      {
        Console.WriteLine($"Window_Loaded {count}: {i} - {i.GetType()}");
        count++;
      }
      //CollectionView.Filter = (x => ((Nachricht)x).ID_Nachricht < 20);
      ParentGrid.DataContext = CollectionView;

    }

    private void BtForward_Click(object sender, RoutedEventArgs e)
    {
      int count = 1;
      foreach (var i in CollectionView)
      {
        Console.WriteLine($"BtForward_Click {count}: {i}  - {i.GetType()}");
        count++;
      }

      // Navigieren zum nächsten Element
      CollectionView.MoveCurrentToNext();
      // Falls das Ende überschritten wurde, wird zurück auf das letzte Element gewechselt.
      if (CollectionView.IsCurrentAfterLast)
      {
        //CollectionView.MoveCurrentToLast();
        CollectionView.MoveCurrentToFirst();
      }
    }

    private void BtBack_Click(object sender, RoutedEventArgs e)
    {
      CollectionView.MoveCurrentToPrevious();
      if (CollectionView.IsCurrentBeforeFirst)
      {
        //CollectionView.MoveCurrentToFirst();
        CollectionView.MoveCurrentToLast();
      }
    }

    private void BtSave_Click(object sender, RoutedEventArgs e)
    {
      Context.SaveChanges();
    }

    private void TbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      string searchStr = TbSearch.Text.ToLower();
      // Zurücksetzen des Filters
      CollectionView.Filter = null;
      foreach (var i in CollectionView)
      {
        Console.WriteLine(i);
      }

      Func<Nachricht, bool> checkLambda = (x => x.Text.ToLower().Contains(searchStr)
                  || x.NutzerSender.Name.ToLower().Contains(searchStr)
                  || x.NutzerEmpfaenger.Any(y => y.Name.ToLower().Contains(searchStr))
                 );

      // Typumwandlung der Liste mit der Cast() Methode
      var list = CollectionView.Cast<Nachricht>();
      // führt zu Fehler, wenn kein Suchtreffer vorhanden ist.
      var first = list.FirstOrDefault(checkLambda);
      // fix:
      //var list = CollectionView.SourceCollection.Cast<Nachricht>();
      if (Context.Nachrichten.Any(checkLambda))
      {
        Nachricht n = list.FirstOrDefault(checkLambda);
        CollectionView.MoveCurrentTo(n);
      }


    }

    private void BtDelete_Click(object sender, RoutedEventArgs e)
    {
      int searchID = (int)LbMessage.Content;
      Nachricht n = Context.Nachrichten.Where(x => x.ID_Nachricht == searchID).FirstOrDefault();
      Context.Nachrichten.Remove(n);
      Context.SaveChanges();
    }

    private void TbSearchFilter_KeyDown(object sender, KeyEventArgs e)
    {
      string filter = TbFilter.Text.ToLower();
      CollectionView.Filter = (x => ((Nachricht)x).Text.ToLower().Contains(filter));
    }
  }

}
