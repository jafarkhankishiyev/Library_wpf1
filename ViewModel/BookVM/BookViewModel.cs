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
        private RelayCommand deleteBookCommand;
        private RelayCommand sortByNameCommand;
        private RelayCommand switchToAddBookViewCommand;
        private RelayCommand switchToEditBookViewCommand;
        private MainViewModel _mainViewModel;
        private bool saveButtonEnabled;
        private bool editButtonEnabled;
        private bool deleteButtonEnabled;
        private bool addButtonEnabled;
        private bool isGenreFilterChecked;
        private bool isAuthorFilterChecked;
        private bool dynamicVisGridEnabled;
        private Book selectedBook;
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
        private string searchByNameText;
        private List<Genre> genreListSource;
        private ObservableCollection<Author> authorListSource;
        public RelayCommand SwitchToAddBookViewCommand { get { return switchToAddBookViewCommand ?? (switchToAddBookViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddEditBookView(new AddEditBookViewModel(_bookDB, _authorDB, _genreDB, _mainViewModel)))); } }
        public RelayCommand SwitchToEditBookViewCommand { get { return switchToEditBookViewCommand ?? (switchToEditBookViewCommand = new RelayCommand(obj => _mainViewModel.CurrentView = new AddEditBookView(new AddEditBookViewModel(_bookDB, _authorDB, _genreDB, _mainViewModel, SelectedBook)))); } }
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
                    _ = FilterByAuthor(SelectedAuthorToFilter);
                }
                else if (selectedAuthorToFilter != AuthorListSource[0] && selectedGenreToFilter != GenreListSource[0])
                {
                    _ = FilterByAuthorGenre(SelectedAuthorToFilter, SelectedGenreToFilter);
                }
                else if (selectedAuthorToFilter == AuthorListSource[0] && selectedGenreToFilter != GenreListSource[0])
                {
                    _ = FilterByGenre(SelectedGenreToFilter);
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
                    _ = FilterByGenre(SelectedGenreToFilter);
                }
                else if (selectedGenreToFilter != GenreListSource[0] && selectedAuthorToFilter != AuthorListSource[0])
                {
                    _ = FilterByAuthorGenre(SelectedAuthorToFilter, SelectedGenreToFilter);
                }
                else if (selectedGenreToFilter == GenreListSource[0] && selectedAuthorToFilter != AuthorListSource[0])
                {
                    _ = FilterByAuthor(SelectedAuthorToFilter);
                }
                else
                {
                    _ = GetBooks();
                }
            }
        }
        public bool IsAuthorFilterChecked { get { return isAuthorFilterChecked; } set { isAuthorFilterChecked = value; OnPropertyChanged("IsAuthorFilterChecked");  SelectedAuthorToFilter = AuthorListSource[0]; } } 
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
        public async Task FilterByAuthor(Author author)
        {
            BookListSource = new List<Book>();
            BookListSource = await _bookDB.FilterBooks(author);
        }
        public async Task FilterByAuthorGenre(Author author, Genre genre)
        {
            BookListSource = new List<Book>();
            BookListSource = await _bookDB.FilterBooks(author, genre);
        }
        public async Task FilterByGenre(Genre genre)
        {
            BookListSource = new List<Book>();
            BookListSource = await _bookDB.FilterBooks(genre);
        }
    }
}
