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
    public MainWindow()
    {
        InitializeComponent();
        _ = ShowBooks();
        switchVisibilityOff();
        _bookDB = new BookDB();
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
                isButton1Clicked = false;
                switchVisibilityOff();
                _ = ShowBooks();
        } else if(isEditBookButtonClicked)
        {
            string dataChoicePrepared = "";
            Book selectedItem = bookList.SelectedItem as Book;

        }
    }
    public async void deleteBookButton_Click(object sender, RoutedEventArgs e)
    {
        if(bookList.SelectedItem != null)
        {
            Book selectedItem = bookList.SelectedItem as Book;
            int deleteResult = await Book.DeleteBook(selectedItem);
            deleteBookButton.IsEnabled = false;
            _ = ShowBooks();
        }

    }
    public async Task ShowBooks()
    {
        List<Book> books = await Book.GetBooksAsync();
        bookList.ItemsSource = new List<Book>();
        bookList.ItemsSource = books;
    }
    private void addBookButton_Click(object sender, RoutedEventArgs e)
    {
        switchVisibilityOn();
        nameTextBox.Focus();
        isAddBookButtonClicked = true;

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
        deleteBookButton.IsEnabled = true;
        editBookButton.IsEnabled = true;
    }
    private void editBookButton_Click(object sender, RoutedEventArgs e)
    {
        isEditBookButtonClicked = true;
        switchVisibilityOn();
        Book oldBook = bookList.SelectedItem as Book;
        nameTextBox.Text = oldBook.Name;
        authorTextBox.Text = oldBook.Author;
        genreTextBox.Text = oldBook.Genre;
        yearTextBox.Text = oldBook.Release;
    }
}