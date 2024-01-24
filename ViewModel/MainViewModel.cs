using Library_wpf.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Library_wpf;
using System.Runtime.CompilerServices;
using Library_wpf.Models;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Library_wpf.Views;
using Library_wpf.ViewModel;

namespace Library_wpf.ViewModelNameSpace
{
    public class MainViewModel : BaseViewModel
    {

        //fields
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;
        private BookViewModel _bookViewModel;
        private AuthorViewModel _authorViewModel;
        private GenreViewModel _genreViewModel;
        private RelayCommand switchToAuthorViewCommand;
        private RelayCommand switchToGenreViewCommand;
        private RelayCommand switchToBookViewCommand;
        //properties
        public RelayCommand SwitchToAuthorViewCommand { get { return switchToAuthorViewCommand ?? (switchToAuthorViewCommand = new RelayCommand(obj => CurrentView = new AuthorView(new AuthorViewModel(_authorDB, this)))); } }
        public RelayCommand SwitchToGenreViewCommand { get { return switchToGenreViewCommand ?? (switchToGenreViewCommand = new RelayCommand(obj => CurrentView = new GenreView(new GenreViewModel(_genreDB)))); } }
        public RelayCommand SwitchToBookViewCommand { get { return switchToBookViewCommand ?? (switchToBookViewCommand = new RelayCommand(obj => CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, this)))); } }




        public MainViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB)
        {
            _authorDB = authorDB;
            _genreDB = genreDB;
            _bookDB = bookDB;
            if (CurrentView == null)
            {
                CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, this));
            }
        }
        public void SwitchToEditBookViewCommandMethod()
        {
            
        }
        private void OnSwitchViewRequested(object sender, SwitchViewEventArgs e)
        {
            CurrentView = e.CreateView.Invoke();
        }
    }
}
