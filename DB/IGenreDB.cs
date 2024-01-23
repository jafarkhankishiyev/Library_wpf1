using Library_wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public interface IGenreDB
    {
        Task<List<Genre>> GetGenresAsync();
        Task<int> AddGenre(Genre genre);
        Task<int> DeleteGenre(Genre genre);
        Task<int> EditGenre(Genre oldGenre, Genre newGenre);
    }
}
