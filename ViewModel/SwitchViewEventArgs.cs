using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library_wpf.ViewModel
{
    public class SwitchViewEventArgs :EventArgs
    {
        public Func<UserControl> CreateView { get; }
        public object ViewModel { get; }

        public SwitchViewEventArgs(Func<UserControl> createView, object viewModel)
        {
            CreateView = createView;
            ViewModel = viewModel;
        }
    }
}
