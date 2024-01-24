using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Library_wpf
{
    public class Book : INotifyPropertyChanged
    {

        //fields
        private int id;
        private string name;
        private string author;
        private string genre;
        private int release;

        //properties
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged("Name");
            }
        } 
        public string Author
        {
            get { return author; }
            set 
            {
                author = value; 
                OnPropertyChanged("Author");
            }
        }
        public string Genre 
        { 
            get { return genre; }
            set 
            { 
                genre = value; 
                OnPropertyChanged("Genre");
            }
        }
        public int Release
        { 
            get { return release; }
            set 
            { 
                release = value; 
                OnPropertyChanged("Release");
            }
        }

        //constructors   
        public Book()
        {
            Name = "Unknown";
            Author = "Unkwnown";
            Genre = "Unknown";
            Release = 0;
        }
        public Book(string name, string author, string genre, int release) 
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
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}