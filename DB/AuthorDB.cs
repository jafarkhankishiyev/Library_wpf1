using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_wpf.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Library_wpf.DB
{
    public class AuthorDB : DB, IAuthorDB
    {
        //fields
        private readonly string _table = "authors";
        private readonly string _addColumns = "name, birthday, email, mobile";
        private readonly string _readColumns = "name, birthday, email, mobile, id";
        private readonly string _insertParameters = "@AuthorName, @AuthorBirthday, @AuthorEmail, @AuthorMobile";
        private readonly string _deleteColAndParam = "id=@AuthorToDelete";
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;
        private readonly string _connectionstring;

        //constructor
        public AuthorDB(string connectionstring)
        {
            (_readquery, _addquery, _deletequery) = TailorDB(_table, _addColumns, _readColumns, _insertParameters, _deleteColAndParam);
            _connectionstring = connectionstring;
        }

        //methods
        public async Task<ObservableCollection<Author>> GetAuthorsAsync()
        {
            ObservableCollection<Author> authors = new ObservableCollection<Author>();
            Author defaultAuthor = new Author();
            defaultAuthor.Name = "Not Chosen";
            authors.Add(defaultAuthor);
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(_readquery);
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Author author = new Author();
                    author.Name = reader.GetString(0);
                    author.Birthday = reader.GetDateTime(1); 
                    author.Email = reader.GetString(2);
                    author.Mobile = reader.GetString(3);
                    author.Id = reader.GetInt32(4);
                    authors.Add(author);
                }
            }
            return authors;
        }
        public async Task<int> AddAuthor(Author author)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command2 = dataSource.CreateCommand(_addquery);
            command2.Parameters.AddWithValue("@AuthorName", author.Name);
            command2.Parameters.AddWithValue("@AuthorBirthday", author.Birthday);
            command2.Parameters.AddWithValue("@AuthorEmail", author.Email);
            command2.Parameters.AddWithValue("@AuthorMobile", author.Mobile);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> EditAuthor(Author oldAuthor, Author newAuthor)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            List<string> dataChoiceList = new List<string>();
            List<string> dataUpdateList = new List<string>();
            if (oldAuthor.Name != newAuthor.Name)
            {
                dataChoiceList.Add("name");
                dataUpdateList.Add(newAuthor.Name);
            }
            if (oldAuthor.Birthday != newAuthor.Birthday)
            {
                dataChoiceList.Add("birthday");
                dataUpdateList.Add(newAuthor.Birthday.ToString());
            }
            if (oldAuthor.Email != newAuthor.Email)
            {
                dataChoiceList.Add("email");
                dataUpdateList.Add(newAuthor.Email);
            }
            if (oldAuthor.Mobile != newAuthor.Mobile)
            {
                dataChoiceList.Add("mobile");
                dataUpdateList.Add(newAuthor.Mobile);
            }
            string editQuery = "";
            string dataChoice = "";
            if (dataChoiceList.Count != 0)
            {
                if (dataChoiceList.Count > 1)
                {
                    editQuery = "UPDATE authors SET ";
                    for (int i = 0; i < dataChoiceList.Count; i++)
                    {
                        editQuery += $"{dataChoiceList[i]}=@DataUpdate{i}";
                        if (dataChoiceList.Count - i > 1)
                        {
                            editQuery += ", ";
                        }
                    }
                    editQuery += $" WHERE id={oldAuthor.Id}";
                }
                else
                {
                    dataChoice = dataChoiceList[0];
                    editQuery = $"UPDATE authors SET {dataChoice}=@DataUpdate WHERE id={oldAuthor.Id}";
                }
                await using var command5 = dataSource.CreateCommand(editQuery);
                if (dataChoiceList.Count <= 1)
                {
                    if (dataChoiceList[0] != "birthday")
                    {
                        command5.Parameters.AddWithValue("@DataUpdate", dataUpdateList[0]);
                    }
                    else
                    {
                        command5.Parameters.AddWithValue("@DataUpdate", DateTime.Parse(dataUpdateList[0]));
                    }
                }
                else
                {
                    for (int i = 0; i < dataChoiceList.Count; i++)
                    {
                        if (dataChoiceList[i] != "birthday")
                        {
                            command5.Parameters.AddWithValue($"@DataUpdate{i}", dataUpdateList[i]);
                        }
                        else
                        {
                            command5.Parameters.AddWithValue($"DataUpdate{i}", DateTime.Parse(dataUpdateList[i]));
                        }
                    }
                }
                int number = await command5.ExecuteNonQueryAsync();
                return number;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteAuthor(Author author)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command7 = dataSource.CreateCommand(_deletequery);
            command7.Parameters.AddWithValue("@AuthorToDelete", author.Id);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}
