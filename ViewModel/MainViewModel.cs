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

namespace Library_wpf.ViewModelNameSpace
{
    class MainViewModel : INotifyPropertyChanged
    {

        //fields
        public event PropertyChangedEventHandler? PropertyChanged;
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;
        private RelayCommand addBookCommand;
        private RelayCommand deleteBookCommand;
        private RelayCommand editBookCommand;
        private RelayCommand saveCommand;
        private RelayCommand sortByNameCommand;
        private RelayCommand selectionChangedCommand;
        private string nameText;
        private string authorText;
        private string genreText;
        private string yearText;
        private string authorNameText;
        private string genreNameText;
        private string authorBirthdayText;
        private string authorEmailText;
        private string authorMobileText;
        private bool saveButtonEnabled;
        private bool editButtonEnabled;
        private bool deleteButtonEnabled;
        private bool addButtonEnabled;
        private bool addAuthorButtonEnabled;
        private bool addGenreButtonEnabled;
        private bool deleteAuthorButtonEnabled;
        private bool deleteGenreButtonEnabled;
        private bool editAuthorButtonEnabled;
        private bool editGenreButtonEnabled;
        private bool saveAuthorButtonEnabled;
        private bool saveGenreButtonEnabled;
        private bool isAddAuthorButtonClicked;
        private bool isAddGenreButtonClicked;
        private bool isDeleteAuthorButtonClicked;
        private bool isDeleteGenreButtonClicked;
        private bool isEditAuthorButtonClicked;
        private bool isEditGenreButtonClicked;
        private bool isSaveAuthorButtonClicked;
        private bool isSaveGenreButtonClicked;
        private bool isAddBookButtonClicked;
        private bool isDeleteBookButtonClicked;
        private bool isEditBookButtonClicked;
        private bool isButton1Clicked;
        private string nameWarningText;
        private string authorWarningText;
        private string genreWarningText;
        private string yearWarningText;
        private string genreNameWarningText;
        private string authorNameWarningText;
        private string authorMobileWarningText;
        private string authorEmailWarningText;
        private string authorBirthdayWarningText;
        private bool nameTextBoxFocus;
        private object selectedBook;
        private object selectedAuthor;
        private object selectedGenre;
        private bool dynamicVisGridEnabled;
        private List<Book> bookListSource;
        private List<Author> authorListSource;
        private List<Genre> genreListSource;
        private RelayCommand sortByAuthorCommand;
        private RelayCommand sortByGenreCommand;
        private RelayCommand sortByYearCommand;
        private RelayCommand addAuthorCommand;
        private RelayCommand editAuthorCommand;
        private RelayCommand deleteAuthorCommand;
        private RelayCommand saveAuthorCommand;
        private RelayCommand addGenreCommand;
        private RelayCommand editGenreCommand;
        private RelayCommand deleteGenreCommand;
        private RelayCommand saveGenreCommand;

        //properties
        public string NameText { get { return nameText; } 
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
        public string YearText { get { return yearText; } 
            set 
            { 
                yearText = value; OnPropertyChanged("YearText");
            }
        }
        public string AuthorNameText { get { return authorNameText; } 
            set 
            {
                authorNameText = value;
                OnPropertyChanged("AuthorNameText");
            } 
        }
        public string AuthorBirthdayText { get { return authorBirthdayText; } 
            set 
            {
                authorBirthdayText = value;
                OnPropertyChanged("AuthorBirthdayText");
            } 
        }
        public string AuthorMobileText { get { return authorMobileText; } 
            set 
            {
                authorMobileText = value;
                OnPropertyChanged("AuthorMobileText");
            } 
        }
        public string AuthorEmailText { get { return authorEmailText; } 
            set 
            {
                authorEmailText = value;
                OnPropertyChanged("AuthorEmailText");
            } 
        }
        public string GenreNameText { get { return genreNameText; } 
            set 
            {
                genreNameText = value;
                OnPropertyChanged("GenreNameText");
            } 
        }
        public bool NameTextBoxFocus { get { return nameTextBoxFocus; } 
            set 
            {
                nameTextBoxFocus = value;
                OnPropertyChanged("NameTextBoxFocus");
            } 
        }
        public string NameWarningText { get { return nameWarningText; } 
            set
            {
                nameWarningText = value; OnPropertyChanged("NameWarningText");
            }
        }
        public string AuthorWarningText { get { return authorWarningText; } 
            set
            {
                authorWarningText = value; OnPropertyChanged("AuthorWarningText");
            }
        }
        public string GenreWarningText { get { return genreWarningText; } 
            set
            {
                genreWarningText = value; 
                OnPropertyChanged("GenreWarningText");
            }
        }
        public string YearWarningText { get { return yearWarningText; }
            set 
            {
                yearWarningText = value;
                OnPropertyChanged("YearWarningText");
            }    
        }
        public string GenreNameWarningText { get { return genreNameWarningText; } 
            set 
            {
                genreNameWarningText = value; 
                OnPropertyChanged("GenreNameWarningText");
            } 
        }
        public string AuthorNameWarningText { get { return authorNameWarningText; } 
            set 
            {
                authorNameWarningText = value;
                OnPropertyChanged("AuthorNameWarningText");
            } 
        }
        public string AuthorEmailWarningText { get { return authorEmailWarningText; } 
            set 
            {
                authorEmailWarningText = value;
                OnPropertyChanged("AuthorEmailWarningText");
            } 
        }
        public string AuthorMobileWarningText { get { return authorMobileWarningText; } 
            set 
            {
                authorMobileWarningText = value;
                OnPropertyChanged("AuthorMobileWarningText");
            } 
        }
        public string AuthorBirthdayWarningText { get { return authorBirthdayWarningText; } 
            set 
            {
                authorBirthdayWarningText = value; 
                OnPropertyChanged("AuthorBirthdayWarningText"); 
            } 
        }
        public bool AddButtonEnabled { get { return addButtonEnabled; } 
            set
            {
                addButtonEnabled = value; 
                OnPropertyChanged("AddButtonEnabled");
            }
        }
        public bool EditButtonEnabled { get { return editButtonEnabled; } 
            set
            {
                editButtonEnabled = value;
                OnPropertyChanged("EditButtonEnabled");
            }
        }
        public bool DeleteButtonEnabled { get { return deleteButtonEnabled; }
            set
            {
                deleteButtonEnabled = value;
                OnPropertyChanged("DeleteButtonEnabled");
            }
        }
        public bool SaveButtonEnabled { get { return saveButtonEnabled; } 
        set
            {
                saveButtonEnabled = value;
                OnPropertyChanged("SaveButtonEnabled");
            }
        }
        public bool AddAuthorButtonEnabled { get { return addAuthorButtonEnabled; } 
            set 
            { 
                addAuthorButtonEnabled = value;
                OnPropertyChanged("AddAuthorButtonEnabled");
            } 
        }
        public bool AddGenreButtonEnabled { get { return addGenreButtonEnabled; } 
            set 
            {
                addGenreButtonEnabled = value;
                OnPropertyChanged("AddGenreButtonEnabled");
            } 
        }
        public bool EditAuthorButtonEnabled { get { return editAuthorButtonEnabled; } 
            set 
            {
                editAuthorButtonEnabled = value;
                OnPropertyChanged("EditAuthorButtonEnabled");
            } 
        }
        public bool EditGenreButtonEnabled { get { return editGenreButtonEnabled; } 
            set 
            { 
                editGenreButtonEnabled = value;
                OnPropertyChanged("EditGenreButtonEnabled");
            } 
        }
        public bool DeleteGenreButtonEnabled { get { return deleteGenreButtonEnabled; } 
            set 
            { 
                deleteGenreButtonEnabled = value;
                OnPropertyChanged("DeleteGenreButtonEnabled");
            } 
        }
        public bool DeleteAuthorButtonEnabled { get { return deleteAuthorButtonEnabled; } 
            set
            {
                deleteAuthorButtonEnabled = value;
                OnPropertyChanged("DeleteAuthorButtonEnabled");
            } 
        }
        public bool SaveAuthorButtonEnabled { get { return saveAuthorButtonEnabled; } 
            set 
            { 
                saveAuthorButtonEnabled = value;
                OnPropertyChanged("SaveAuthorButtonEnabled");
            } 
        }
        public bool SaveGenreButtonEnabled { get { return saveGenreButtonEnabled; } 
            set 
            {
                saveGenreButtonEnabled = value;
                OnPropertyChanged("SaveGenreButtonEnabled");
            } 
        }
        public bool DynamicVisGridEnabled { get { return dynamicVisGridEnabled; } 
            set
            {
                dynamicVisGridEnabled = value;
                OnPropertyChanged("DynamicVisGridEnabled");
            }
        }
        public object SelectedBook { get { return selectedBook; } 
            set 
            {
                selectedBook = value;
                if(selectedBook == null)
                {
                    ClearBookText();
                    AddButtonEnabled = true;
                    EditButtonEnabled = false;
                    DeleteButtonEnabled = false;
                } 
                else
                {
                    AddButtonEnabled = false;
                    EditButtonEnabled = true;
                    DeleteButtonEnabled = true;
                }
                OnPropertyChanged("SelectedBook");
            } 
        }
        public object SelectedAuthor { get { return selectedAuthor; } 
            set 
            {
                selectedAuthor = value;
                if(selectedAuthor == null)
                {
                    AddAuthorButtonEnabled = true;
                    EditAuthorButtonEnabled  = false;
                    DeleteAuthorButtonEnabled = false;
                } 
                else
                {
                    AddAuthorButtonEnabled= false;
                    EditAuthorButtonEnabled = true;
                    DeleteAuthorButtonEnabled = true;
                }
                OnPropertyChanged("SelectedAuthor");
            } 
        }
        public object SelectedGenre { get { return selectedGenre; } 
            set 
            {
                selectedGenre = value;
                if(selectedGenre == null) 
                {
                    AddGenreButtonEnabled = true;
                    EditGenreButtonEnabled = false;
                    DeleteGenreButtonEnabled = false;
                }
                else
                {
                    AddGenreButtonEnabled= false;
                    EditGenreButtonEnabled= true;
                    DeleteGenreButtonEnabled = true;
                }
                OnPropertyChanged("SelectedGenre");
            } 
        }
        public List<Book> BookListSource { get { return bookListSource; }
            set 
            {
                bookListSource = value;
                OnPropertyChanged("BookListSource");
            }
        }
        public List<Author> AuthorListSource { get { return authorListSource; } 
            set
            {
                authorListSource = value;
                OnPropertyChanged("AuthorListSource");
            }
        }
        public List<Genre> GenreListSource { get { return genreListSource; }
            set
            {
                genreListSource = value;
                OnPropertyChanged("GenreListSource");
            }
        }
        public RelayCommand AddBookCommand { get { return addBookCommand ?? (addBookCommand = new RelayCommand(obj => AddBookCommandMethod())); }}
        public RelayCommand DeleteBookCommand { get { return deleteBookCommand ?? (deleteBookCommand = new RelayCommand(obj => DeleteBookCommandMethod())); }}
        public RelayCommand EditBookCommand { get { return editBookCommand ?? (editBookCommand = new RelayCommand(obj => EditBookCommandMethod())); }}
        public RelayCommand SaveCommand{ get { return saveCommand ?? (saveCommand = new RelayCommand(obj => SaveCommandMethod())); }}
        public RelayCommand SortByNameCommand { get { return sortByNameCommand ?? (sortByNameCommand = new RelayCommand(obj => SortByNameCommandMethod())); }}
        public RelayCommand SortByAuthorCommand { get { return sortByAuthorCommand ?? (sortByAuthorCommand = new RelayCommand(obj => SortByAuthorCommandMethod())); }}
        public RelayCommand SortByGenreCommand { get { return sortByGenreCommand ?? (sortByGenreCommand = new RelayCommand(obj => SortByGenreCommandMethod())); }}
        public RelayCommand SortByYearCommand { get { return sortByYearCommand ?? (sortByYearCommand = new RelayCommand(obj => SortByYearCommandMethod())); }}
        public RelayCommand SaveAuthorCommand { get { return saveAuthorCommand ?? (saveAuthorCommand = new RelayCommand(obj => SaveAuthorCommandMethod())); }}
        public RelayCommand SaveGenreCommand { get { return saveGenreCommand ?? (saveGenreCommand = new RelayCommand(obj => SaveGenreCommandMethod())); }}
        public RelayCommand EditAuthorCommand { get { return editAuthorCommand ?? (editAuthorCommand = new RelayCommand(obj =>  EditAuthorCommandMethod())); }}
        public RelayCommand EditGenreCommand { get { return editGenreCommand ?? (editGenreCommand = new RelayCommand(obj => EditGenreCommandMethod())); }}
        public RelayCommand AddAuthorCommand { get { return addAuthorCommand ?? (addAuthorCommand = new RelayCommand(obj => AddAuthorCommandMethod())); }}
        public RelayCommand AddGenreCommand { get { return addGenreCommand ?? (addGenreCommand = new RelayCommand(obj => AddGenreCommandMethod())); }}
        public RelayCommand DeleteAuthorCommand { get { return deleteAuthorCommand ?? (deleteAuthorCommand = new RelayCommand(obj => DeleteAuthorCommandMethod())); }}
        public RelayCommand DeleteGenreCommand { get { return deleteGenreCommand ?? (deleteGenreCommand = new RelayCommand(obj => DeleteAuthorCommandMethod())); }}
        public bool isNameSortClicked = false;
        public bool isAuthorSortClicked = false;
        public bool isGenreSortClicked = false;
        public bool isYearSortClicked = false;
        private List<Book> booksToSort = new List<Book>();

        public MainViewModel(IBookDB bookDB, IAuthorDB authorDB, IGenreDB genreDB)
        {
            _authorDB = authorDB;
            _genreDB = genreDB;
            _bookDB = bookDB;
            _ = GetBooks();
            _ = GetAuthors();
            _ = GetGenres();
            AddButtonEnabled = true;
            AddGenreButtonEnabled = true;
            AddAuthorButtonEnabled = true;
        }

        //main methods
        public async Task GetBooks()
        {
            BookListSource = await _bookDB.GetBooksAsync();
        }
        public async Task GetAuthors()
        {
            AuthorListSource = await _authorDB.GetAuthorsAsync();
        }
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
        }
        public void ClearBookText()
        {
            NameWarningText = string.Empty;
            AuthorWarningText = string.Empty;
            GenreWarningText = string.Empty;
            YearWarningText = string.Empty;
            NameText = string.Empty;
            AuthorText = string.Empty;
            GenreText = string.Empty;
            YearText = string.Empty;
        }
        public void ClearAuthorText()
        {
            AuthorNameText = string.Empty;
            AuthorBirthdayText = string.Empty;
            AuthorMobileText = string.Empty;
            AuthorEmailText = string.Empty;
        }
        public void ClearGenreText()
        {
            GenreNameText = string.Empty;
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
        public int ValidateGenre(Genre genre)
        {
            if (string.IsNullOrEmpty(GenreNameText))
            {
                GenreNameWarningText = "*";
                return 2;
            }
            else
            {
                return 1;
            }
        }
        public int ValidateAuthor(Author author)
        {
            if (string.IsNullOrEmpty(AuthorNameText))
            {
                AuthorNameWarningText = "*";
                return 2;
            } 
            else if (string.IsNullOrEmpty(AuthorEmailText)) 
            {
                AuthorEmailWarningText = "*";
                return 3;
            }
            else if (string.IsNullOrEmpty(AuthorBirthdayText))
            {
                AuthorBirthdayWarningText = "*";
                return 4;
            }
            else if (string.IsNullOrEmpty(AuthorMobileText))
            {
                AuthorMobileWarningText = "*";
                return 5;
            } 
            else 
            {
                return 1; 
            }
        }

        //book button commands
        public async void SaveCommandMethod()
        {
            isButton1Clicked = true;
            Book book = new Book();
            book.Name = NameText;
            book.Author = AuthorText;
            book.Genre = GenreText;
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
                if (isAddBookButtonClicked)
                {
                    int number = await _bookDB.AddBook(book);
                    MessageBox.Show($"Added {number} object.");
                    isButton1Clicked = false;
                    DynamicVisGridEnabled = false;
                    ClearBookText();
                    isAddBookButtonClicked = false;
                }
                else if (isEditBookButtonClicked)
                {
                    string dataChoicePrepared = "";
                    Book oldBook = SelectedBook as Book;
                    int number = await _bookDB.EditBook(oldBook, book);
                    MessageBox.Show($"Modified {number} object.");
                    isEditBookButtonClicked = false;
                    DynamicVisGridEnabled = false;
                    ClearBookText();
                }
                isButton1Clicked = false;
                EditButtonEnabled = false;
                DeleteButtonEnabled = false;
                _ = GetBooks();
                SaveButtonEnabled = false;
            }
            else
            {
                isButton1Clicked = false;
            }
        }
        public void EditBookCommandMethod()
        {
            isEditBookButtonClicked = true;
            isAddBookButtonClicked = false;
            DynamicVisGridEnabled = true;
            Book oldBook = SelectedBook as Book;
            NameText = oldBook.Name;
            AuthorText = oldBook.Author;
            GenreText = oldBook.Genre;
            YearText = oldBook.Release.ToString();
            SaveButtonEnabled = true;
        }
        public async void DeleteBookCommandMethod()
        {
            if (SelectedBook != null)
            {
                int deleteResult = 0;
                DynamicVisGridEnabled = false;
                ClearBookText();
                Book selectedItem = SelectedBook as Book;
                deleteResult = await _bookDB.DeleteBook(selectedItem);
                DeleteButtonEnabled = false;
                EditButtonEnabled = false;
                _ = GetBooks();
                MessageBox.Show($"Deleted {deleteResult} object(s).");
            }
        }
        public void AddBookCommandMethod()
        {
            NameText = string.Empty;
            AuthorText = string.Empty;
            GenreText = string.Empty;
            YearText = string.Empty;
            DynamicVisGridEnabled = true;
            NameTextBoxFocus = true;
            isAddBookButtonClicked = true;
            isEditBookButtonClicked = false;
            SaveButtonEnabled = true;
        }
        
        //sort commands
        public void SortByNameCommandMethod()
        {
            if (isNameSortClicked)
            {
                isNameSortClicked = false;
                _ = GetBooks();
            }
            else
            {
                isAuthorSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = false;
                isNameSortClicked = true;
                booksToSort = BookListSource as List<Book>;
                booksToSort.Sort((x, y) => string.Compare(x.Name, y.Name));
                BookListSource = new List<Book>();
                BookListSource = booksToSort;
            }
        }
        public void SortByAuthorCommandMethod()
        {
            if (isAuthorSortClicked)
            {
                isAuthorSortClicked = false;
                _ = GetBooks();
            }
            else
            {
                isNameSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = false;
                isAuthorSortClicked = true;
                booksToSort = BookListSource;
                booksToSort.Sort((x, y) => string.Compare(x.Author, y.Author));
                BookListSource = new List<Book>();
                BookListSource = booksToSort;
            }
        }
        public void SortByGenreCommandMethod()
        {
            if (isGenreSortClicked)
            {
                isGenreSortClicked = false;
                _ = GetBooks();
            }
            else
            {
                isNameSortClicked = false;
                isAuthorSortClicked = false;
                isYearSortClicked = false;
                isGenreSortClicked = true;
                booksToSort = BookListSource;
                booksToSort.Sort((x, y) => string.Compare(x.Genre, y.Genre));
                BookListSource = new List<Book>();
                BookListSource = booksToSort;
            }
        }
        public void SortByYearCommandMethod()
        {
            if (isYearSortClicked)
            {
                isYearSortClicked = false;
                _ = GetBooks();
            }
            else
            {
                isNameSortClicked = false;
                isAuthorSortClicked = false;
                isGenreSortClicked = false;
                isYearSortClicked = true;
                booksToSort = BookListSource;
                booksToSort = booksToSort.OrderByDescending(x => x.Release).ToList();
                BookListSource = new List<Book>();
                BookListSource = booksToSort;
            }
        }

        //author and genre button commands
        public void SaveAuthorCommandMethod()
        {

        }
        public void SaveGenreCommandMethod()
        {

        }
        public void AddAuthorCommandMethod()
        {

        }
        public void AddGenreCommandMethod()
        {

        }
        public void EditAuthorCommandMethod()
        {

        }
        public void EditGenreCommandMethod()
        {

        }
        public void DeleteAuthorCommandMethod()
        {

        }
        public void DeleteGenreCommandMethod()
        {

        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
