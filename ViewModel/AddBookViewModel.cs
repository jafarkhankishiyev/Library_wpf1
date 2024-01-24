using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using Library_wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.ViewModel
{
    public class AddBookViewModel : BaseViewModel
    {
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;
        private string nameText;
        private string authorText;
        private string genreText;
        private string yearText;
        private string nameWarningText;
        private string authorWarningText;
        private string genreWarningText;
        private string yearWarningText;
        private List<Book> bookListSource;
        private List<Genre> genreListSource;
        private ObservableCollection<Author> authorListSource;
        private RelayCommand saveCommand;
        private Author selectedBookAuthor;
        private Genre selectedBookGenre;
        private bool saveButtonEnabled;
        private MainViewModel _mainViewModel;

        public string NameText
        {
            get { return nameText; }
            set
            {
                nameText = value;
                OnPropertyChanged("NameText");
            }
        }
        public string AuthorText
        {
            get { return authorText; }
            set
            {
                authorText = value;
                OnPropertyChanged("AuthorText");
            }
        }
        public string GenreText
        {
            get { return genreText; }
            set
            {
                genreText = value;
                OnPropertyChanged("GenreText");
            }
        }
        public string YearText
        {
            get { return yearText; }
            set
            {
                yearText = value; OnPropertyChanged("YearText");
            }
        }
        public string NameWarningText
        {
            get { return nameWarningText; }
            set
            {
                nameWarningText = value; OnPropertyChanged("NameWarningText");
            }
        }
        public string AuthorWarningText
        {
            get { return authorWarningText; }
            set
            {
                authorWarningText = value; OnPropertyChanged("AuthorWarningText");
            }
        }
        public string GenreWarningText
        {
            get { return genreWarningText; }
            set
            {
                genreWarningText = value;
                OnPropertyChanged("GenreWarningText");
            }
        }
        public string YearWarningText
        {
            get { return yearWarningText; }
            set
            {
                yearWarningText = value;
                OnPropertyChanged("YearWarningText");
            }
        }
        public Author SelectedBookAuthor
        {
            get { return selectedBookAuthor; }
            set
            {
                selectedBookAuthor = value;
                OnPropertyChanged("SelectedBookAuthor");
                    if (selectedBookAuthor == AuthorListSource[0] || selectedBookGenre == GenreListSource[0])
                    {
                        SaveButtonEnabled = false;
                    }
                    else
                    {
                        SaveButtonEnabled = true;
                    }
            }
        }
        public bool SaveButtonEnabled { get { return saveButtonEnabled; } set { saveButtonEnabled = value; OnPropertyChanged("SaveButtonEnabled"); } }
        public Genre SelectedBookGenre
        {
            get { return selectedBookGenre; }
            set
            {
                selectedBookGenre = value;
                OnPropertyChanged("SelectedBookGenre");
                if (selectedBookGenre == GenreListSource[0] || SelectedBookAuthor == AuthorListSource[0])
                {
                    SaveButtonEnabled = false;
                }
                else
                {
                    SaveButtonEnabled = true;
                }
            }
        }
        public List<Book> BookListSource
        {
            get { return bookListSource; }
            set
            {
                bookListSource = value;
                OnPropertyChanged("BookListSource");
            }
        }
        public List<Genre> GenreListSource
        {
            get { return genreListSource; }
            set
            {
                genreListSource = value;
                OnPropertyChanged("GenreListSource");
            }
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
        public RelayCommand SaveCommand { get { return saveCommand ?? (saveCommand = new RelayCommand(obj => SaveCommandMethod())); } }
        public AddBookViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB, MainViewModel mainViewModel)
        {
            _bookDB = bookDB;
            _authorDB = authorDB;
            _genreDB = genreDB;
            _ = GetAuthors();
            _ = GetGenres();
            _mainViewModel = mainViewModel;
        }
        public int ValidateBook(Book book)
        {
            NameWarningText = string.Empty;
            AuthorWarningText = string.Empty;
            GenreWarningText = string.Empty;
            YearWarningText = string.Empty;
            if (string.IsNullOrWhiteSpace(book.Name))
            {
                NameWarningText = "*fill the name field";
                return 2;
            }
            else if (string.IsNullOrWhiteSpace(book.Author))
            {
                AuthorWarningText = "*fill the author field";
                return 3;
            }
            else if (string.IsNullOrEmpty(book.Genre))
            {
                GenreWarningText = "*fill the genre field";
                return 4;
            }
            else if (book.Release == 0)
            {
                YearWarningText = "*fill the year field with YYYY (e.g. 1984)";
                return 5;
            }
            else
            {
                return 1;
            }
        }
        public async Task GetAuthors()
        {
            AuthorListSource = await _authorDB.GetAuthorsAsync();
            SelectedBookAuthor = AuthorListSource[0];
        }
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
            SelectedBookGenre = GenreListSource[0];
        }
        public async void SaveCommandMethod()
        {
            Book book = new Book();
            book.Name = NameText;
            book.Author = (SelectedBookAuthor as Author).Name;
            book.Genre = (SelectedBookGenre as Genre).Name;
            int result;
            bool releaseCheck = Int32.TryParse(YearText, out result);
            if (releaseCheck)
            {
                book.Release = result;
            }
            else
            {
                book.Release = 0;
            }
            int validateNum = ValidateBook(book);
            if (validateNum == 1)
            {
                int number = await _bookDB.AddBook(book);
                MessageBox.Show($"Added {number} object.");
                _mainViewModel.CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, _mainViewModel));
            }
        }
    }
}
