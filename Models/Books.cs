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

    }
}