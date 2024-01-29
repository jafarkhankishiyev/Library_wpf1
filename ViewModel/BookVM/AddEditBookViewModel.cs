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
    public class AddEditBookViewModel : BaseViewModel
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
        private Book selectedBook;
        private Author selectedBookAuthor;
        private Genre selectedBookGenre;
        private bool saveButtonEnabled;
        private MainViewModel _mainViewModel;
        private bool addAnotherAuthorButtonEnabled;
        private bool deleteAnotherAuthorButtonEnabled;
        private bool addAnotherGenreButtonEnabled;
        private bool deleteAnotherGenreButtonEnabled;
        private RelayCommand addAnotherAuthorCommand;
        private RelayCommand deleteAnotherAuthorCommand;
        private RelayCommand addAnotherGenreCommand;
        private RelayCommand deleteAnotherGenreCommand;
        private string selectedAuthorsText;
        private string selectedGenresText;
        private ObservableCollection<Author> selectedAuthors;
        private ObservableCollection<Genre> selectedGenres;

        public bool AddAnotherAuthorButtonEnabled { get { return addAnotherAuthorButtonEnabled; } set {  addAnotherAuthorButtonEnabled = value; OnPropertyChanged("AddAnotherAuthorButtonEnabled"); } }
        public bool DeleteAnotherAuthorButtonEnabled { get { return deleteAnotherAuthorButtonEnabled; } set { deleteAnotherAuthorButtonEnabled = value; OnPropertyChanged("DeleteAnotherAuthorButtonEnabled"); } }
        public bool AddAnotherGenreButtonEnabled { get { return addAnotherGenreButtonEnabled; } set { addAnotherGenreButtonEnabled = value; OnPropertyChanged("AddAnotherGenreButtonEnabled"); } }
        public bool DeleteAnotherGenreButtonEnabled { get { return deleteAnotherGenreButtonEnabled; } set { deleteAnotherGenreButtonEnabled = value; OnPropertyChanged("DeleteAnotherGenreButtonEnabled"); } }
        public string SelectedAuthorsText { get { return selectedAuthorsText; } set { selectedAuthorsText = value; OnPropertyChanged("SelectedAuthorsText"); } }
        public string SelectedGenresText { get { return selectedGenresText; } set { selectedGenresText = value; OnPropertyChanged("SelectedGenresText"); } }
        private ObservableCollection<Author> SelectedAuthors { get { return selectedAuthors; } 
            set 
            { 
                selectedAuthors = value; 
                OnPropertyChanged("SelectedAuthors"); 
                if(selectedAuthors.Count > 0 && SelectedGenres.Count > 0)
                {
                    SaveButtonEnabled = true;
                }
                else
                {
                    SaveButtonEnabled = false;
                }
                ShowSelectedAuthors();
            } 
        }
        private ObservableCollection<Genre> SelectedGenres { get { return selectedGenres; } 
            set 
            { 
                selectedGenres = value; 
                OnPropertyChanged("SelectedGenres");
                if (selectedGenres.Count > 0 && SelectedAuthors.Count > 0)
                {
                    SaveButtonEnabled = true;
                }
                else
                {
                    SaveButtonEnabled = false;
                }
                ShowSelectedGenres();
            } 
        }
        public RelayCommand AddAnotherAuthorCommand { get { return addAnotherAuthorCommand ?? (addAnotherAuthorCommand = new RelayCommand(obj => AddAnotherAuthorCommandMethod())); } }
        public RelayCommand DeleteAnotherAuthorCommand { get { return deleteAnotherAuthorCommand ?? (deleteAnotherAuthorCommand = new RelayCommand(obj => DeleteAnotherAuthorCommandMethod())); } }
        public RelayCommand AddAnotherGenreCommand { get { return addAnotherGenreCommand ?? (addAnotherGenreCommand = new RelayCommand(obj => AddAnotherGenreCommandMethod())); } }
        public RelayCommand DeleteAnotherGenreCommand { get { return deleteAnotherGenreCommand ?? (deleteAnotherGenreCommand = new RelayCommand(obj => DeleteAnotherGenreCommandMethod())); } }
        public Book SelectedBook { get { return selectedBook; } set { selectedBook = value; OnPropertyChanged("SelectedBook"); } }
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
                if(selectedBookAuthor != AuthorListSource[0])
                {
                    AddAnotherAuthorButtonEnabled = true;
                }
                else
                {
                    AddAnotherAuthorButtonEnabled = false;
                }
                /*if ((selectedBookAuthor != AuthorListSource[0] || SelectedAuthors.Count != 0) && (SelectedBookGenre != GenreListSource[0] || SelectedGenres.Count != 0))
                {
                    SaveButtonEnabled = true;
                }
                else
                {
                    SaveButtonEnabled = false;
                }
                if(selectedBookAuthor != AuthorListSource[0])
                {
                    AddAnotherAuthorButtonEnabled = true;
                }   
                else 
                { 
                    AddAnotherAuthorButtonEnabled = false; 
                }   */
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
                if(selectedBookGenre != GenreListSource[0])
                {
                    AddAnotherGenreButtonEnabled = true;
                }
                else
                {
                    AddAnotherGenreButtonEnabled = false;
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
        public AddEditBookViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB, MainViewModel mainViewModel)
        {
            _bookDB = bookDB;
            _authorDB = authorDB;
            _genreDB = genreDB;
            _ = GetAuthors();
            _ = GetGenres();
            _mainViewModel = mainViewModel;
            SelectedGenres = new ObservableCollection<Genre>();
            SelectedAuthors = new ObservableCollection<Author>();
        }
        public AddEditBookViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB, MainViewModel mainViewModel, Book selectedBook)
        {
            _bookDB = bookDB;
            _authorDB = authorDB;
            _genreDB = genreDB;
            SelectedAuthors = [];
            SelectedGenres = [];
            _ = GetAuthors();
            _ = GetGenres();
            _bookDB = bookDB;
            _authorDB = authorDB;
            _genreDB = genreDB;
            _mainViewModel = mainViewModel;
            SelectedBook = selectedBook;
            NameText = SelectedBook.Name;
            YearText = SelectedBook.Release.ToString();
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
            if(SelectedBook != null) 
            {
                foreach(Author author in AuthorListSource) 
                {
                    if(author.Name == SelectedBook.Author) 
                    { 
                        SelectedBookAuthor = author;
                        MessageBox.Show("Yes");
                    }
                }
                while (SelectedBook.Author.Contains(','))
                {
                    string authorToInput = SelectedBook.Author.Substring(0, SelectedBook.Author.IndexOf(','));
                    authorToInput = authorToInput.TrimStart();
                    SelectedBook.Author = SelectedBook.Author.Remove(0, SelectedBook.Author.IndexOf(',') + 1);
                    foreach (Author author in AuthorListSource)
                    {
                        if (author.Name == authorToInput)
                        {
                            SelectedAuthors.Add(author);
                        }
                    }
                }
                string lastAuthorToInput = SelectedBook.Author.TrimStart();
                foreach (Author author in AuthorListSource)
                {
                    if (author.Name == lastAuthorToInput)
                    {
                        SelectedAuthors.Add(author);
                    }
                }
            }
            else
            {
                SelectedBookAuthor = AuthorListSource[0];
            }
            if(SelectedAuthors.Count > 1)
            {
                SelectedBookAuthor = AuthorListSource[0];
            }
            ShowSelectedAuthors();
        }
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
            if(SelectedBook != null)
            {
                foreach(Genre genre in GenreListSource)
                {
                    if(genre.Name == SelectedBook.Genre)
                    {
                        SelectedBookGenre = genre;
                    }
                }
                while (SelectedBook.Genre.Contains(','))
                {
                    string genreToInput = SelectedBook.Genre.Substring(0, SelectedBook.Genre.IndexOf(','));
                    genreToInput = genreToInput.TrimStart();
                    SelectedBook.Genre = SelectedBook.Genre.Remove(0, SelectedBook.Genre.IndexOf(',') + 1);
                    foreach (Genre genre in GenreListSource)
                    {
                        if (genre.Name == genreToInput)
                        {
                            SelectedGenres.Add(genre);
                        }
                    }
                }
                string lastGenreToInput = SelectedBook.Genre.TrimStart();
                foreach (Genre genre in GenreListSource)
                {
                    if (genre.Name == lastGenreToInput)
                    {
                        SelectedGenres.Add(genre);
                    }
                }
            }
            else
            {
                SelectedBookGenre = GenreListSource[0];
            }
            if(SelectedGenres.Count > 0)
            {
                SelectedBookGenre = GenreListSource[0];
            }
            ShowSelectedGenres();
        }
        public void AddAnotherAuthorCommandMethod()
        {
            if(!SelectedAuthors.Contains(SelectedBookAuthor))
            {
                SelectedAuthors.Add(SelectedBookAuthor);
                SelectedBookAuthor = AuthorListSource[0];
                ShowSelectedAuthors();
            }
            else
            {
                MessageBox.Show("This author has already been added.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void DeleteAnotherAuthorCommandMethod()
        {
            SelectedAuthors.Remove(SelectedAuthors[SelectedAuthors.Count - 1]);
            ShowSelectedAuthors();
        }
        public void ShowSelectedAuthors()
        {
            if (SelectedAuthors.Count > 0)
            {
                DeleteAnotherAuthorButtonEnabled = true;
                int i = 1;
                SelectedAuthorsText = string.Empty;
                foreach (Author author in selectedAuthors)
                {
                    SelectedAuthorsText += $"{author.Name}";
                    if (selectedAuthors.Count - i > 0)
                    {
                        SelectedAuthorsText += ", \n";
                        i++;
                    }
                }
            }
            else
            {
                SelectedAuthorsText = string.Empty;
                DeleteAnotherAuthorButtonEnabled = false;
            }
            if(SelectedAuthorsText != string.Empty)
            {
                SaveButtonEnabled = true;
            }
            else { SaveButtonEnabled = false; }
        }
        public void AddAnotherGenreCommandMethod()
        {
            if (!SelectedGenres.Contains(SelectedBookGenre))
            {
                SelectedGenres.Add(SelectedBookGenre);
                SelectedBookGenre = GenreListSource[0];
                ShowSelectedGenres();
            }
            else
            {
                MessageBox.Show("This genre has already been added.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void DeleteAnotherGenreCommandMethod()
        {
            SelectedGenres.Remove(SelectedGenres[SelectedGenres.Count() - 1]);
            ShowSelectedGenres();
        }
        public void ShowSelectedGenres()
        {
            if (SelectedGenres.Count > 0)
            {
                int i = 1;
                DeleteAnotherGenreButtonEnabled = true;
                SelectedGenresText = string.Empty;
                foreach (Genre genre in SelectedGenres)
                {
                    SelectedGenresText += $"{genre.Name}";
                    if (SelectedGenres.Count - i > 0)
                    {
                        SelectedGenresText += ", \n";
                        i++;
                    }
                }
            }
            else
            {
                SelectedGenresText = string.Empty;
                DeleteAnotherGenreButtonEnabled = false;
            }
            if(SelectedGenresText !=  string.Empty)
            {
                SaveButtonEnabled = true;
            } 
            else { SaveButtonEnabled = false; }
        }
        public async void SaveCommandMethod()
        {
            Book book = new Book();
            book.Name = NameText;
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
            book.Author = SelectedBookAuthor.Id.ToString();
            book.Genre = SelectedBookGenre.Id.ToString();
            int validateNum = ValidateBook(book);
            if(SelectedAuthors == null && SelectedGenres == null) 
            {    
                if (validateNum == 1)
                {
                    if(SelectedBook != null)
                    {
                        int number = await _bookDB.EditBook(SelectedBook, book);
                        MessageBox.Show($"Modified {number} object.");
                        _mainViewModel.CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, _mainViewModel));
                    }
                    else 
                    { 
                        int number = await _bookDB.AddBook(book);
                        MessageBox.Show($"Added {number} object.");
                        _mainViewModel.CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, _mainViewModel));
                    }
                }
            }
            else
            {
                if(validateNum == 1)
                {
                    if (SelectedBook != null)
                    {
                        int number = await _bookDB.EditBook(SelectedBook, book, SelectedAuthors, SelectedGenres);
                        number = number > 0 ? 1: 0;
                        MessageBox.Show($"Modified {number} object.");
                        _mainViewModel.CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, _mainViewModel));
                    }
                    else
                    {
                        int number = await _bookDB.AddBook(book, SelectedAuthors, SelectedGenres);
                        number = number > 0 ? 1 : 0;
                        MessageBox.Show($"Added {number} object.");
                        _mainViewModel.CurrentView = new BookView(new BookViewModel(_bookDB, _genreDB, _authorDB, _mainViewModel));
                    }
                }
            }
        }
    }
}
