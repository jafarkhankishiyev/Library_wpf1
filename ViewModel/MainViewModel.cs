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
    class MainViewModel : BaseViewModel
    {

        //fields
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;











        private RelayCommand switchToBookViewCommand;
        private RelayCommand switchToAuthorViewCommand;
        private RelayCommand switchToGenreViewCommand;
        private UserControl currentView;
        //properties
        

        

        

       
        public RelayCommand SwitchToBookViewCommand { get { return switchToBookViewCommand ?? (switchToBookViewCommand = new RelayCommand(obj => CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB)))); } }
        public RelayCommand SwitchToAuthorViewCommand { get { return switchToAuthorViewCommand ?? (switchToAuthorViewCommand = new RelayCommand(obj => CurrentView = new AuthorView(new AuthorViewModel(_authorDB)))); } }
        public RelayCommand SwitchToGenreViewCommand { get { return switchToGenreViewCommand ?? (switchToGenreViewCommand = new RelayCommand(obj => CurrentView = new GenreView(new GenreViewModel(_genreDB)))); } }
        public UserControl CurrentView { get { return currentView; } set { if (value != null) { currentView = value;  OnPropertyChanged("CurrentView"); } } }

        

        public MainViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB)
        {
            _authorDB = authorDB;
            _genreDB = genreDB;
            _bookDB = bookDB;
            CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB));
        }

        //main methods
 




        //book button commands
        
        //sort commands

        //author and genre button commands
        //combobox methods

    }
}
