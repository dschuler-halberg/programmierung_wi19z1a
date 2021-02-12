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

namespace bsp_enity_framework_code_first
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private BloggingContext Context = new BloggingContext();

    private ICollectionView CollectionView;

    public MainWindow()
    {
      InitializeComponent();
    }



    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      AddDataToDB();

      Context.Blogs.Load();
      CollectionView = CollectionViewSource.GetDefaultView(Context.Blogs.Local);
      MainGrid.DataContext = CollectionView;


    }

    private void AddDataToDB()
    {
      var name = $"Test Blog {DateTime.Now}";
      var blog = new Blog
      {
        Name = name,
        Posts = new List<Post>()
      };
      for (int i = 0; i < 5; i++)
      {
        Post p = new Post()
        {
          Title = $"Title {i}",
          Content = $"Test Content {i} {DateTime.Now}"
        };
        blog.Posts.Add(p);
      }
      Context.Blogs.Add(blog);
      Context.SaveChanges();
    }
  }
}
