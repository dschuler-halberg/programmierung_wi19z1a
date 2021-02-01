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
      Context.Nutzer.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Nutzer.Local);
      ParentGrid.DataContext = CollectionView;

    }

    private void TbSearch_KeyUp(object sender, KeyEventArgs e)
    {
      string searchStr = TbSearch.Text.ToLower();
      // Zurücksetzen des Filters
      CollectionView.Filter = null;
      // Typumwandlung der Liste mit der Cast() Methode
      var list = CollectionView.SourceCollection.Cast<Nutzer>();
      Nutzer n = list.FirstOrDefault(x => x.Name.ToLower().Contains(searchStr));
      if (n != null)
      {
        CollectionView.MoveCurrentTo(n);
        DgNames.ScrollIntoView(n);
      }
    }

    private void TbSearchFilter_KeyUp(object sender, KeyEventArgs e)
    {
      string filter = TbFilter.Text.ToLower();
      CollectionView.Filter = (x => ((Nutzer)x).Name.ToLower().Contains(filter));
    }

    private void TbFilterID_KeyUp(object sender, KeyEventArgs e)
    {

      string filter = TbFilterID.Text.ToLower();
      CollectionView.Filter = (x => ((Nutzer)x).ID_Nutzer.ToString().ToLower().Contains(filter));

      //Alternativ (immer nur ein Treffer):
      //int searchID = -1;
      //Int32.TryParse(filter, out searchID);
      //CollectionView.Filter = (x => ((Nutzer)x).ID_Nutzer == searchID);

    }
  }


}
