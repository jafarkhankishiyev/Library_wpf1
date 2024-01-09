using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.UI
{
public class UIClass
    {
        protected readonly MainWindow _mainwindow;
        protected UIClass(MainWindow mainwindow)
        {
            _mainwindow = mainwindow;
        }
    }
}
