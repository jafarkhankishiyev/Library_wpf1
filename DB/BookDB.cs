using Library_wpf.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_wpf.DB
{
    public class BookDB : IBookDB
    {
        //fields
        private readonly string _readquery = "SELECT books.name, authors.name, genres.name, books.released, books.Id FROM books LEFT JOIN authors ON books.author = authors.id LEFT JOIN genres ON books.genre = genres.id";
        private readonly string _addquerybooks = "INSERT INTO books(name, released) VALUES(@BookName, @BookYear); ";
        private readonly string _addqueryauthorsbooks = "INSERT INTO authors_books VALUES(@BookAuthor, (SELECT id FROM books WHERE name = @BookName LIMIT 1)); ";
        private readonly string _addquerybooksgenres = "INSERT INTO books_genres VALUES((SELECT id FROM books WHERE name=@BookName AND author=@BookAuthor), @BookGenre); ";
        private readonly string _addquery = "INSERT INTO books(name, released) VALUES(@BookName, @BookYear); INSERT INTO authors_books VALUES(@BookAuthor, (SELECT id FROM books WHERE name = @BookName AND author=@BookAuthor)); INSERT INTO books_genres VALUES((SELECT id FROM books WHERE name=@BookName AND author=@BookAuthor), @BookGenre)";
        private readonly string _deletequery = "DELETE FROM authors_books WHERE book_id = @BookToDelete; DELETE FROM books_genres WHERE book_id = @BookToDelete; DELETE FROM books WHERE id=@BookToDelete";
        private readonly string _connectionstring;

        //constructor
        public BookDB(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        //methods
        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = new List<Book>();
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand(_readquery);
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetInt32(3);
                    book.Id = reader.GetInt32(4);
                    books.Add(book);
                }
            }
            return books;
        }
        public async Task<int> AddBook(Book book, ObservableCollection<Author> ?authors = null, ObservableCollection<Genre> ?genres = null)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            if(authors == null && genres == null)
            {
                await using var command2 = dataSource.CreateCommand(_addquery);
                command2.Parameters.AddWithValue("@BookName", book.Name);
                command2.Parameters.AddWithValue("@BookAuthor", Int32.Parse(book.Author));
                command2.Parameters.AddWithValue("@BookGenre", Int32.Parse(book.Genre));
                command2.Parameters.AddWithValue("@BookYear", book.Release);
                int number = await command2.ExecuteNonQueryAsync();
                if (number == 3)
                {
                    number -= 2;
                }
                else
                {
                    number = 0;
                }
                return number;
            }
            else if (authors != null && genres == null)
            {
                string booksauthorsquery = _addquerybooks;
                List<string> authorstrings = new List<string>;
                foreach (Author author in authors)
                {

                }
            }
            else if(authors == null && genres != null)
            {

            }
            else
            {

            }
        }
        public async Task<int> EditBook(Book oldBook, Book newBook, ObservableCollection<Author>? authors = null, ObservableCollection<Genre>? genres = null)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            if (authors == null && genres == null)
            {
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
                    foreach (string choice in dataChoiceList)
                    {
                        if (choice == "author")
                        {
                            editQuery += $"UPDATE authors_books SET author_id={newBook.Author} WHERE book_id={oldBook.Id}; ";
                        }
                        else if (choice == "genre")
                        {
                            editQuery += $"UPDATE books_genres SET genre_id={newBook.Genre} WHERE book_id={oldBook.Id}; ";
                        }
                    }
                    if (dataChoiceList.Count > 1)
                    {
                        editQuery += "UPDATE books SET ";
                        for (int i = 0; i < dataChoiceList.Count; i++)
                        {
                            if (dataChoiceList[i] == "author")
                            {
                                editQuery += $"author={newBook.Author}";
                            }
                            else if (dataChoiceList[i] == "genre")
                            {
                                editQuery += $"genre={newBook.Genre}";
                            }
                            else
                            {
                                editQuery += $"{dataChoiceList[i]}=@DataUpdate{i}";
                            }
                            if (dataChoiceList.Count - i > 1)
                            {
                                editQuery += ", ";
                            }
                        }
                        editQuery += $" WHERE id={oldBook.Id}; ";
                    }
                    else
                    {
                        dataChoice = dataChoiceList[0];
                        editQuery = $"UPDATE books SET {dataChoice}=@DataUpdate WHERE id={oldBook.Id}; ";
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
                    MessageBox.Show(command5.CommandText);
                    int number = await command5.ExecuteNonQueryAsync();
                    return number;
                }
                else
                {
                    return 0;
                }
            }
            else if (authors != null && genres == null)
            {
                int number = 0;
                return number;
            }
            else if(authors != null && genres != null)
            {
                int number = 0;
                return number;
            }
            else if(authors == null && genres != null)
            {
                int number = 0;
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
            command7.Parameters.AddWithValue("@BookToDelete", book.Id);
            int number = await command7.ExecuteNonQueryAsync();
            if(number == 3)
            {
                number -= 2;
            }
            else
            {
                number = 0;
            }
            return number;
        }
        public async Task<List<Book>> FilterBooks(Author author)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand($"{_readquery} WHERE author={author.Id};");
            await using var reader = await command.ExecuteReaderAsync();
            List<Book> books = new List<Book>();
            if(reader.HasRows) 
            { 
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetInt32(3);
                    book.Id = reader.GetInt32(4);
                    books.Add(book);
                }
            }
            return books;
        }
        public async Task<List<Book>> FilterBooks(Author author, Genre genre)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand($"{_readquery} WHERE author={author.Id} AND genre={genre.Id};");
            MessageBox.Show(command.CommandText);
            await using var reader = await command.ExecuteReaderAsync();
            List<Book> books = new List<Book>();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetInt32(3);
                    book.Id = reader.GetInt32(4);
                    books.Add(book);
                }
            }
            return books;
        }
        public async Task<List<Book>> FilterBooks(Genre genre)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            await using var command = dataSource.CreateCommand($"{_readquery} WHERE genre={genre.Id};");
            await using var reader = await command.ExecuteReaderAsync();
            List<Book> books = new List<Book>();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetInt32(3);
                    book.Id = reader.GetInt32(4);
                    books.Add(book);
                }
            }
            return books;
        }
    }
}
