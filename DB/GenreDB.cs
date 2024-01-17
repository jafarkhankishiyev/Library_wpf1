using Library_wpf.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public class GenreDB : DB, IGenreDB
    {
        //fields
        private readonly string _table = "genres";
        private readonly string _columns = "name";
        private readonly string _insertParameters = "@GenreName";
        private readonly string _deleteColAndParam = "name=@GenreToDelete";
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;

        //constructor
        public GenreDB()
        {
            (_readquery, _addquery, _deletequery) = TailorDB(_table, _columns, _insertParameters, _deleteColAndParam);
        }

        //methods
        public async Task<List<Genre>> GetGenresAsync()
        {
            List<Genre> genres = new List<Genre>();
            Genre defaultGenre = new Genre();
            defaultGenre.Name = "Not Chosen";
            genres.Add(defaultGenre);
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
        public async Task<int> AddGenre(Genre genre)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command2 = dataSource.CreateCommand(_addquery);
            command2.Parameters.AddWithValue("@GenreName", genre.Name);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> EditGenre(Genre oldGenre, Genre newGenre)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            string dataUpdate = "";
            if (oldGenre.Name != newGenre.Name)
            {
                dataUpdate = newGenre.Name;
            }
            if(!string.IsNullOrEmpty(dataUpdate))
            { 
                string editQuery = "";
                string dataChoice = "name";
                editQuery = $"UPDATE genres SET {dataChoice}=@DataUpdate WHERE name=@GenreName";
                await using var command5 = dataSource.CreateCommand(editQuery);
                command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
                command5.Parameters.AddWithValue("@GenreName", oldGenre.Name);
                int number = await command5.ExecuteNonQueryAsync();
                return number;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteGenre(Genre genre)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command7 = dataSource.CreateCommand(_deletequery);
            command7.Parameters.AddWithValue("@GenreToDelete", genre.Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }

    }
}
