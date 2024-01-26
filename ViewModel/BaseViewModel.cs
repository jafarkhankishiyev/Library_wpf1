using Library_wpf.DB;
using Library_wpf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Library_wpf.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private UserControl currentView;
        public UserControl CurrentView { get { return currentView; } set { if (value != null) { currentView = value; OnPropertyChanged("CurrentView"); } } }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
