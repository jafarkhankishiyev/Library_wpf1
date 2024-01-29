using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using Library_wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.ViewModel
{
    public class AuthorViewModel : BaseViewModel
    {
        private MainViewModel _mainViewModel;
        private bool addAuthorButtonEnabled;
        private bool deleteAuthorButtonEnabled;
        private bool editAuthorButtonEnabled;
        private bool saveAuthorButtonEnabled;
        private bool authorNameTextBoxEnabled;
        private bool authorMobileTextBoxEnabled;
        private bool authorEmailTextBoxEnabled;
        private bool authorBirthdayTextBoxEnabled;
        private bool clearAuthorComboBoxEnabled;
        private Author selectedAuthor;
        private RelayCommand editAuthorCommand;
        private RelayCommand deleteAuthorCommand;
        private RelayCommand switchToAddAuthorViewCommand;
        private RelayCommand switchToEditAuthorViewCommand;
        private IAuthorDB _authorDB;
        private ObservableCollection<Author> authorListSource;

        public AuthorViewModel(IAuthorDB authorDB, MainViewModel mainViewModel) 
        {
            _authorDB = authorDB;
            _ = GetAuthors();
            AddAuthorButtonEnabled = true;
            _mainViewModel = mainViewModel;
        }

        public ObservableCollection<Author> AuthorListSource
        {
            get { return authorListSource; }
            set
            {
                authorListSource = value;
                OnPropertyChanged("AuthorListSource");
            }
        }
        public bool AddAuthorButtonEnabled
        {
            get { return addAuthorButtonEnabled; }
            set
            {
                addAuthorButtonEnabled = value;
                OnPropertyChanged("AddAuthorButtonEnabled");
            }
        }
        public bool EditAuthorButtonEnabled
        {
            get { return editAuthorButtonEnabled; }
            set
            {
                editAuthorButtonEnabled = value;
                OnPropertyChanged("EditAuthorButtonEnabled");
            }
        }
        public bool DeleteAuthorButtonEnabled
        {
            get { return deleteAuthorButtonEnabled; }
            set
            {
                deleteAuthorButtonEnabled = value;
                OnPropertyChanged("DeleteAuthorButtonEnabled");
            }
        }
        public bool SaveAuthorButtonEnabled
        {
            get { return saveAuthorButtonEnabled; }
            set
            {
                saveAuthorButtonEnabled = value;
                OnPropertyChanged("SaveAuthorButtonEnabled");
            }
        }
        public bool ClearAuthorComboBoxEnabled { get { return clearAuthorComboBoxEnabled; } set { clearAuthorComboBoxEnabled = value; OnPropertyChanged("ClearAuthorComboBoxEnabled"); } }
        public bool AuthorNameTextBoxEnabled { get { return authorNameTextBoxEnabled; } set { authorNameTextBoxEnabled = value; OnPropertyChanged("AuthorNameTextBoxEnabled"); } }
        public bool AuthorEmailTextBoxEnabled { get { return authorEmailTextBoxEnabled; } set { authorEmailTextBoxEnabled = value; OnPropertyChanged("AuthorEmailTextBoxEnabled"); } }
        public bool AuthorBirthdayTextBoxEnabled { get { return authorBirthdayTextBoxEnabled; } set { authorBirthdayTextBoxEnabled = value; OnPropertyChanged("AuthorBirthdayTextBoxEnabled"); } }
        public bool AuthorMobileTextBoxEnabled { get { return authorMobileTextBoxEnabled; } set { authorMobileTextBoxEnabled = value; OnPropertyChanged("AuthorMobileTextBoxEnabled"); } }
        public RelayCommand EditAuthorCommand { get { return editAuthorCommand; } }
        public RelayCommand SwitchToAddAuthorViewCommand { get { return switchToAddAuthorViewCommand ?? (switchToAddAuthorViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddEditAuthorView(new AddEditAuthorViewModel(_authorDB, _mainViewModel)))); } }
        public RelayCommand SwitchToEditAuthorViewCommand { get { return switchToEditAuthorViewCommand ?? (switchToEditAuthorViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddEditAuthorView(new AddEditAuthorViewModel(_authorDB, _mainViewModel, SelectedAuthor)))); } }
        public RelayCommand DeleteAuthorCommand { get { return deleteAuthorCommand ?? (deleteAuthorCommand = new RelayCommand(obj => DeleteAuthorCommandMethod())); } }
        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                selectedAuthor = value;
                if (selectedAuthor == null)
                {
                    AddAuthorButtonEnabled = true;
                    EditAuthorButtonEnabled = false;
                    DeleteAuthorButtonEnabled = false;
                    ClearAuthorComboBoxEnabled = false;
                }
                else
                {
                    AddAuthorButtonEnabled = false;
                    EditAuthorButtonEnabled = true;
                    DeleteAuthorButtonEnabled = true;
                    ClearAuthorComboBoxEnabled = true;
                }
                OnPropertyChanged("SelectedAuthor");
            }
        }
        public async Task GetAuthors()
        {
            AuthorListSource = await _authorDB.GetAuthorsAsync();
            AuthorListSource.Remove(AuthorListSource[0]);
        }
        public async void DeleteAuthorCommandMethod()
        {
            if(MessageBox.Show($"Are you sure you want to delete {SelectedAuthor.Name} from authors?", "Delete Author", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int result = await _authorDB.DeleteAuthor(SelectedAuthor);
                _ = GetAuthors();
                MessageBox.Show($"Deleted {result} author.");
            }
        }
    }
}
