namespace Library_wpf
{
    public class Book 
    {

        //fields
        private string name;
        private string author;
        private string genre;
        private int release;

        //properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        } 
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Genre 
        { 
            get { return genre; }
            set { genre = value; }
        }
        public int Release
        { 
            get { return release; }
            set { release = value; }
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
    }
}