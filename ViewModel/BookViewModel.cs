using Library_wpf.DB;
using Library_wpf.Models;
using Library_wpf.ViewModelNameSpace;
using Library_wpf.Views;
using Library_wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Library_wpf.ViewModel
{
    public class BookViewModel : BaseViewModel
    {
        private RelayCommand addBookCommand;
        private RelayCommand deleteBookCommand;
        private RelayCommand editBookCommand;
        private RelayCommand saveCommand;
        private RelayCommand sortByNameCommand;
        private RelayCommand switchToAddBookViewCommand;
        private RelayCommand switchToEditBookViewCommand;
        private MainViewModel _mainViewModel;
        private bool saveButtonEnabled;
        private bool editButtonEnabled;
        private bool deleteButtonEnabled;
        private bool addButtonEnabled;
        private bool isAddBookButtonClicked;
        private bool isDeleteBookButtonClicked;
        private bool isEditBookButtonClicked;

        private bool isButton1Clicked;
        private bool authorFilterEnabled;
        private bool genreCheckBoxEnabled;
        private bool genreFilterEnabled;
        private bool isGenreFilterChecked;
        private bool isAuthorFilterChecked;
        private bool dynamicVisGridEnabled;
        private Book selectedBook;
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
        public RelayCommand SwitchToAddBookViewCommand { get { return switchToAddBookViewCommand ?? (switchToAddBookViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddBookView(new AddBookViewModel(_bookDB, _authorDB, _genreDB, _mainViewModel)))); } }
        public RelayCommand SwitchToEditBookViewCommand { get { return switchToEditBookViewCommand ?? (switchToEditBookViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new EditBookView(new EditBookViewModel(_bookDB, _authorDB, _genreDB, SelectedBook, _mainViewModel)))); } }
        public BookViewModel(IBookDB bookDB, IGenreDB genreDB, IAuthorDB authorDB, MainViewModel mainViewModel)
        {
            _authorDB = authorDB;
            _genreDB = genreDB;
            _bookDB = bookDB;
            _ = GetBooks();
            _ = GetGenres();
            _ = GetAuthors();
            AddButtonEnabled = true;
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
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                if (selectedBook == null)
                {
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
        public RelayCommand DeleteBookCommand { get { return deleteBookCommand ?? (deleteBookCommand = new RelayCommand(obj => DeleteBookCommandMethod())); } }
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
        /*public async void SaveCommandMethod()
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
                CurrentView = new BookView(this);
            }
            else
            {
                isButton1Clicked = false;
            }
        }*/
        /*public void EditBookCommandMethod()
        {
            CurrentView = new AddBookView(new AddBookViewModel());
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
        }*/
        public async void DeleteBookCommandMethod()
        {
            if (SelectedBook != null)
            {
                DynamicVisGridEnabled = false;
                if(MessageBox.Show($"Are you sure you want to delete {SelectedBook.Name} from books?", "Delete Book", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int deleteResult = await _bookDB.DeleteBook(SelectedBook);
                    DeleteButtonEnabled = false;
                    EditButtonEnabled = false;
                    _ = GetBooks();
                    MessageBox.Show($"Deleted {deleteResult} object(s).");
                }
            }
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
            if (SearchByNameText != "" && SearchByNameText != null)
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
            SelectedGenreToFilter = GenreListSource[0];
        }
        public void ClearBookAuthorComboBox()
        {
            SelectedAuthorToFilter = AuthorListSource[0];
        }
    }
}
