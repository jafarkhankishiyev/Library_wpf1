using Library_wpf.DB;
using Library_wpf.ViewModel;
using Library_wpf.ViewModelNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_wpf.Views
{
    /// <summary>
    /// Interaction logic for AuthorView.xaml
    /// </summary>
    public partial class AuthorView : UserControl
    {
        public AuthorView(AuthorViewModel authorViewModel)
        {
            InitializeComponent();
            DataContext = authorViewModel;
        }
    }
}
