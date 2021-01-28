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
      //CollectionView.Filter = (x => ((Nachricht)x).ID_Nachricht < 20);
      ParentGrid.DataContext = CollectionView;

    }

    private void TbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      string searchStr = TbSearch.Text.ToLower();
      // Zurücksetzen des Filters
      CollectionView.Filter = null;
      // Typumwandlung der Liste mit der Cast() Methode
      var list = CollectionView.Cast<Nachricht>();
      Func<Nachricht, bool> checkLambda = (x => x.Text.ToLower().Contains(searchStr)
                  || x.NutzerSender.Name.ToLower().Contains(searchStr)
                  || x.NutzerEmpfaenger.Any(y => y.Name.ToLower().Contains(searchStr))
                 );
      //list.FirstOrDefault(checkLambda);
      if (Context.Nachrichten.Any(checkLambda))
      {
        Nachricht n = list.FirstOrDefault(checkLambda);
        CollectionView.MoveCurrentTo(n);
      }


    }

    private void TbSearchFilter_KeyDown(object sender, KeyEventArgs e)
    {
      string filter = TbFilter.Text.ToLower();
      CollectionView.Filter = (x => ((Nachricht)x).Text.ToLower().Contains(filter));
    }
  }

}
