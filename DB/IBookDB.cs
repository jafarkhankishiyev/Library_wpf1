using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public interface IBookDB
    {
        Task<List<Book>> GetBooksAsync();
        Task<int> AddBook(Book book);
        Task<int> DeleteBook(Book book);
        Task<int> EditBook(Book oldBook, Book newBook);
    }
}
