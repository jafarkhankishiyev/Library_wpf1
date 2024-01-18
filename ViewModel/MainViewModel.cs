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

namespace Library_wpf.ViewModelNameSpace
{
    class MainViewModel : INotifyPropertyChanged
    {

        //fields
        public event PropertyChangedEventHandler? PropertyChanged;
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;
        private List<Author> filteredAuthors;
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
        private bool authorNameTextBoxEnabled;
        private bool authorMobileTextBoxEnabled;
        private bool authorEmailTextBoxEnabled;
        private bool authorBirthdayTextBoxEnabled;
        private bool genreNameTextBoxEnabled;
        private bool authorGenreGridEnabled;
        private bool clearAuthorComboBoxEnabled;
        private bool clearGenreComboBoxEnabled;
        private bool nameTextBoxFocus;
        private object selectedBook;
        private object selectedAuthor;
        private object selectedGenre;
        private object selectedBookAuthor;
        private object selectedBookGenre;
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
        private RelayCommand clearAuthorComboBoxCommand;
        private RelayCommand clearGenreComboBoxCommand;
        private RelayCommand<ComboBox> authorComboBoxKeyDownCommand;

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
        public List<Author> FilteredAuthors { get { return filteredAuthors; } set { filteredAuthors = value; OnPropertyChanged("FilteredAuthors"); }}
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
        public bool AuthorGenreGridEnabled { get { return authorGenreGridEnabled; } set { authorGenreGridEnabled = value; OnPropertyChanged("AuthorGenreGridEnabled"); } }
        public bool ClearAuthorComboBoxEnabled { get { return clearAuthorComboBoxEnabled; } set { clearAuthorComboBoxEnabled = value; OnPropertyChanged("ClearAuthorComboBoxEnabled"); } }
        public bool ClearGenreComboBoxEnabled { get { return clearGenreComboBoxEnabled; } set { clearGenreComboBoxEnabled = value; OnPropertyChanged("ClearGenreComboBoxEnabled"); } }
        public bool AuthorNameTextBoxEnabled { get { return authorNameTextBoxEnabled; } set { authorNameTextBoxEnabled = value; OnPropertyChanged("AuthorNameTextBoxEnabled"); } }
        public bool AuthorEmailTextBoxEnabled { get { return authorEmailTextBoxEnabled; } set { authorEmailTextBoxEnabled = value; OnPropertyChanged("AuthorEmailTextBoxEnabled"); } }
        public bool AuthorBirthdayTextBoxEnabled { get { return authorBirthdayTextBoxEnabled; } set { authorBirthdayTextBoxEnabled = value; OnPropertyChanged("AuthorBirthdayTextBoxEnabled"); } }
        public bool AuthorMobileTextBoxEnabled { get { return authorMobileTextBoxEnabled; } set { authorMobileTextBoxEnabled = value; OnPropertyChanged("AuthorMobileTextBoxEnabled"); } }
        public bool GenreNameTextBoxEnabled { get { return genreNameTextBoxEnabled; } set { genreNameTextBoxEnabled = value; OnPropertyChanged("GenreNameTextBoxEnabled"); } }
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
                if(selectedAuthor == AuthorListSource[0])
                {
                    AddAuthorButtonEnabled = true;
                    EditAuthorButtonEnabled  = false;
                    DeleteAuthorButtonEnabled = false;
                    ClearAuthorComboBoxEnabled = false;
                } 
                else
                {
                    ClearBookAuthorComboBox();
                    ClearBookGenreComboBox();
                    ClearGenreComboBoxCommandMethod();
                    DynamicVisGridEnabled = false;
                    SaveButtonEnabled = false;
                    _ = GetBooks();
                    AddAuthorButtonEnabled= false;
                    EditAuthorButtonEnabled = true;
                    DeleteAuthorButtonEnabled = true;
                    ClearAuthorComboBoxEnabled = true;
                }
                OnPropertyChanged("SelectedAuthor");
            } 
        }
        public object SelectedGenre { get { return selectedGenre; } 
            set 
            {
                selectedGenre = value;
                if(selectedGenre == GenreListSource[0]) 
                {
                    AddGenreButtonEnabled = true;
                    EditGenreButtonEnabled = false;
                    DeleteGenreButtonEnabled = false;
                    ClearGenreComboBoxEnabled = false;
                }
                else
                {
                    ClearBookAuthorComboBox();
                    ClearBookGenreComboBox();
                    ClearAuthorComboBoxCommandMethod();
                    ClearBookText();
                    AddGenreButtonEnabled= false;
                    EditGenreButtonEnabled= true;
                    DeleteGenreButtonEnabled = true;
                    ClearGenreComboBoxEnabled = true;
                }
                OnPropertyChanged("SelectedGenre");
            } 
        }
        public object SelectedBookAuthor { get { return selectedBookAuthor; } 
            set 
            {
                selectedBookAuthor = value; 
                OnPropertyChanged("SelectedBookAuthor");
                if (isAddBookButtonClicked || isEditBookButtonClicked)
                {
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
        }
        public object SelectedBookGenre { get { return selectedBookGenre; } 
            set 
            { 
                selectedBookGenre = value; 
                OnPropertyChanged("SelectedBookGenre");
                if(isAddBookButtonClicked || isEditBookButtonClicked)
                {

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
        public RelayCommand DeleteGenreCommand { get { return deleteGenreCommand ?? (deleteGenreCommand = new RelayCommand(obj => DeleteGenreCommandMethod())); }}
        public RelayCommand ClearAuthorComboBoxCommand { get { return clearAuthorComboBoxCommand ?? (clearAuthorComboBoxCommand = new RelayCommand(obj => ClearAuthorComboBoxCommandMethod())); } }
        public RelayCommand ClearGenreComboBoxCommand { get { return clearGenreComboBoxCommand ?? (clearGenreComboBoxCommand = new RelayCommand(obj => ClearGenreComboBoxCommandMethod())); }}
        public RelayCommand<ComboBox> AuthorComboBoxKeyDownCommand { get { return authorComboBoxKeyDownCommand ?? (authorComboBoxKeyDownCommand = new RelayCommand<ComboBox>(AuthorComboBoxKeyDownCommandMethod)); } }

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
            ClearAuthorComboBoxCommandMethod();
            ClearBookAuthorComboBox();
        }
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
            ClearGenreComboBoxCommandMethod();
            ClearBookGenreComboBox();
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
            AuthorBirthdayWarningText = string.Empty;
            AuthorMobileWarningText = string.Empty;
            AuthorEmailWarningText = string.Empty;
            AuthorBirthdayWarningText = string.Empty;
        }
        public void ClearGenreText()
        {
            GenreNameText = string.Empty;
            GenreNameWarningText = string.Empty;
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
            GenreNameWarningText = string.Empty;
            if (string.IsNullOrEmpty(genre.Name))
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

        //book button commands
        public async void SaveCommandMethod()
        {
            isButton1Clicked = true;
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
                    int number = await _bookDB.EditBook(SelectedBook as Book, book);
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
                ClearBookGenreComboBox();
                ClearBookAuthorComboBox();
            }
            else
            {
                isButton1Clicked = false;
            }
        }
        public void EditBookCommandMethod()
        {
            ClearAuthorComboBoxCommandMethod();
            ClearGenreComboBoxCommandMethod();
            isEditBookButtonClicked = true;
            isAddBookButtonClicked = false;
            DynamicVisGridEnabled = true;
            Book oldBook = SelectedBook as Book;
            Author bookAuthor = new Author();
            bookAuthor.Name = (SelectedBook as Book).Author;
            Genre bookGenre = new Genre();
            bookGenre.Name = (SelectedBook as Book).Genre;
            foreach (Author author in AuthorListSource)
            {
                if(author.Name == bookAuthor.Name)
                {
                    SelectedBookAuthor = author;
                }
            }
            foreach (Genre genre in GenreListSource)
            {
                if(genre.Name == bookGenre.Name)
                {
                    SelectedBookGenre = genre;
                }
            }
            NameText = oldBook.Name;
            YearText = oldBook.Release.ToString();
        }
        public async void DeleteBookCommandMethod()
        {
            ClearAuthorComboBoxCommandMethod();
            ClearGenreComboBoxCommandMethod();
            if (SelectedBook != null)
            {
                int deleteResult = 0;
                DynamicVisGridEnabled = false;
                ClearBookText();
                deleteResult = await _bookDB.DeleteBook(SelectedBook as Book);
                DeleteButtonEnabled = false;
                EditButtonEnabled = false;
                _ = GetBooks();
                MessageBox.Show($"Deleted {deleteResult} object(s).");
            }
        }
        public void AddBookCommandMethod()
        {
            ClearAuthorComboBoxCommandMethod();
            ClearGenreComboBoxCommandMethod();
            ClearBookText();
            DynamicVisGridEnabled = true;
            NameTextBoxFocus = true;
            isAddBookButtonClicked = true;
            isEditBookButtonClicked = false;
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
                ClearAuthorComboBoxCommandMethod();
                ClearAuthorText();
            }
            else
            {
                isSaveAuthorButtonClicked = false;
            }
        }
        public async void SaveGenreCommandMethod()
        {
            isSaveGenreButtonClicked = true;
            Genre newGenre = new Genre();
            newGenre.Name = GenreNameText;
            int validateNum = ValidateGenre(newGenre);
            if (validateNum == 1)
            {
                if(isAddGenreButtonClicked)
                {
                    int number = await _genreDB.AddGenre(newGenre);
                    MessageBox.Show($"Added {number} genre.");
                    isAddGenreButtonClicked = false;
                }
                else if(isEditGenreButtonClicked)
                {
                    int number = await _genreDB.EditGenre(SelectedGenre as Genre, newGenre);
                    MessageBox.Show($"Edited {number} genre.");
                    isEditGenreButtonClicked = false;
                }
                _ = GetGenres();
                isSaveGenreButtonClicked = false;
                EditGenreButtonEnabled = false;
                DeleteGenreButtonEnabled = false;
                AddGenreButtonEnabled = true;
                ClearGenreComboBoxCommandMethod();
                ClearGenreText();
            }
            else
            {
                isSaveGenreButtonClicked = false;
            }
        }
        public void AddAuthorCommandMethod()
        {
            _ = GetBooks();
            ClearBookText();
            DynamicVisGridEnabled = false;
            SaveButtonEnabled = false; 
            ClearAuthorText();
            AuthorNameTextBoxEnabled = true;
            AuthorEmailTextBoxEnabled = true;
            AuthorMobileTextBoxEnabled = true;
            AuthorBirthdayTextBoxEnabled = true;
            isAddAuthorButtonClicked = true;
            isEditAuthorButtonClicked = false;
            SaveAuthorButtonEnabled = true;
            ClearBookAuthorComboBox();
            ClearBookGenreComboBox();
        }
        public void AddGenreCommandMethod()
        {
            _ = GetBooks();
            ClearBookText();
            ClearAuthorText();
            DynamicVisGridEnabled = false;
            SaveButtonEnabled = false;
            ClearGenreText();
            GenreNameTextBoxEnabled = true;
            isEditGenreButtonClicked = false;
            isAddGenreButtonClicked = true;
            SaveGenreButtonEnabled = true;
            ClearBookAuthorComboBox();
            ClearBookGenreComboBox();
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
        public void EditGenreCommandMethod()
        {
            Genre selectedGenre = SelectedGenre as Genre;
            GenreNameText = selectedGenre.Name;
            isEditGenreButtonClicked = true;
            isAddGenreButtonClicked = false;
            SaveGenreButtonEnabled = true;
            GenreNameTextBoxEnabled = true;
        }
        public async void DeleteAuthorCommandMethod()
        {
            Author selectedAuthor = SelectedAuthor as Author;
            int result = await _authorDB.DeleteAuthor(selectedAuthor);
            _ = GetAuthors();
            MessageBox.Show($"Deleted {result} author.");
            ClearAuthorComboBoxCommandMethod();
        }
        public async void DeleteGenreCommandMethod()
        {
            Genre selectedGenre = SelectedGenre as Genre;
            int result = await _genreDB.DeleteGenre(selectedGenre);
            _ = GetGenres();
            MessageBox.Show($"Deleted {result} genre.");
            ClearGenreComboBoxCommandMethod();
        }

        //combobox methods
        public void ClearAuthorComboBoxCommandMethod()
        {
            AuthorNameTextBoxEnabled = false;
            AuthorEmailTextBoxEnabled = false;
            AuthorBirthdayTextBoxEnabled = false;
            AuthorMobileTextBoxEnabled = false;
            ClearAuthorText();
            SelectedAuthor = AuthorListSource[0];
        }
        public void ClearGenreComboBoxCommandMethod()
        {
            GenreNameTextBoxEnabled = false;
            ClearGenreText();
            SelectedGenre = GenreListSource[0];
        }
        public void ClearBookGenreComboBox()
        {
            SelectedBookGenre = GenreListSource[0];
        }
        public void ClearBookAuthorComboBox() 
        {
            SelectedBookAuthor = AuthorListSource[0];
        }
        public void AuthorComboBoxKeyDownCommandMethod(ComboBox comboBox)
        {
            if (comboBox != null && comboBox.IsDropDownOpen)
            {
                char keyPressed = KeyInterop.VirtualKeyFromKey(e.Key).ToString().ToLower()[0];
                FilteredAuthors = new List<Author>(
                    AuthorListSource.Where(author => author.Name.ToLower()[0] == keyPressed));
            }
            AuthorListSource = FilteredAuthors;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
