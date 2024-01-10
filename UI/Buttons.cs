using Library_wpf.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Library_wpf;
using System.Windows.Controls;

namespace Library_wpf.UI
{
    public class ButtonsUI : UIClass
    { 

        public bool isButton1Clicked = false;
        public bool isAddBookButtonClicked = false;
        public bool isDeleteBookButtonClicked = false;
        public bool isEditBookButtonClicked = false;
        private WarningsUI _warningmanager;

        public ButtonsUI(MainWindow mainwindow) : base(mainwindow)
        {
            _warningmanager = new WarningsUI(_mainwindow);
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            isButton1Clicked = true;
            Book book = new Book();
            book.Name = _mainwindow.nameTextBox.Text;
            book.Author = _mainwindow.authorTextBox.Text;
            book.Genre = _mainwindow.genreTextBox.Text;
            int result;
            bool releaseCheck = Int32.TryParse(_mainwindow.yearTextBox.Text, out result);
            if (releaseCheck)
            {
                book.Release = result;
            } 
            else
            {
                book.Release = 0;
            }
            int validateNum = _warningmanager.Validate(book);
            if (validateNum == 1)
            {
                if (isAddBookButtonClicked)
                {
                    int number = await _mainwindow._bookDB.AddBook(book);
                    MessageBox.Show($"Added {number} object.");
                    isButton1Clicked = false;
                    _mainwindow.SwitchVisibilityOff();
                    isAddBookButtonClicked = false;
                }
                else if (isEditBookButtonClicked)
                {
                    string dataChoicePrepared = "";
                    Book oldBook = _mainwindow.bookList.SelectedItem as Book;
                    int number = await _mainwindow._bookDB.EditBook(oldBook, book);
                    MessageBox.Show($"Modified {number} object.");
                    isEditBookButtonClicked = false;
                    _mainwindow.SwitchVisibilityOff();
                }
                isButton1Clicked = false;
                _mainwindow.editBookButton.IsEnabled = false;
                _mainwindow.deleteBookButton.IsEnabled = false;
                _ = _mainwindow.ShowBooks();
                _mainwindow.Button1.IsEnabled = false;
            } else
            {
                isButton1Clicked = false;
            }
        }
        public async void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mainwindow.bookList.SelectedItem != null)
            {
                _mainwindow.SwitchVisibilityOff();
                Book selectedItem = _mainwindow.bookList.SelectedItem as Book;
                int deleteResult = await _mainwindow._bookDB.DeleteBook(selectedItem);
                _mainwindow.deleteBookButton.IsEnabled = false;
                _mainwindow.editBookButton.IsEnabled = false;
                _ = _mainwindow.ShowBooks();
                MessageBox.Show($"Deleted {deleteResult} object(s).");
            }
        }
        public void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.nameTextBox.Text = _mainwindow.nameTextBox.Text.Remove(0);
            _mainwindow.authorTextBox.Text = _mainwindow.authorTextBox.Text.Remove(0);
            _mainwindow.genreTextBox.Text = _mainwindow.genreTextBox.Text.Remove(0);
            _mainwindow.yearTextBox.Text = _mainwindow.yearTextBox.Text.Remove(0);
            _mainwindow.SwitchVisibilityOn();
            _mainwindow.nameTextBox.Focus();
            isAddBookButtonClicked = true;
            isEditBookButtonClicked = false;
            _mainwindow.Button1.IsEnabled = true;
        }
        public void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            isEditBookButtonClicked = true;
            isAddBookButtonClicked = false;
            _mainwindow.SwitchVisibilityOn();
            Book oldBook = _mainwindow.bookList.SelectedItem as Book;
            _mainwindow.nameTextBox.Text = oldBook.Name;
            _mainwindow.authorTextBox.Text = oldBook.Author;
            _mainwindow.genreTextBox.Text = oldBook.Genre;
            _mainwindow.yearTextBox.Text = oldBook.Release.ToString();
            _mainwindow.Button1.IsEnabled = true;
        }
    }
}
