using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class User
    {
        public string Name { get; }
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public List<Book> Books { get; set; }

        public User(int id, string name)
        {
            Name = name;
            Id = id;
            IsAdmin = false;
            Books = new List<Book>();
        }
        public User(string name)
        {
            
            Name = name;
            Books = new List<Book>();
            IsAdmin = false;
        }
        
       
    }
}
