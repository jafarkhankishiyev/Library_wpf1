using Library_wpf.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Library_wpf.UI;
using Library_wpf;
using System.Runtime.CompilerServices;

namespace Library_wpf.ViewModelNameSpace
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private BookDB _bookDB;
        private RelayCommand addBookCommand;
        private RelayCommand deleteBookCommand;
        private RelayCommand editBookCommand;
        private RelayCommand saveCommand;
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
        private bool dynamicVisGridEnabled;
        private List<Book> bookListSource;
        private bool BookList_SelectionChanged;

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
                OnPropertyChanged("SelectedBook");
            } 
        }
        public List<Book> BookListSource { get { return bookListSource; }
            set {
                bookListSource = value;
                OnPropertyChanged("BookListSource");
            }
        }
        public RelayCommand AddBookCommand 
        { 
            get { return addBookCommand ?? (addBookCommand = new RelayCommand(obj =>
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
                    }));
                }
        }
        public RelayCommand DeleteBookCommand
        { get { return deleteBookCommand ?? (deleteBookCommand = new RelayCommand(async obj => 
                    {
                        if (SelectedBook != null)
                        {
                            DynamicVisGridEnabled = false;
                            ClearText();
                            Book selectedItem = SelectedBook as Book;
                            int deleteResult = await _bookDB.DeleteBook(selectedItem);
                            DeleteButtonEnabled = false;
                            EditButtonEnabled = false;
                            _ = ShowBooks();
                            MessageBox.Show($"Deleted {deleteResult} object(s).");
                        }
                    })); 
            } 
        }
        public RelayCommand EditBookCommand
        {
            get {
                return editBookCommand ?? (editBookCommand = new RelayCommand(obj =>
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
                    }));
                }   
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(async obj =>
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
                        _ = ShowBooks();
                        SaveButtonEnabled = false;
                    }
                    else
                    {
                        isButton1Clicked = false;
                    }
                }));
            }
        }
        public LibraryViewModel()
        {
            _bookDB = new BookDB();
            _ = ShowBooks();
            AddButtonEnabled = true;
            EditButtonEnabled = false; 
            DeleteButtonEnabled = false;
        }
        public async Task ShowBooks()
        {
            List<Book> books = await _bookDB.GetBooksAsync();
            BookListSource = books;
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
