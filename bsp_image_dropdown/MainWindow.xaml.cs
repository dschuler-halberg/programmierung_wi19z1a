using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace bsp_image_dropdown
{

  public class ImageInfo
  {

    public string Name { get; set; }

    public string ImagePath { get; set; }

  }
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  ///
  public partial class MainWindow : Window
  {

    public ObservableCollection<ImageInfo> Images { get; set; } = new ObservableCollection<ImageInfo>();

    public MainWindow()
    {
      InitializeComponent();

      ChangeImages();

      CbMonkeys.DataContext = Images;
    }

    string topic = "monkeys";

    private void ChangeImages()
    {

      string[] filePaths = Directory.GetFiles($@".\images\{topic}\", "*.jpg", SearchOption.TopDirectoryOnly);
      int count = 1;
      Images.Clear();
      foreach (string path in filePaths)
      {
        Images.Add(new ImageInfo()
        {
          Name = $"Tier {count}",
          ImagePath = path
        });
        count++;
      }
    }


    private void BtChangeImages_Click(object sender, RoutedEventArgs e)
    {
      topic = topic == "monkeys" ? "sloths" : "monkeys";
      ChangeImages();
    }
  }
}
