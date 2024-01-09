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
        SwitchVisibilityOff();
    }
    public async Task ShowBooks()
    {
        List<Book> books = await _bookDB.GetBooksAsync();
        bookList.ItemsSource = new List<Book>();
        bookList.ItemsSource = books;
    }

    //visibility
    public void SwitchVisibilityOn() 
    {
        _visibility.SwitchVisibilityOn();
    }
    public void SwitchVisibilityOff()
    {
        _visibility.SwitchVisibilityOff();
    }

    //selection
    private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bookList.SelectedItem != null)
        {
            deleteBookButton.IsEnabled = true;
            editBookButton.IsEnabled = true;
        }
    }
    //sort clicks
    private void NameColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.nameColumnHeader_Click(sender, e);
    }
    private void AuthorColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.authorColumnHeader_Click(sender, e);
    }
    private void GenreColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.genreColumnHeader_Click(sender, e);
    }
    private void YearColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        _sort.yearColumnHeader_Click(sender, e);
    }
    //button methods
    private void AddBookButton_Click(object sender, RoutedEventArgs e)
    {
        _buttons.AddBookButton_Click(sender, e);
    }
    private void DeleteBookButton_Click( object sender, RoutedEventArgs e)
    {
        _buttons.DeleteBookButton_Click(sender, e);
    }
    private void EditBookButton_Click(object sender, RoutedEventArgs e)
    {
        _buttons.EditBookButton_Click(sender, e);
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _buttons.Button_Click(sender, e);
    }
}