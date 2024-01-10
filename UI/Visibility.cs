using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Library_wpf.UI
{
    class VisibilityUI : UIClass
    {
        public VisibilityUI(MainWindow mainwindow) : base(mainwindow)
        { 

        }
        public void SwitchVisibilityOn()
        {
            _mainwindow.dynamicVisGrid.IsEnabled = true;
        }
        public void SwitchVisibilityOff()
        {
            _mainwindow.dynamicVisGrid.IsEnabled = false;
            _mainwindow.nameWarningTextBlock.Text = _mainwindow.nameWarningTextBlock.Text.Remove(0);
            _mainwindow.authorWarningTextBlock.Text = _mainwindow.authorWarningTextBlock.Text.Remove(0);
            _mainwindow.genreWarningTextBlock.Text = _mainwindow.genreWarningTextBlock.Text.Remove(0);
            _mainwindow.yearWarningTextBlock.Text = _mainwindow.yearWarningTextBlock.Text.Remove(0);
            _mainwindow.nameTextBox.Text = _mainwindow.nameTextBox.Text.Remove(0);
            _mainwindow.authorTextBox.Text = _mainwindow.authorTextBox.Text.Remove(0);
            _mainwindow.genreTextBox.Text = _mainwindow.genreTextBox.Text.Remove(0);
            _mainwindow.yearTextBox.Text = _mainwindow.yearTextBox.Text.Remove(0);
        }
    }
}
