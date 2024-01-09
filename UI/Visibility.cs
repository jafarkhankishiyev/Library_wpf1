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
            _mainwindow.dynamicVisGrid.Visibility = Visibility.Visible;
        }
        public void SwitchVisibilityOff()
        {
            _mainwindow.dynamicVisGrid.Visibility = Visibility.Hidden;
        }
    }
}
