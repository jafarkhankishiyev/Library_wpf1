using Library_wpf.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public class GenreDB : DB
    {
        private readonly string _table = "genres";
        private readonly string _columns = "name";
        private readonly string _insertParameters = "@GenreName";
        private readonly string _deleteColAndParam = "name=@GenreToDelete";
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;

        public GenreDB()
        {
            (_readquery, _addquery, _deletequery) = TailorDB(_table, _columns, _insertParameters, _deleteColAndParam);
        }
        public async Task<List<Genre>> GetGenresAsync()
        {
            List<Genre> genres = new List<Genre>();
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(_readquery);
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Genre genre = new Genre();
                    genre.Name = reader.GetString(0);
                    genres.Add(genre);
                }
            }
            return genres;
        }

    }
}
