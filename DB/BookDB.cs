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


        public BookDB(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = new List<Book>();
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(DB_.Read());
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
        public async Task<int> AddBook(string bookName, string bookAuthor, string bookGenre, string bookYear)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command2 = dataSource.CreateCommand(DB.Create());
            command2.Parameters.AddWithValue("@BookName", bookName);
            command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            command2.Parameters.AddWithValue("@BookGenre", bookGenre);
            command2.Parameters.AddWithValue("@BookYear", bookYear);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> AddBook(Book book)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command2 = dataSource.CreateCommand(DB.Create());
            command2.Parameters.AddWithValue("@BookName", book.Name);
            command2.Parameters.AddWithValue("@BookAuthor", book.Author);
            command2.Parameters.AddWithValue("@BookGenre", book.Genre);
            command2.Parameters.AddWithValue("@BookYear", book.Release);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> EditBook(string dataChoicePrepared, string dataUpdate, string bookNameString)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command5 = dataSource.CreateCommand(DB.Edit(dataChoicePrepared));
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> EditBook(Book oldBook, Book newBook)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            List<string> dataChoiceList = new List<string>();
            List<string> dataUpdateList = new List<string>();
            if (oldBook.Name != newBook.Name)
            {
                dataChoiceList.Add("name");
                dataUpdateList.Add(newBook.Name);
            }
            else if (oldBook.Author != newBook.Author)
            {
                dataChoiceList.Add("author");
                dataUpdateList.Add(newBook.Author);
            }
            else if (oldBook.Genre != newBook.Genre)
            {
                dataChoiceList.Add("genre");
                dataUpdateList.Add(newBook.Genre);
            }
            else if (oldBook.Release != newBook.Release)
            {
                dataChoiceList.Add("released");
                dataUpdateList.Add(newBook.Release);
            }

            await using var command5 = dataSource.CreateCommand(DB.Edit(dataChoiceList, dataUpdateList));
            foreach (string dataUpdate in dataUpdateList)
            {
                foreach (string dataChoice in dataChoiceList)
                {
                    int i = 1;
                    command5.Parameters.AddWithValue($"@DataUpdate{i}", dataUpdate);
                    command5.Parameters.AddWithValue($"@DataChoice{i}", dataChoice);
                    i++;
                }
            }
            command5.Parameters.AddWithValue("@BookName", oldBook.Name);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> DeleteBook(List<Book> books, int bookDeleteNumInt)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
        public async Task<int> DeleteBook(Book book)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", book.Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}
