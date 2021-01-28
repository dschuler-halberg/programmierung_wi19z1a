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
      //CollectionView.Filter = (x => ((Nachricht)x).ID_Nachricht < 20);
      ParentGrid.DataContext = CollectionView;

    }

    private void BtForward_Click(object sender, RoutedEventArgs e)
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

    private void BtSearch_Click(object sender, RoutedEventArgs e)
    {
      string searchStr = TbSearchFilter.Text.ToLower();
      // Zurücksetzen des Filters
      CollectionView.Filter = null;
      // Typumwandlung der Liste mit der Cast() Methode
      var list = CollectionView.Cast<Nachricht>();
      Nachricht n = list.FirstOrDefault(x => x.Text.ToLower().Contains(searchStr)
             || x.NutzerSender.Name.ToLower().Contains(searchStr)
             || x.NutzerEmpfaenger.Any(y => y.Name.ToLower().Contains(searchStr))
      
      );
      CollectionView.MoveCurrentTo(n);
    }

    private void BtFilter_Click(object sender, RoutedEventArgs e)
    {
      string filter = TbSearchFilter.Text.ToLower();
      CollectionView.Filter = (x => ((Nachricht)x).Text.ToLower().Contains(filter));
    }

    private void BtDelete_Click(object sender, RoutedEventArgs e)
    {
      int searchID = (int)LbMessage.Content;
      Nachricht n = Context.Nachrichten.Where(x => x.ID_Nachricht == searchID).FirstOrDefault();
      Context.Nachrichten.Remove(n);
      Context.SaveChanges();
    }
  }

}
