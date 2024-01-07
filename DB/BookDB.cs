using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public class BookDB
    {
        private readonly string _connectionstring;
        private readonly string _readquery;
        private readonly string _addquery;
        private readonly string _deletequery;


        public BookDB()
        {
            _connectionstring = "Server=localhost;User Id = postgres; Password = 123; Database=library";
            _readquery = "SELECT * FROM books;";
            _addquery = "INSERT INTO books (name, author, genre, released) VALUES (@BookName, @BookAuthor, @BookGenre, @BookYear);";
            _deletequery = "DELETE FROM books WHERE name=@BookToDelete;";
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
                    book.Release = reader.GetString(3);
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
                dataUpdateList.Add(newBook.Release);
            }
            string editQuery = "";
            string dataChoice = "";
            if (dataChoiceList.Count > 1)
            {
                editQuery = "UPDATE books SET ";
                for(int i = 0; i < dataChoiceList.Count; i++)
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
            if(dataChoiceList.Count <= 1)
            {
                command5.Parameters.AddWithValue("@DataUpdate", dataUpdateList[0]);
            } else
            {
                for (int i = 0; i < dataChoiceList.Count;i++)
                {
                    command5.Parameters.AddWithValue($"@DataUpdate{i}", dataUpdateList[i]);
                }
            }
            command5.Parameters.AddWithValue("@BookName", oldBook.Name);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
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
