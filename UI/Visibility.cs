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
        public void switchVisibilityOn()
        {
            _mainwindow.dynamicVisGrid.Visibility = Visibility.Visible;
        }
        public void switchVisibilityOff()
        {
            _mainwindow.dynamicVisGrid.Visibility = Visibility.Hidden;
        }
    }
}
