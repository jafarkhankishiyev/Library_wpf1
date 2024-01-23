using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
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
        private string authorNameText;
        private string genreNameText;
        private string authorBirthdayText;
        private string authorEmailText;
        private string authorMobileText;
        private bool addAuthorButtonEnabled;
        private bool deleteAuthorButtonEnabled;
        private bool editAuthorButtonEnabled;
        private bool saveAuthorButtonEnabled;
        private bool isEditAuthorButtonClicked;
        private bool isAddAuthorButtonClicked;
        private bool isSaveAuthorButtonClicked;
        private string authorNameWarningText;
        private string authorMobileWarningText;
        private string authorEmailWarningText;
        private string authorBirthdayWarningText;
        private bool authorNameTextBoxEnabled;
        private bool authorMobileTextBoxEnabled;
        private bool authorEmailTextBoxEnabled;
        private bool authorBirthdayTextBoxEnabled;
        private bool clearAuthorComboBoxEnabled;
        private bool authorCheckBoxEnabled;
        private object selectedAuthor;
        private RelayCommand addAuthorCommand;
        private RelayCommand editAuthorCommand;
        private RelayCommand deleteAuthorCommand;
        private RelayCommand saveAuthorCommand;
        private RelayCommand clearAuthorComboBoxCommand;
        private IAuthorDB _authorDB;
        private ObservableCollection<Author> authorListSource;
        public ObservableCollection<Author> AuthorListSource
        {
            get { return authorListSource; }
            set
            {
                authorListSource = value;
                OnPropertyChanged("AuthorListSource");
            }
        }

        public AuthorViewModel(IAuthorDB authorDB) 
        {
            _authorDB = authorDB;
            _ = GetAuthors();
            AddAuthorButtonEnabled = true;
        }
        public string AuthorNameText
        {
            get { return authorNameText; }
            set
            {
                authorNameText = value;
                OnPropertyChanged("AuthorNameText");
            }
        }
        public string AuthorBirthdayText
        {
            get { return authorBirthdayText; }
            set
            {
                authorBirthdayText = value;
                OnPropertyChanged("AuthorBirthdayText");
            }
        }
        public string AuthorMobileText
        {
            get { return authorMobileText; }
            set
            {
                authorMobileText = value;
                OnPropertyChanged("AuthorMobileText");
            }
        }
        public string AuthorEmailText
        {
            get { return authorEmailText; }
            set
            {
                authorEmailText = value;
                OnPropertyChanged("AuthorEmailText");
            }
        }
        public string AuthorNameWarningText
        {
            get { return authorNameWarningText; }
            set
            {
                authorNameWarningText = value;
                OnPropertyChanged("AuthorNameWarningText");
            }
        }
        public string AuthorEmailWarningText
        {
            get { return authorEmailWarningText; }
            set
            {
                authorEmailWarningText = value;
                OnPropertyChanged("AuthorEmailWarningText");
            }
        }
        public string AuthorMobileWarningText
        {
            get { return authorMobileWarningText; }
            set
            {
                authorMobileWarningText = value;
                OnPropertyChanged("AuthorMobileWarningText");
            }
        }
        public string AuthorBirthdayWarningText
        {
            get { return authorBirthdayWarningText; }
            set
            {
                authorBirthdayWarningText = value;
                OnPropertyChanged("AuthorBirthdayWarningText");
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
        public RelayCommand SaveAuthorCommand { get { return saveAuthorCommand ?? (saveAuthorCommand = new RelayCommand(obj => SaveAuthorCommandMethod())); } }
        public RelayCommand EditAuthorCommand { get { return editAuthorCommand ?? (editAuthorCommand = new RelayCommand(obj => EditAuthorCommandMethod())); } }
        public RelayCommand AddAuthorCommand { get { return addAuthorCommand ?? (addAuthorCommand = new RelayCommand(obj => AddAuthorCommandMethod())); } }
        public RelayCommand DeleteAuthorCommand { get { return deleteAuthorCommand ?? (deleteAuthorCommand = new RelayCommand(obj => DeleteAuthorCommandMethod())); } }
        public object SelectedAuthor
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
        public int ValidateAuthor(Author author)
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
        public void ClearAuthorText()
        {
            AuthorNameText = string.Empty;
            AuthorBirthdayText = string.Empty;
            AuthorMobileText = string.Empty;
            AuthorEmailText = string.Empty;
            AuthorBirthdayWarningText = string.Empty;
            AuthorMobileWarningText = string.Empty;
            AuthorEmailWarningText = string.Empty;
            AuthorBirthdayWarningText = string.Empty;
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
            }
            else
            {
                isSaveAuthorButtonClicked = false;
            }
        }
        public void EditAuthorCommandMethod()
        {
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
            ClearAuthorText();
            AuthorNameTextBoxEnabled = true;
            AuthorEmailTextBoxEnabled = true;
            AuthorMobileTextBoxEnabled = true;
            AuthorBirthdayTextBoxEnabled = true;
            isAddAuthorButtonClicked = true;
            isEditAuthorButtonClicked = false;
            SaveAuthorButtonEnabled = true;
        }
        public async void DeleteAuthorCommandMethod()
        {
            Author selectedAuthor = SelectedAuthor as Author;
            int result = await _authorDB.DeleteAuthor(selectedAuthor);
            _ = GetAuthors();
            MessageBox.Show($"Deleted {result} author.");
        }
    }
}
