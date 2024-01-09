using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Library_wpf.DB
{
    public class BookDB : DB
    {
        private readonly string _table = "books";
        private readonly string _columns = "name, author, genre, released";
        private readonly string _insertParameters = "@BookName, @BookAuthor, @BookGenre, @BookYear";
        private readonly string _deleteColAndParam = "name=@BookToDelete";
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;

        public BookDB()
        {
            (_readquery, _addquery, _deletequery) = TailorDB(_table, _columns, _insertParameters, _deleteColAndParam);
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = new List<Book>();
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(_readquery);
            await using var reader = await command.ExecuteReaderAsync();
            int i = 0;
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetInt32(3);
                    books.Add(book);
                    i++;
                }
            }
            return books;
        }
        public async Task<int> AddBook(Book book)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command2 = dataSource.CreateCommand(_addquery);
            command2.Parameters.AddWithValue("@BookName", book.Name);
            command2.Parameters.AddWithValue("@BookAuthor", book.Author);
            command2.Parameters.AddWithValue("@BookGenre", book.Genre);
            command2.Parameters.AddWithValue("@BookYear", book.Release);
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
                    command5.Parameters.AddWithValue("@DataUpdate", dataUpdateList[0]);
                }
                else
                {
                    for (int i = 0; i < dataChoiceList.Count; i++)
                    {
                            command5.Parameters.AddWithValue($"@DataUpdate{i}", dataUpdateList[i]);
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
        public async Task<int> DeleteBook(List<Book> books, int bookDeleteNumInt)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command7 = dataSource.CreateCommand(_deletequery);
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
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
