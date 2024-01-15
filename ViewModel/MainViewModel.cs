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

namespace Library_wpf.ViewModelNameSpace
{
    class MainViewModel : INotifyPropertyChanged
    {

        //fields
        public event PropertyChangedEventHandler? PropertyChanged;
        private IBookDB _bookDB;
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
        private bool saveButtonEnabled;
        private bool editButtonEnabled;
        private bool deleteButtonEnabled;
        private bool addButtonEnabled;
        private bool isAddBookButtonClicked;
        private bool isDeleteBookButtonClicked;
        private bool isEditBookButtonClicked;
        private bool isButton1Clicked;
        private string nameWarningText;
        private string authorWarningText;
        private string genreWarningText;
        private string yearWarningText;
        private bool nameTextBoxFocus;
        private object selectedBook;
        private List<object> selectedBooks;
        private bool dynamicVisGridEnabled;
        private List<Book> bookListSource;
        private bool BookList_SelectionChanged;
        private RelayCommand sortByAuthorCommand;
        private RelayCommand sortByGenreCommand;
        private RelayCommand sortByYearCommand;

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
                    ClearText();
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
        public List<Book> BookListSource { get { return bookListSource; }
            set {
                bookListSource = value;
                OnPropertyChanged("BookListSource");
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
        public bool isNameSortClicked = false;
        public bool isAuthorSortClicked = false;
        public bool isGenreSortClicked = false;
        public bool isYearSortClicked = false;
        private List<Book> booksToSort = new List<Book>();

        public MainViewModel(IBookDB bookDB)
        {
            _bookDB = bookDB;
            _ = GetBooks();
            AddButtonEnabled = true;
            EditButtonEnabled = false; 
            DeleteButtonEnabled = false;
        }
        public async Task GetBooks()
        {
            BookListSource = await _bookDB.GetBooksAsync();
        }
        public void ClearText()
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
        public int Validate(Book book)
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

        //button commands
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
            int validateNum = Validate(book);
            if (validateNum == 1)
            {
                if (isAddBookButtonClicked)
                {
                    int number = await _bookDB.AddBook(book);
                    MessageBox.Show($"Added {number} object.");
                    isButton1Clicked = false;
                    DynamicVisGridEnabled = false;
                    ClearText();
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
                    ClearText();
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
                ClearText();
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
