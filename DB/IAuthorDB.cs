using Library_wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public interface IAuthorDB
    {
        Task<ObservableCollection<Author>> GetAuthorsAsync();
        Task<int> AddAuthor(Author author);
        Task<int> DeleteAuthor(Author author);
        Task<int> EditAuthor(Author oldAuthor, Author newAuthor);
    }
}
