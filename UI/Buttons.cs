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
            book.Release = Int32.Parse(_mainwindow.yearTextBox.Text);
            int validateNum = _warningmanager.Validate(book);
            if (validateNum == 1)
            {
                if (isAddBookButtonClicked)
                {
                    int number = await _mainwindow._bookDB.AddBook(book);
                    MessageBox.Show($"Добавлен {number} объект.");
                    isButton1Clicked = false;
                    _mainwindow.switchVisibilityOff();
                    isAddBookButtonClicked = false;
                }
                else if (isEditBookButtonClicked)
                {
                    string dataChoicePrepared = "";
                    Book oldBook = _mainwindow.bookList.SelectedItem as Book;
                    int number = await _mainwindow._bookDB.EditBook(oldBook, book);
                    MessageBox.Show($"Изменен {number} объект.");
                    isEditBookButtonClicked = false;
                    _mainwindow.nameTextBox.Text.Remove(0);
                    _mainwindow.authorTextBox.Text.Remove(0);
                    _mainwindow.genreTextBox.Text.Remove(0);
                    _mainwindow.yearTextBox.Text.Remove(0);
                    _mainwindow.switchVisibilityOff();
                    _mainwindow.bookList.SelectionMode = SelectionMode.Multiple;
                }
                isButton1Clicked = false;
                _mainwindow.editBookButton.IsEnabled = false;
                _mainwindow.deleteBookButton.IsEnabled = false;
                _ = _mainwindow.ShowBooks();
            } else
            {
                isButton1Clicked = false;
            }
        }
        public async void deleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mainwindow.bookList.SelectedItem != null)
            {
                _mainwindow.switchVisibilityOff();
                Book selectedItem = _mainwindow.bookList.SelectedItem as Book;
                int deleteResult = await _mainwindow._bookDB.DeleteBook(selectedItem);
                _mainwindow.deleteBookButton.IsEnabled = false;
                _mainwindow.editBookButton.IsEnabled = false;
                _ = _mainwindow.ShowBooks();
                MessageBox.Show($"Удален {deleteResult} объект.");
            }
        }
        public void addBookButton_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.nameTextBox.Text = _mainwindow.nameTextBox.Text.Remove(0);
            _mainwindow.authorTextBox.Text = _mainwindow.authorTextBox.Text.Remove(0);
            _mainwindow.genreTextBox.Text = _mainwindow.genreTextBox.Text.Remove(0);
            _mainwindow.yearTextBox.Text = _mainwindow.yearTextBox.Text.Remove(0);
            _mainwindow.switchVisibilityOn();
            _mainwindow.nameTextBox.Focus();
            isAddBookButtonClicked = true;
            isEditBookButtonClicked = false;
        }
        public void editBookButton_Click(object sender, RoutedEventArgs e)
        {
            isEditBookButtonClicked = true;
            _mainwindow.bookList.SelectionMode = SelectionMode.Single;
            isAddBookButtonClicked = false;
            _mainwindow.switchVisibilityOn();
            Book oldBook = _mainwindow.bookList.SelectedItem as Book;
            _mainwindow.nameTextBox.Text = oldBook.Name;
            _mainwindow.authorTextBox.Text = oldBook.Author;
            _mainwindow.genreTextBox.Text = oldBook.Genre;
            _mainwindow.yearTextBox.Text = oldBook.Release.ToString();
        }
    }
}
