using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.Models
{
    public class Genre : INotifyPropertyChanged
    {
        private string name;
        private int id;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get { return name; } 
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            } 
        }
        public int Id { get; set; }
        public Genre() 
        {
            name = "Unkown";
        }
        public Genre(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return $"{this.Name}";
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
