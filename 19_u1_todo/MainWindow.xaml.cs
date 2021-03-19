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

namespace _19_u1_todo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>

  public class TodoItem
  {

    public string Text { get; set; }

    public bool Done { get; set; } = false;

    public static TodoItem FromString (string str)
    {
      return null;
    }

  }

  public partial class MainWindow : Window
  {
    public List<string> Aufgaben = new List<string>();

    public MainWindow()
    {
      //Aufgaben.Add("Aufräumen");
      //Aufgaben.Add("Putzen");
      InitializeComponent();
      //Lv1.ItemsSource = Aufgaben;


      if (Properties.Settings.Default.TodoItems == null)
      {
        Properties.Settings.Default.TodoItems = new System.Collections.Specialized.StringCollection();
      }
      if(Properties.Settings.Default.DoneItems == null)
      {

        Properties.Settings.Default.DoneItems = new System.Collections.Specialized.StringCollection();
      }

      foreach (string todoitem in Properties.Settings.Default.TodoItems)
      {
        AddTodoItemToGUI(todoitem);
      }

      foreach (string doneItem in Properties.Settings.Default.DoneItems)
      {
        MarkAsDone(doneItem);
      }
    }

    private void MarkAsDone(string doneItem)
    {
      foreach(var item in Lv1.Items)
      {
        ListViewItem lvi = (ListViewItem)item;
        TextBlock tb = (TextBlock)lvi.Content;
        if(tb.Text == doneItem)
        {
          MarkAsDone(lvi);
        }
      }
    }

    public int count = 0;

    public void AddTodoItem(string task)
    {
      Properties.Settings.Default.TodoItems.Add(task);
      Properties.Settings.Default.Save();
      AddTodoItemToGUI(task);
    }

    public void AddTodoItemToGUI(string task) { 
      TextBlock bl = new TextBlock()
      {
        Text = task,
        TextWrapping = TextWrapping.Wrap,
        Width = 300
      };

      ListViewItem newTodoEntry = new ListViewItem() { Content = bl };
      if (count % 2 == 0)
      {
        newTodoEntry.Background = Brushes.Blue;
      }
      else
      {
        newTodoEntry.Background = Brushes.Red;
      }
      count++;
      Lv1.Items.Add(newTodoEntry);
    }


    private void Tb1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        //Aufgaben.Add(Tb1.Text);
        //Lv1.Items.Refresh();
        AddTodoItem(Tb1.Text);
        Tb1.Clear();
      }
    }

    private void Lv1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      var selected = Lv1.SelectedItem;
      if (selected != null)
      {
        ListViewItem lvi = (ListViewItem)selected;
        MarkAsDone(lvi);
        TextBlock tb = (TextBlock)lvi.Content;
        string doneItem = tb.Text;
        Properties.Settings.Default.DoneItems.Add(doneItem);
        Properties.Settings.Default.Save();
      }
    }

    private static void MarkAsDone(ListViewItem lvi)
    {
      lvi.IsEnabled = false;
      lvi.HorizontalAlignment = HorizontalAlignment.Right;
      lvi.Background = Brushes.Gray;
    }
  }

}
