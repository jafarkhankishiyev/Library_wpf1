using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library_wpf 
{
    public static class DB 
    {
        public static string GetConnectionString() {
            DBConfiguration dbConfData = new DBConfiguration();
            string connectionString = $"Server={dbConfData.Server};User Id = {dbConfData.UserId}; Password = {dbConfData.Password}; Database={dbConfData.Database}";
            return connectionString;
        }
        public static string Read() {
            string readQuery = "SELECT * FROM books";
            return readQuery;
            /*
            await using var command = dataSource.CreateCommand("");
            await using var reader = await command.ExecuteReaderAsync();
            return reader; 
            */
        }
        public static string Create() {
            string createQuery = "INSERT INTO books (name, author, genre, released) VALUES (@BookName, @BookAuthor, @BookGenre, @BookYear)";
            return createQuery;
        }
        public static string Edit(string dataChoicePrepared) 
        {
            string editQuery = $"UPDATE books SET {dataChoicePrepared}=@DataUpdate WHERE name=@BookName";
            return editQuery;
        }
        public static string Edit(List<string> dataChoicePreparedList, List<string> dataUpdateList) 
        {
            string editQuery = "";
            string dataChoice = "";
            if (dataChoicePreparedList.Count > 1)
            {
                editQuery = "UPDATE books SET ";
                int i = 1;
                foreach (string choice in dataChoicePreparedList)
                {
                    foreach(string update in dataUpdateList)
                    editQuery += $"@DataChoice{i}=@DataUpdate{i}";
                    i++;
                    if (dataChoicePreparedList.Count - dataChoicePreparedList.IndexOf(choice) > 1)
                    {
                        editQuery += ", ";
                    }
                }
                editQuery += " WHERE name=@BookName";
            } else
            {
                dataChoice = dataChoicePreparedList[0];
                editQuery = $"UPDATE books SET {dataChoice}=@DataUpdate WHERE name=@BookName";
            }
            return editQuery;
        }
        public static string Delete() 
        {
            string deleteQuery = "DELETE FROM books WHERE name=@BookToDelete;";
            return deleteQuery;
        }
    }

}