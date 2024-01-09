using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.UI
{
    class SortUI : UIClass
    {
        public bool isNameSortClicked = false;
        public bool isAuthorSortClicked = false;
        public bool isGenreSortClicked = false;
        public bool isYearSortClicked = false;
        private List<Book> booksToSort = new List<Book>();

        public SortUI(MainWindow mainwindow) : base(mainwindow)
        {
        }

        public void nameColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (isNameSortClicked)
            {
                isNameSortClicked = false;
                _ = _mainwindow.ShowBooks();
            }
            else
            {
                isAuthorSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = false;
                isNameSortClicked = true;
                booksToSort = _mainwindow.bookList.ItemsSource as List<Book>;
                booksToSort.Sort((x, y) => string.Compare(x.Name, y.Name));
                _mainwindow.bookList.ItemsSource = new List<Book>();
                _mainwindow.bookList.ItemsSource = booksToSort;
            }
        }
        public void authorColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (isAuthorSortClicked)
            {
                isAuthorSortClicked = false;
                _ = _mainwindow.ShowBooks();
            }
            else
            {
                isNameSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = false;
                isAuthorSortClicked = true;
                booksToSort = _mainwindow.bookList.ItemsSource as List<Book>;
                booksToSort.Sort((x, y) => string.Compare(x.Author, y.Author));
                _mainwindow.bookList.ItemsSource = new List<Book>();
                _mainwindow.bookList.ItemsSource = booksToSort;
            }
        }
        public void genreColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (isGenreSortClicked)
            {
                isGenreSortClicked = false;
                _ = _mainwindow.ShowBooks();
            }
            else
            {
                isNameSortClicked = false;
                isAuthorSortClicked = false;
                isYearSortClicked = false;
                isGenreSortClicked = true;
                booksToSort = _mainwindow.bookList.ItemsSource as List<Book>;
                booksToSort.Sort((x, y) => string.Compare(x.Genre, y.Genre));
                _mainwindow.bookList.ItemsSource = new List<Book>();
                _mainwindow.bookList.ItemsSource = booksToSort;
            }
        }
        public void yearColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (isYearSortClicked)
            {
                isYearSortClicked = false;
                _ = _mainwindow.ShowBooks();
            }
            else
            {
                isNameSortClicked = false;
                isAuthorSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = true;
                booksToSort = _mainwindow.bookList.ItemsSource as List<Book>;
                //booksToSort.Sort((x, y) => string.Compare(x.Release, y.Release));
                booksToSort = booksToSort.OrderByDescending(x => x.Release).ToList();
                _mainwindow.bookList.ItemsSource = new List<Book>();
                _mainwindow.bookList.ItemsSource = booksToSort;
            }
        }
    }
}
