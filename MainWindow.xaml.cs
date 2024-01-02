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

namespace Library_wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool isButton1Clicked = false;
    private bool isAddBookButtonClicked = false;
    public MainWindow()
    {
        InitializeComponent();
        _ = ShowBooks();
        switchVisibilityOff();
    }
    private async void Button_Click(object sender, RoutedEventArgs e) 
    {
        isButton1Clicked=true;
        if(isAddBookButtonClicked)
        {
                Book book = new Book();
                book.Name = nameTextBox.Text;
                book.Author = authorTextBox.Text;
                book.Genre = genreTextBox.Text;
                book.Release = yearTextBox.Text;
                int number = await Book.AddBook(book);
                isButton1Clicked = false;
                bookList.Items.Refresh();
                switchVisibilityOff();
                _ = ShowBooks();
        }
    }
    public void deleteBookButton_Click(object sender, RoutedEventArgs e)
    {

    }
    public async Task ShowBooks()
    {
        List<Book> books = await Book.GetBooksAsync();
        bookList.ItemsSource = new List<Book>();
        bookList.ItemsSource = books;
    }

    private async void addBookButton_Click(object sender, RoutedEventArgs e)
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
}