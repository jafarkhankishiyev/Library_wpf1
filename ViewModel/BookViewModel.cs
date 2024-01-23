using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_wpf.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private RelayCommand addBookCommand;
        private RelayCommand deleteBookCommand;
        private RelayCommand editBookCommand;
        private RelayCommand saveCommand;
        private RelayCommand sortByNameCommand;
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
        private string nameWarningText;
        private string authorWarningText;
        private string genreWarningText;
        private string yearWarningText;
        private bool isButton1Clicked;
        private bool authorFilterEnabled;
        private bool genreCheckBoxEnabled;
        private bool genreFilterEnabled;
        private bool isGenreFilterChecked;
        private bool isAuthorFilterChecked;
        private bool dynamicVisGridEnabled;
        private object selectedBook;
        private object selectedBookAuthor;
        private object selectedBookGenre;
        private RelayCommand sortByAuthorCommand;
        private RelayCommand sortByGenreCommand;
        private RelayCommand sortByYearCommand;
        private RelayCommand searchByNameCommand;
        private Author selectedAuthorToFilter;
        private Genre selectedGenreToFilter;
        private IBookDB _bookDB;
        private IAuthorDB _authorDB;
        private IGenreDB _genreDB;
        private List<Book> bookListSource;
        private List<Book> bookListSourceCopy;
        private string searchByNameText;
        private List<Genre> genreListSource;
        private ObservableCollection<Author> authorListSource;
        private bool authorCheckBoxEnabled;

        public BookViewModel(IBookDB bookDB, IGenreDB genreDB, IAuthorDB authorDB)
        {
            _authorDB = authorDB;
            _genreDB = genreDB;
            _bookDB = bookDB;
            _ = GetBooks();
            _ = GetGenres();
            _ = GetAuthors();
            AddButtonEnabled = true;
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
        public List<Book> BookListSourceCopy { get; set; }
        public List<Book> BookListSourceMixFilter = new List<Book>();

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
        public string SearchByNameText { get { return searchByNameText; } set { searchByNameText = value; OnPropertyChanged("SearchByNameText"); } }
        public bool AddButtonEnabled
        {
            get { return addButtonEnabled; }
            set
            {
                addButtonEnabled = value;
                OnPropertyChanged("AddButtonEnabled");
            }
        }
        public bool EditButtonEnabled
        {
            get { return editButtonEnabled; }
            set
            {
                editButtonEnabled = value;
                OnPropertyChanged("EditButtonEnabled");
            }
        }
        public bool DeleteButtonEnabled
        {
            get { return deleteButtonEnabled; }
            set
            {
                deleteButtonEnabled = value;
                OnPropertyChanged("DeleteButtonEnabled");
            }
        }
        public bool SaveButtonEnabled
        {
            get { return saveButtonEnabled; }
            set
            {
                saveButtonEnabled = value;
                OnPropertyChanged("SaveButtonEnabled");
            }
        }
        public bool DynamicVisGridEnabled
        {
            get { return dynamicVisGridEnabled; }
            set
            {
                dynamicVisGridEnabled = value;
                OnPropertyChanged("DynamicVisGridEnabled");
            }
        }
        public object SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                if (selectedBook == null)
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
        public object SelectedBookAuthor
        {
            get { return selectedBookAuthor; }
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
        public object SelectedBookGenre
        {
            get { return selectedBookGenre; }
            set
            {
                selectedBookGenre = value;
                OnPropertyChanged("SelectedBookGenre");
                if (isAddBookButtonClicked || isEditBookButtonClicked)
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
        public Author SelectedAuthorToFilter
        {
            get { return selectedAuthorToFilter; }
            set
            {
                selectedAuthorToFilter = value;
                OnPropertyChanged("SelectedAuthorToFilter");
                if (selectedAuthorToFilter != AuthorListSource[0] && selectedGenreToFilter == GenreListSource[0])
                {
                    BookListSource = new List<Book>();
                    foreach (Book book in BookListSourceCopy)
                    {
                        if (book.Author == selectedAuthorToFilter.Name)
                        {
                            BookListSource.Add(book);
                        }
                    }
                }
                else if (selectedAuthorToFilter != AuthorListSource[0] && selectedGenreToFilter != GenreListSource[0])
                {
                    FilterAuthorAndGenre();
                }
                else if (selectedAuthorToFilter == AuthorListSource[0] && selectedGenreToFilter != GenreListSource[0])
                {
                    BookListSource = new List<Book>();
                    foreach (Book book in BookListSourceCopy)
                    {
                        if (book.Genre == selectedGenreToFilter.Name)
                        {
                            BookListSource.Add(book);
                        }
                    }
                }
                else
                {
                    _ = GetBooks();
                }
            }
        }
        public Genre SelectedGenreToFilter
        {
            get { return selectedGenreToFilter; }
            set
            {
                selectedGenreToFilter = value;
                OnPropertyChanged("SelectedGenreToFilter");
                if (selectedGenreToFilter != GenreListSource[0] && selectedAuthorToFilter == AuthorListSource[0])
                {
                    BookListSource = new List<Book>();
                    foreach (Book book in BookListSourceCopy)
                    {
                        if (book.Genre == selectedGenreToFilter.Name)
                        {
                            BookListSource.Add(book);
                        }
                    }
                }
                else if (selectedGenreToFilter != GenreListSource[0] && selectedAuthorToFilter != AuthorListSource[0])
                {
                    FilterAuthorAndGenre();
                }
                else if (selectedGenreToFilter == GenreListSource[0] && selectedAuthorToFilter != AuthorListSource[0])
                {
                    BookListSource = new List<Book>();
                    foreach (Book book in BookListSourceCopy)
                    {
                        if (book.Author == selectedAuthorToFilter.Name)
                        {
                            BookListSource.Add(book);
                        }
                    }
                }
                else
                {
                    _ = GetBooks();
                }
            }
        }
        public bool IsAuthorFilterChecked { get { return isAuthorFilterChecked; } set { isAuthorFilterChecked = value; OnPropertyChanged("IsAuthorFilterChecked"); if (!authorCheckBoxEnabled) { SelectedAuthorToFilter = AuthorListSource[0]; } } }
        public bool IsGenreFilterChecked { get { return isGenreFilterChecked; } set { isGenreFilterChecked = value; OnPropertyChanged("IsGenreFilterChecked"); SelectedGenreToFilter = GenreListSource[0]; } }
        public RelayCommand AddBookCommand { get { return addBookCommand ?? (addBookCommand = new RelayCommand(obj => AddBookCommandMethod())); } }
        public RelayCommand DeleteBookCommand { get { return deleteBookCommand ?? (deleteBookCommand = new RelayCommand(obj => DeleteBookCommandMethod())); } }
        public RelayCommand EditBookCommand { get { return editBookCommand ?? (editBookCommand = new RelayCommand(obj => EditBookCommandMethod())); } }
        public RelayCommand SaveCommand { get { return saveCommand ?? (saveCommand = new RelayCommand(obj => SaveCommandMethod())); } }
        public RelayCommand SortByNameCommand { get { return sortByNameCommand ?? (sortByNameCommand = new RelayCommand(obj => SortByNameCommandMethod())); } }
        public RelayCommand SortByAuthorCommand { get { return sortByAuthorCommand ?? (sortByAuthorCommand = new RelayCommand(obj => SortByAuthorCommandMethod())); } }
        public RelayCommand SortByGenreCommand { get { return sortByGenreCommand ?? (sortByGenreCommand = new RelayCommand(obj => SortByGenreCommandMethod())); } }
        public RelayCommand SortByYearCommand { get { return sortByYearCommand ?? (sortByYearCommand = new RelayCommand(obj => SortByYearCommandMethod())); } }
        public RelayCommand SearchByNameCommand { get { return searchByNameCommand ?? (searchByNameCommand = new RelayCommand(obj => SearchByNameCommandMethod())); } }
        public bool isNameSortClicked = false;
        public bool isAuthorSortClicked = false;
        public bool isGenreSortClicked = false;
        public bool isYearSortClicked = false;
        private List<Book> booksToSort = new List<Book>();
        public async Task GetBooks()
        {
            BookListSource = await _bookDB.GetBooksAsync();
            BookListSourceCopy = await _bookDB.GetBooksAsync();
        }
        public async Task GetAuthors()
        {
            AuthorListSource = await _authorDB.GetAuthorsAsync();
            SelectedAuthorToFilter = AuthorListSource[0];
        } 
        public async Task GetGenres()
        {
            GenreListSource = await _genreDB.GetGenresAsync();
            SelectedGenreToFilter = GenreListSource[0];
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
                if (author.Name == bookAuthor.Name)
                {
                    SelectedBookAuthor = author;
                }
            }
            foreach (Genre genre in GenreListSource)
            {
                if (genre.Name == bookGenre.Name)
                {
                    SelectedBookGenre = genre;
                }
            }
            NameText = oldBook.Name;
            YearText = oldBook.Release.ToString();
        }
        public async void DeleteBookCommandMethod()
        {
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
            ClearBookText();
            DynamicVisGridEnabled = true;
            isAddBookButtonClicked = true;
            isEditBookButtonClicked = false;
        }
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
        private void SearchByNameCommandMethod()
        {
            if (SearchByNameText != "")
            {
                BookListSource = new List<Book>();
                foreach (Book book in BookListSourceCopy)
                {
                    if (book.Name.StartsWith(SearchByNameText))
                    {
                        BookListSource.Add(book);
                    }
                }
            }
            else
            {
                _ = GetBooks();
            }
        }
        private async void FilterAuthorAndGenre()
        {
            await GetBooks();
            if (BookListSourceMixFilter.Count == 0 && BookListSource.Count != 0)
            {
                BookListSourceMixFilter = BookListSource;
            }
            BookListSource = new List<Book>();
            foreach (Book book in BookListSourceMixFilter)
            {
                if (book.Author == selectedAuthorToFilter.Name && book.Genre == SelectedGenreToFilter.Name)
                {
                    BookListSource.Add(book);
                }
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
        public void ClearBookGenreComboBox()
        {
            SelectedBookGenre = GenreListSource[0];
            SelectedGenreToFilter = GenreListSource[0];
        }
        public void ClearBookAuthorComboBox()
        {
            SelectedBookAuthor = AuthorListSource[0];
            SelectedAuthorToFilter = AuthorListSource[0];
        }
    }
}
