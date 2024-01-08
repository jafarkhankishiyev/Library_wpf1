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

namespace Library_wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool isButton1Clicked = false;
    private bool isAddBookButtonClicked = false;
    private bool isDeleteBookButtonClicked = false;
    private bool isEditBookButtonClicked = false;
    private readonly BookDB _bookDB;
    private bool isNameSortClicked = false;
    private bool isAuthorSortClicked = false;
    private bool isGenreSortClicked = false;
    private bool isYearSortClicked = false;
    private List<Book> booksToSort = new List<Book>();
    public MainWindow()
    {
        InitializeComponent();
        _bookDB = new BookDB();
        _ = ShowBooks();
        switchVisibilityOff();
    }
    private async void Button_Click(object sender, RoutedEventArgs e) 
    {
        isButton1Clicked=true;
        Book book = new Book();
        book.Name = nameTextBox.Text;
        book.Author = authorTextBox.Text;
        book.Genre = genreTextBox.Text;
        book.Release = yearTextBox.Text;
        if (isAddBookButtonClicked)
        {
            int number = await _bookDB.AddBook(book);
            MessageBox.Show($"Добавлен {number} объект.");
            isButton1Clicked = false;
            switchVisibilityOff();
            isAddBookButtonClicked=false;
        } else if(isEditBookButtonClicked)
        {
            string dataChoicePrepared = "";
            Book oldBook = bookList.SelectedItem as Book;
            int number = await _bookDB.EditBook(oldBook, book);
            MessageBox.Show($"Изменен {number} объект.");
            isEditBookButtonClicked = false;
            nameTextBox.Text.Remove(0);
            authorTextBox.Text.Remove(0);
            genreTextBox.Text.Remove(0);
            yearTextBox.Text.Remove(0);
            switchVisibilityOff();
        }
        isButton1Clicked = false;
        editBookButton.IsEnabled = false;
        deleteBookButton.IsEnabled = false;
        _ = ShowBooks();
    }
    private async void deleteBookButton_Click(object sender, RoutedEventArgs e)
    {
        if (bookList.SelectedItem != null)
        {
            switchVisibilityOff();
            Book selectedItem = bookList.SelectedItem as Book;
            int deleteResult = await _bookDB.DeleteBook(selectedItem);
            deleteBookButton.IsEnabled = false;
            editBookButton.IsEnabled = false;
            _ = ShowBooks();
            MessageBox.Show($"Удален {deleteResult} объект.");
        }
    }
    private async Task ShowBooks()
    {
            List<Book> books = await _bookDB.GetBooksAsync();
            bookList.ItemsSource = new List<Book>();
            bookList.ItemsSource = books;
    }
    private void addBookButton_Click(object sender, RoutedEventArgs e)
    {
        nameTextBox.Text = nameTextBox.Text.Remove(0);
        authorTextBox.Text = authorTextBox.Text.Remove(0);
        genreTextBox.Text = genreTextBox.Text.Remove(0);
        yearTextBox.Text = yearTextBox.Text.Remove(0);
        switchVisibilityOn();
        nameTextBox.Focus();
        isAddBookButtonClicked = true;
        isEditBookButtonClicked = false;
    }
    private void switchVisibilityOn() 
    {
        nameTextBox.Visibility = Visibility.Visible;
        yearTextBox.Visibility = Visibility.Visible;
        authorTextBox.Visibility = Visibility.Visible;
        genreTextBox.Visibility = Visibility.Visible;
        nameTextBlock.Visibility = Visibility.Visible;
        yearTextBlock.Visibility = Visibility.Visible;
        authorTextBlock.Visibility = Visibility.Visible;
        genreTextBlock.Visibility = Visibility.Visible;
        Button1.Visibility = Visibility.Visible;
    }
    private void switchVisibilityOff()
    {
        nameTextBox.Visibility = Visibility.Hidden;
        yearTextBox.Visibility = Visibility.Hidden;
        authorTextBox.Visibility = Visibility.Hidden;
        genreTextBox.Visibility = Visibility.Hidden;
        nameTextBlock.Visibility = Visibility.Hidden;
        yearTextBlock.Visibility = Visibility.Hidden;
        authorTextBlock.Visibility = Visibility.Hidden;
        genreTextBlock.Visibility = Visibility.Hidden;
        Button1.Visibility = Visibility.Hidden;
     }
    private void bookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bookList.SelectedItem != null)
        {
            deleteBookButton.IsEnabled = true;
            editBookButton.IsEnabled = true;
        }
    }
    private void editBookButton_Click(object sender, RoutedEventArgs e)
    {
        isEditBookButtonClicked = true;
        isAddBookButtonClicked = false;
        switchVisibilityOn();
        Book oldBook = bookList.SelectedItem as Book;
        nameTextBox.Text = oldBook.Name;
        authorTextBox.Text = oldBook.Author;
        genreTextBox.Text = oldBook.Genre;
        yearTextBox.Text = oldBook.Release;
    }
    private void nameColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isNameSortClicked) 
        {
            isNameSortClicked = false;
            _ = ShowBooks();
        } else
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
    private void authorColumnHeader_Click(object sender, RoutedEventArgs e)
    {
        if (isAuthorSortClicked)
        {
            isAuthorSortClicked = false;
            _ = ShowBooks();
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
    private void genreColumnHeader_Click( object sender, RoutedEventArgs e )
    {
        if (isGenreSortClicked)
        {
            isGenreSortClicked = false;
            _ = ShowBooks();
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
    private void yearColumnHeader_Click(object sender, RoutedEventArgs e ) 
    {
        if (isYearSortClicked)
        {
            isYearSortClicked = false;
            _ = ShowBooks();
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
}