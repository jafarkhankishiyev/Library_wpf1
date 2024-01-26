using Library_wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public interface IBookDB
    {
        Task<List<Book>> GetBooksAsync();
        Task<int> AddBook(Book book, ObservableCollection<Author> ?authors = null, ObservableCollection<Genre> ?genres = null);
        Task<int> DeleteBook(Book book);
        Task<int> EditBook(Book oldBook, Book newBook, ObservableCollection<Author> ?authors = null, ObservableCollection<Genre> ?genres = null);
        Task<List<Book>> FilterBooks(Author author);
        Task<List<Book>> FilterBooks(Genre genre);
        Task<List<Book>> FilterBooks(Author author, Genre genre);
    }
}
