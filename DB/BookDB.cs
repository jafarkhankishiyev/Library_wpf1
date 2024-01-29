using Library_wpf.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library_wpf.DB
{
    public class BookDB : IBookDB
    {
        //fields
        private readonly string _readquery = "SELECT books.name, authors.name, genres.name, books.released, books.id FROM books JOIN authors_books ON books.id = authors_books.book_id JOIN authors ON authors_books.author_id = authors.id JOIN books_genres ON books.id = books_genres.book_id JOIN genres ON books_genres.genre_id = genres.id ";
        private readonly string _addquerybooks = "INSERT INTO books(name, released) VALUES(@BookName, @BookYear); ";
        private readonly string _addqueryauthorsbooks = "INSERT INTO authors_books VALUES(@BookAuthor, @BookId); ";
        private readonly string _addquerybooksgenres = "INSERT INTO books_genres VALUES(@BookId, @BookGenre); ";
        private readonly string _lastbookquery = "SELECT id FROM books ORDER BY id DESC LIMIT 1; ";
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
            List<Book> updatedBooks = new List<Book>();
            foreach(Book book in books)
            {
                if(updatedBooks.Count == 0)
                {
                    updatedBooks.Add(book);
                }
                foreach(Book book2 in updatedBooks)
                {
                    if(book.Name == book2.Name)
                    {
                        if(!book2.Author.Contains(book.Author))
                        {
                            book2.Author += $", {book.Author}";
                        }
                        if(!book2.Genre.Contains(book.Genre))
                        {
                            book2.Genre += $", {book.Genre}" ;
                        }
                    }
                    else
                    {
                        updatedBooks.Add(book);
                    }
                }
            }
            books = updatedBooks;
            return updatedBooks;
        }
        public async Task<int> AddBook(Book book, ObservableCollection<Author>? authors = null, ObservableCollection<Genre>? genres = null)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            int bookId = 0;
            int number = 0;
            if (authors == null && genres == null)
            {
                await using var command2 = dataSource.CreateCommand(_addquery);
                command2.Parameters.AddWithValue("@BookName", book.Name);
                command2.Parameters.AddWithValue("@BookAuthor", Int32.Parse(book.Author));
                command2.Parameters.AddWithValue("@BookGenre", Int32.Parse(book.Genre));
                command2.Parameters.AddWithValue("@BookYear", book.Release);
                number += await command2.ExecuteNonQueryAsync();
            }
            await using var command3 = dataSource.CreateCommand(_lastbookquery);
            await using var reader = await command3.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    bookId = reader.GetInt32(0);
                }
            }
            if (authors != null || genres != null)
            {
                await using var command5 = dataSource.CreateCommand(_addquerybooks);
                command5.Parameters.AddWithValue("@BookName", book.Name);
                command5.Parameters.AddWithValue("@BookYear", book.Release);
                number += await command5.ExecuteNonQueryAsync();
            }
            if (authors != null)
            {
                foreach (Author author in authors)
                {
                    await using var command4 = dataSource.CreateCommand(_addqueryauthorsbooks);
                    command4.Parameters.AddWithValue("@BookAuthor", author.Id);
                    command4.Parameters.AddWithValue("@BookId", bookId);
                    number += await command4.ExecuteNonQueryAsync();
                }
            }
            if (genres != null)
            {
                foreach (Genre genre in genres)
                {
                    await using var command6 = dataSource.CreateCommand(_addquerybooksgenres);
                    command6.Parameters.AddWithValue("@BookGenre", genre.Id);
                    command6.Parameters.AddWithValue("@BookId", bookId);
                    number += await command6.ExecuteNonQueryAsync();
                }
            }
            if (number > 1)
            {
                number = 1;
            }
            else
            {
                number = 0;
            }
            return number;
        }
        public async Task<int> EditBook(Book oldBook, Book newBook, ObservableCollection<Author>? authors = null, ObservableCollection<Genre>? genres = null)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
            int number = 0;
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
                    dataChoiceList.Remove("author");
                    dataChoiceList.Remove("genre");
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
                    else if(dataChoiceList.Count > 0)
                    {
                        dataChoice = dataChoiceList[0];
                        editQuery = $"UPDATE books SET {dataChoice}=@DataUpdate WHERE id={oldBook.Id}; ";
                    }
                    await using var command5 = dataSource.CreateCommand(editQuery);
                    if (dataChoiceList.Count == 1)
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
                    number = await command5.ExecuteNonQueryAsync();
                }
                else
                {
                    number = 0;
                }
            }
            if (authors != null)
            {
                await using var command7 = dataSource.CreateCommand($"SELECT author_id FROM authors_books WHERE book_id={oldBook.Id}");
                await using var reader = await command7.ExecuteReaderAsync();
                ObservableCollection<int> oldAuthors = [];
                if(reader.HasRows)
                {
                    while(await reader.ReadAsync())
                    {
                        oldAuthors.Add(reader.GetInt32(0));
                    }
                }
                foreach (Author author in authors)
                {
                    if(oldAuthors.Contains(author.Id))
                    {
                        await using var command4 = dataSource.CreateCommand($"UPDATE authors_books SET author_id=@BookAuthor WHERE book_id={oldBook.Id}; ");
                        command4.Parameters.AddWithValue("@BookAuthor", author.Id);
                        command4.Parameters.AddWithValue("@BookId", oldBook.Id);
                        number += await command4.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        await using var command4 = dataSource.CreateCommand("INSERT INTO authors_books VALUES (@BookAuthor, @BookId); ");
                        command4.Parameters.AddWithValue("@BookAuthor", author.Id);
                        command4.Parameters.AddWithValue("@BookId", oldBook.Id);
                        number += await command4.ExecuteNonQueryAsync();
                    }
                }
            }
            if (genres != null)
            {
                await using var command7 = dataSource.CreateCommand($"SELECT genre_id FROM books_genres WHERE book_id={oldBook.Id}; ");
                await using var reader = await command7.ExecuteReaderAsync();
                ObservableCollection<int> oldGenres = [];
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        oldGenres.Add(reader.GetInt32(0));
                    }
                }
                foreach (Genre genre in genres)
                {
                    if(oldGenres.Contains(genre.Id))
                    {
                    await using var command6 = dataSource.CreateCommand($"UPDATE books_genres SET genre_id=@BookGenre WHERE book_id={oldBook.Id}; ");
                    command6.Parameters.AddWithValue("@BookGenre", genre.Id);
                    command6.Parameters.AddWithValue("@BookId", oldBook.Id);
                    number += await command6.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        await using var command4 = dataSource.CreateCommand("INSERT INTO books_genres(book_id, genre_id) VALUES (@BookId, @BookGenre); ");
                        command4.Parameters.AddWithValue("@BookGenre", genre.Id);
                        command4.Parameters.AddWithValue("@BookId", oldBook.Id);
                        number += await command4.ExecuteNonQueryAsync();
                    }
                }
            }
            if (authors != null || genres != null)
            {
                await using var command5 = dataSource.CreateCommand($"UPDATE books SET name=@BookName, released=@BookYear WHERE id={oldBook.Id}");
                command5.Parameters.AddWithValue("@BookName", newBook.Name);
                command5.Parameters.AddWithValue("@BookYear", newBook.Release);
                number += await command5.ExecuteNonQueryAsync();
            }
            return number;
        }
            public async Task<int> DeleteBook(Book book)
            {
                await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
                await using var command7 = dataSource.CreateCommand(_deletequery);
                command7.Parameters.AddWithValue("@BookToDelete", book.Id);
                int number = await command7.ExecuteNonQueryAsync();
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
            public async Task<List<Book>> FilterBooks(Author author)
            {
                await using var dataSource = NpgsqlDataSource.Create(_connectionstring);
                await using var command = dataSource.CreateCommand($"{_readquery} WHERE author={author.Id};");
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
