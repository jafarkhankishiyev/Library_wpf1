﻿using Library_wpf.ViewModel;
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
    /// Interaction logic for AddAuthorView.xaml
    /// </summary>
    public partial class AddEditAuthorView : UserControl
    {
        public AddEditAuthorView(AddEditAuthorViewModel addEditAuthorViewModel)
        {
            InitializeComponent();
            DataContext = addEditAuthorViewModel;
        }
    }
}
