using Library_wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    internal interface IAuthorDB
    {
        Task<List<Author>> GetAuthorsAsync();
        Task<int> AddAuthor(Author author);
        Task<int> DeleteAuthor(Author author);
        Task<int> EditAuthor(Author oldAuthor, Author newAuthor);
    }
}
