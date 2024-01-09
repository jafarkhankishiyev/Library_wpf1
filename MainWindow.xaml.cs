using System.Net.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Library_wpf.DB;
using Library_wpf.UI;

namespace Library_wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public readonly BookDB _bookDB;
    private ButtonsUI _buttons;
    private SortUI _sort;
    private VisibilityUI _visibility;

    public MainWindow()
    {
        _buttons = new ButtonsUI(this);
        _sort = new SortUI(this);
        _visibility = new VisibilityUI(this);
        InitializeComponent();
        _bookDB = new BookDB();
        _ = ShowBooks();
        switchVisibilityOff();
    }
    public async Task ShowBooks()
    {
        List<Book> books = await _bookDB.GetBooksAsync();
        bookList.ItemsSource = new List<Book>();
        bookList.ItemsSource = books;
    }

    //visibility
    public void switchVisibilityOn() 
    {
        _visibility.switchVisibilityOn();
    }
    public void switchVisibilityOff()
    {
        _visibility.switchVisibilityOff();
    }

    //selection
    private void bookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bookList.SelectedItem != null)
        {
            deleteBookButton.IsEnabled = true;
            editBookButton.IsEnabled = true;
        }
    }
    //sort clicks
    private void nameColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.nameColumnHeader_Click(sender, e);
    }
    private void authorColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.authorColumnHeader_Click(sender, e);
    }
    private void genreColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.genreColumnHeader_Click(sender, e);
    }
    private void yearColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.yearColumnHeader_Click(sender, e);
    }
    //button methods
    private void addBookButton_Click(object sender, RoutedEventArgs e)
    {
        _buttons.addBookButton_Click(sender, e);
    }
    private void deleteBookButton_Click( object sender, RoutedEventArgs e)
    {
        _buttons.deleteBookButton_Click(sender, e);
    }
    private void editBookButton_Click(object sender, RoutedEventArgs e)
    {
        _buttons.editBookButton_Click(sender, e);
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _buttons.Button_Click(sender, e);
    }
}