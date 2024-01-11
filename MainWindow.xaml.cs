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
using Library_wpf.ViewModelNameSpace;
using Library_wpf.UI;

namespace Library_wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //    private SortUI _sort;
    /*
    public bool isNameSortClicked = false;
    public bool isAuthorSortClicked = false;
    public bool isGenreSortClicked = false;
    public bool isYearSortClicked = false;
    private List<Book> booksToSort = new List<Book>();
    */
    public MainWindow()
    {
        //_sort = new SortUI(this);
        InitializeComponent();
        DataContext = new LibraryViewModel();
    }
    private void NameColumnHeader_Click(object sender, RoutedEventArgs e)
    {
    }
    private void AuthorColumnHeader_Click(object sender, RoutedEventArgs e)
    {
    }
    private void GenreColumnHeader_Click(object sender, RoutedEventArgs e)
    {
    }
    private void YearColumnHeader_Click(object sender, RoutedEventArgs e)
    {
    }
    private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bookList.SelectedItems.Count == 1)
        {
            addBookButton.IsEnabled = false;
            deleteBookButton.IsEnabled = true;
            editBookButton.IsEnabled = true;
        }
        else if (bookList.SelectedItems.Count > 1)
        {
            addBookButton.IsEnabled = false;
            deleteBookButton.IsEnabled = true;
            editBookButton.IsEnabled = false;
        }
        else
        {
            addBookButton.IsEnabled = true;
            deleteBookButton.IsEnabled = false;
            editBookButton.IsEnabled = false;
        }
//        _visibility.SwitchVisibilityOff();
    }


    /*
    public void nameColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isNameSortClicked)
        {
            isNameSortClicked = false;
            //_ = _libraryviewmodel.ShowBooks();
        }
        else
        {
            isAuthorSortClicked = false;
            isGenreSortClicked = false;
            isYearSortClicked = false;
            isNameSortClicked = true;
            booksToSort = bookList.ItemsSource as List<Book>;
            booksToSort.Sort((x, y) => string.Compare(x.Name, y.Name));
            bookList.ItemsSource = new List<Book>();
            bookList.ItemsSource = booksToSort;
        }
    }
    public void authorColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isAuthorSortClicked)
        {
            isAuthorSortClicked = false;
            //  _ = _libraryviewmodel.ShowBooks();
        }
        else
        {
            isNameSortClicked = false;
            isGenreSortClicked = false;
            isYearSortClicked = false;
            isAuthorSortClicked = true;
            booksToSort = bookList.ItemsSource as List<Book>;
            booksToSort.Sort((x, y) => string.Compare(x.Author, y.Author));
            bookList.ItemsSource = new List<Book>();
            bookList.ItemsSource = booksToSort;
        }
    }
    public void genreColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isGenreSortClicked)
        {
            isGenreSortClicked = false;
            //  _ = _libraryviewmodel.ShowBooks();
        }
        else
        {
            isNameSortClicked = false;
            isAuthorSortClicked = false;
            isYearSortClicked = false;
            isGenreSortClicked = true;
            booksToSort = bookList.ItemsSource as List<Book>;
            booksToSort.Sort((x, y) => string.Compare(x.Genre, y.Genre));
            bookList.ItemsSource = new List<Book>();
            bookList.ItemsSource = booksToSort;
        }
    }
    public void yearColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isYearSortClicked)
        {
            isYearSortClicked = false;
            //  _ = _libraryviewmodel.ShowBooks();
        }
        else
        {
            isNameSortClicked = false;
            isAuthorSortClicked = false;
            isGenreSortClicked = false;
            isYearSortClicked = true;
            booksToSort = bookList.ItemsSource as List<Book>;
            //booksToSort.Sort((x, y) => string.Compare(x.Release, y.Release));
            booksToSort = booksToSort.OrderByDescending(x => x.Release).ToList();
            bookList.ItemsSource = new List<Book>();
            bookList.ItemsSource = booksToSort;
        }
    }
    */
}