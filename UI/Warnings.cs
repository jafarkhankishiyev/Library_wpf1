using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.UI
{
    class WarningsUI : UIClass
    {
        public WarningsUI(MainWindow mainwindow) : base(mainwindow)
        {
        }
        public int Validate(Book book)
        {
            _mainwindow.nameWarningTextBlock.Text = _mainwindow.nameWarningTextBlock.Text.Remove(0);
            _mainwindow.authorWarningTextBlock.Text = _mainwindow.authorWarningTextBlock.Text.Remove(0);
            _mainwindow.genreWarningTextBlock.Text = _mainwindow.genreWarningTextBlock.Text.Remove(0);
            _mainwindow.yearWarningTextBlock.Text = _mainwindow.yearWarningTextBlock.Text.Remove(0);
            int result;
            if (string.IsNullOrWhiteSpace(book.Name))
            {
                _mainwindow.nameWarningTextBlock.Text = "*fill the name field";
                return 2;
            }
            else if (string.IsNullOrWhiteSpace(book.Author))
            {
                _mainwindow.authorWarningTextBlock.Text = "*fill the author field";
                return 3;
            }
            else if (string.IsNullOrEmpty(book.Genre))
            {
                _mainwindow.genreWarningTextBlock.Text = "*fill the genre field";
                return 4;
            }
            else if (book.Release == 0)
            {
                _mainwindow.yearWarningTextBlock.Text = "*fill the year field with YYYY (e.g. 1984)";
                return 5;
            } else
            {
                return 1;
            }
        }
    }
}
