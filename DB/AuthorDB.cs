using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_wpf.Models;

namespace Library_wpf.DB
{
    public class AuthorDB : DB
    {
        private readonly string _table = "authors";
        private readonly string _columns = "name, birthday, email, mobile";
        private readonly string _insertParameters = "@AuthorName, @AuthorBirthday, @AuthorEmail, @AuthorMobile";
        private readonly string _deleteColAndParam = "name=@AuthorToDelete";
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;

        public AuthorDB()
        {
            (_readquery, _addquery, _deletequery) = TailorDB(_table, _columns, _insertParameters, _deleteColAndParam);
        }
        public async Task<List<Author>> GetBooksAsync()
        {
            List<Author> authors = new List<Author>();
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(_readquery);
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Author author = new Author();
                    author.Name = reader.GetString(1);
                    author.Birthday = reader.GetDateTime(2);
                    author.Email = reader.GetString(3);
                    author.Mobile = reader.GetString(4);
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
            command2.Parameters.AddWithValue("@AuthorEmail", author.Email5);
            command2.Parameters.AddWithValue("@AuthorMobile", book.Release);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> EditBook(Book oldBook, Book newBook)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            List<string> dataChoiceList = new List<string>();
            List<string> dataUpdateList = new List<string>();
            if (oldBook.Name != newBook.Name)
            {
                dataChoiceList.Add("name");
                dataUpdateList.Add(newBook.Name);
            }
            if (oldBook.Author != newBook.Author)
            {
                dataChoiceList.Add("author");
                dataUpdateList.Add(newBook.Author);
            }
            if (oldBook.Genre != newBook.Genre)
            {
                dataChoiceList.Add("genre");
                dataUpdateList.Add(newBook.Genre);
            }
            if (oldBook.Release != newBook.Release)
            {
                dataChoiceList.Add("released");
                dataUpdateList.Add(newBook.Release.ToString());
            }
            string editQuery = "";
            string dataChoice = "";
            if (dataChoiceList.Count != 0)
            {
                if (dataChoiceList.Count > 1)
                {
                    editQuery = "UPDATE books SET ";
                    for (int i = 0; i < dataChoiceList.Count; i++)
                    {
                        editQuery += $"{dataChoiceList[i]}=@DataUpdate{i}";
                        if (dataChoiceList.Count - i > 1)
                        {
                            editQuery += ", ";
                        }
                    }
                    editQuery += " WHERE name=@BookName";
                }
                else
                {
                    dataChoice = dataChoiceList[0];
                    editQuery = $"UPDATE books SET {dataChoice}=@DataUpdate WHERE name=@BookName";
                }
                await using var command5 = dataSource.CreateCommand(editQuery);
                if (dataChoiceList.Count <= 1)
                {
                    if (dataChoiceList[0] != "released")
                    {
                        command5.Parameters.AddWithValue("@DataUpdate", dataUpdateList[0]);
                    }
                    else
                    {
                        command5.Parameters.AddWithValue("@DataUpdate", Int32.Parse(dataUpdateList[0]));
                    }
                }
                else
                {
                    for (int i = 0; i < dataChoiceList.Count; i++)
                    {
                        if (dataChoiceList[i] != "released")
                        {
                            command5.Parameters.AddWithValue($"@DataUpdate{i}", dataUpdateList[i]);
                        }
                        else
                        {
                            command5.Parameters.AddWithValue($"DataUpdate{i}", Int32.Parse(dataUpdateList[i]));
                        }
                    }
                }
                command5.Parameters.AddWithValue("@BookName", oldBook.Name);
                int number = await command5.ExecuteNonQueryAsync();
                return number;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteBook(Book book)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command7 = dataSource.CreateCommand(_deletequery);
            command7.Parameters.AddWithValue("@BookToDelete", book.Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}
