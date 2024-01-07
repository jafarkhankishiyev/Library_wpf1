using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library_wpf
{
    public class Book 
    {
        //fields
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Release { get; set; }

        //constructors   
        public Book()
        {
            Name = "Unknown";
            Author = "Unkwnown";
            Genre = "Unknown";
            Release = "Unknown";
        }
        public Book(string name, string author, string genre, string release) 
        {
            Name = name;
            Author = author;
            Genre = genre;
            Release = release;
        }

        //methods
        public override string ToString()
        {
            return $"{this.Name} \t {this.Author} \t {this.Genre} \t {this.Release}";
        }
        public static async Task<List<Book>> GetBooksAsync() 
        {
            List<Book> books = new List<Book>();
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command = dataSource.CreateCommand(DB.Read());
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
        public static async Task<int> AddBook(string bookName, string bookAuthor, string bookGenre, string bookYear) 
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
        public static async Task<int> AddBook(Book book)
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
        public static async Task<int> EditBook(string dataChoicePrepared, string dataUpdate, string bookNameString) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command5 = dataSource.CreateCommand(DB.Edit(dataChoicePrepared));
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public static async Task<int> EditBook(Book oldBook, Book newBook)
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            List<string> dataChoiceList = new List<string>();
            List<string> dataUpdateList = new List<string>();
            if (oldBook.Name != newBook.Name)
            {
                dataChoiceList.Add("name");
                dataUpdateList.Add(newBook.Name);
            } else if(oldBook.Author != newBook.Author)
            {
                dataChoiceList.Add("author");
                dataUpdateList.Add(newBook.Author);
            } else if(oldBook.Genre != newBook.Genre)
            {
                dataChoiceList.Add("genre");
                dataUpdateList.Add(newBook.Genre);
            } else if(oldBook.Release != newBook.Release)
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
                    command5.Parameters.AddWithValue($"");
                    i++;
                }
            }
            command5.Parameters.AddWithValue("@BookName", oldBook.Name);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public static async Task<int> DeleteBook(List<Book> books, int bookDeleteNumInt) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }       
        public static async Task<int> DeleteBook(Book book) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", book.Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}