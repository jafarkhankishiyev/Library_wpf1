using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using Library_wpf.Views;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.ViewModel
{
    public class EditAuthorViewModel : BaseViewModel
    {
        private MainViewModel _mainViewModel;
        private IAuthorDB _authorDB;
        private string authorNameText;
        private string authorBirthdayText;
        private string authorEmailText;
        private string authorMobileText;
        private string authorNameWarningText;
        private string authorMobileWarningText;
        private string authorEmailWarningText;
        private string authorBirthdayWarningText;
        private RelayCommand saveAuthorCommand;
        private Author selectedAuthor;
        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
            }
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
        public RelayCommand SaveAuthorCommand { get { return saveAuthorCommand ?? (saveAuthorCommand = new RelayCommand(obj => SaveAuthorCommandMethod())); } }
        public EditAuthorViewModel(IAuthorDB authorDB, Author selectedAuthor, MainViewModel mainViewModel) 
        { 
            _authorDB = authorDB;
            _mainViewModel = mainViewModel;
            SelectedAuthor = selectedAuthor;
            AuthorNameText = SelectedAuthor.Name;
            AuthorEmailText = SelectedAuthor.Email;
            AuthorMobileText = SelectedAuthor.Mobile;
            AuthorBirthdayText = SelectedAuthor.BirthdayString;
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
        public async void SaveAuthorCommandMethod()
        {
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
                int number = await _authorDB.EditAuthor(SelectedAuthor, newAuthor);
                MessageBox.Show($"Edited {number} author.");
                _mainViewModel.CurrentView = new AuthorView(new AuthorViewModel(_authorDB, _mainViewModel));
            }
        }
    }
}
