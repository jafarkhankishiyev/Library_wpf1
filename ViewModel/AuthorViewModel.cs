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
        private bool isEditAuthorButtonClicked;
        private bool isAddAuthorButtonClicked;
        private bool isSaveAuthorButtonClicked;
        private bool authorNameTextBoxEnabled;
        private bool authorMobileTextBoxEnabled;
        private bool authorEmailTextBoxEnabled;
        private bool authorBirthdayTextBoxEnabled;
        private bool clearAuthorComboBoxEnabled;
        private bool authorCheckBoxEnabled;
        private Author selectedAuthor;
        private RelayCommand addAuthorCommand;
        private RelayCommand editAuthorCommand;
        private RelayCommand deleteAuthorCommand;
        private RelayCommand saveAuthorCommand;
        private RelayCommand clearAuthorComboBoxCommand;
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
        public RelayCommand SwitchToAddAuthorViewCommand { get { return switchToAddAuthorViewCommand ?? (switchToAddAuthorViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddAuthorView(new AddAuthorViewModel(_authorDB, _mainViewModel)))); } }
        public RelayCommand SwitchToEditAuthorViewCommand { get { return switchToEditAuthorViewCommand ?? (switchToEditAuthorViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new EditAuthorView(new EditAuthorViewModel(_authorDB, SelectedAuthor, _mainViewModel)))); } }
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
        /*public int ValidateAuthor(Author author)
        {
            AuthorNameWarningText = string.Empty;
            AuthorEmailWarningText = string.Empty;
            AuthorMobileWarningText = string.Empty;
            AuthorBirthdayWarningText = string.Empty;
            if (string.IsNullOrEmpty(author.Name))
            {
                AuthorNameWarningText = "*";
                return 2;
            }
            else if (string.IsNullOrEmpty(author.Mobile))
            {
                AuthorMobileWarningText = "*";
                return 5;
            }
            else if (string.IsNullOrEmpty(author.Email))
            {
                AuthorEmailWarningText = "*";
                return 3;
            }
            else if (author.Birthday == new DateTime(1, 1, 1))
            {
                AuthorBirthdayWarningText = "*yyyy-MM-dd";
                return 4;
            }
            else
            {
                return 1;
            }
        }
        public async void SaveAuthorCommandMethod()
        {
            isSaveAuthorButtonClicked = true;
            Author newAuthor = new Author();
            newAuthor.Name = AuthorNameText;
            newAuthor.Mobile = AuthorMobileText;
            newAuthor.Email = AuthorEmailText;
            bool birthdayCheck = DateTime.TryParseExact(AuthorBirthdayText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
            if (birthdayCheck)
            {
                newAuthor.Birthday = result;
            }
            else
            {
                newAuthor.Birthday = new DateTime(1, 1, 1);
            }
            int validateNum = ValidateAuthor(newAuthor);
            if (validateNum == 1)
            {
                if (isAddAuthorButtonClicked)
                {
                    int number = await _authorDB.AddAuthor(newAuthor);
                    MessageBox.Show($"Added {number} author.");
                    isAddAuthorButtonClicked = false;
                }
                else if (isEditAuthorButtonClicked)
                {
                    int number = await _authorDB.EditAuthor(SelectedAuthor as Author, newAuthor);
                    MessageBox.Show($"Edited {number} author.");
                    isEditAuthorButtonClicked = false;
                }
                _ = GetAuthors();
                isSaveAuthorButtonClicked = false;
                EditAuthorButtonEnabled = false;
                DeleteAuthorButtonEnabled = false;
                SaveAuthorButtonEnabled = false;
                AddAuthorButtonEnabled = true;
                ClearAuthorText();
                CurrentView = new AuthorView(this);
            }
            else
            {
                isSaveAuthorButtonClicked = false;
            }
        }
        public void EditAuthorCommandMethod()
        {
            //CurrentView = new AddAuthorView(_mainViewModel);
            Author selectedAuthor = SelectedAuthor as Author;
            AuthorNameText = selectedAuthor.Name;
            AuthorEmailText = selectedAuthor.Email;
            AuthorMobileText = selectedAuthor.Mobile;
            AuthorBirthdayText = selectedAuthor.Birthday.ToString("yyyy-MM-dd");
            isEditAuthorButtonClicked = true;
            isAddAuthorButtonClicked = false;
            SaveAuthorButtonEnabled = true;
            AuthorNameTextBoxEnabled = true;
            AuthorEmailTextBoxEnabled = true;
            AuthorMobileTextBoxEnabled = true;
            AuthorBirthdayTextBoxEnabled = true;

        }
        public void AddAuthorCommandMethod()
        {
            //CurrentView = new AddAuthorView(this);
            ClearAuthorText();
            AuthorNameTextBoxEnabled = true;
            AuthorEmailTextBoxEnabled = true;
            AuthorMobileTextBoxEnabled = true;
            AuthorBirthdayTextBoxEnabled = true;
            isAddAuthorButtonClicked = true;
            isEditAuthorButtonClicked = false;
            SaveAuthorButtonEnabled = true;
        }*/
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
