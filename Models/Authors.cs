using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.Models
{
    public class Author : INotifyPropertyChanged
    {
        //fields
        private string name;
        private string mobile;
        private string email;
        private DateTime birthday;
        private string birthdayString;

        //properties
        public string Name { get { return name; } set {  name = value; } }
        public string Mobile { get { return mobile; } set { mobile = value; } }
        public string Email { get { return email; } set { email = value; } }
        public DateTime Birthday { get {  return birthday; } set {  birthday = value; birthdayString = birthday.ToString("yyyy-MM-dd"); } }
        public string BirthdayString { get { return birthdayString; } set { birthdayString = value; } }


        //constructors
        public Author() 
        {
            name = "Unkown";
            mobile = "Unkown";
            email = "Unkonwn";
            birthday = new DateTime();
        }
        public Author(string namePar, string mobilePar, string emailPar, DateTime birthdayPar)
        {
            Name = namePar;
            Mobile = mobilePar;
            Email = emailPar;
            Birthday = birthdayPar;
        }

        //methods
        public override string ToString()
        {
            return $"{this.Name} \t {this.Mobile} \t {this.Email} \t {this.Birthday.ToString("yyyy-MM-dd")}";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
