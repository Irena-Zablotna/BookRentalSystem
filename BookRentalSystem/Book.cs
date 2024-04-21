using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
 public class Book
    {
        public int Id { get; set; }
        public string Author { get; }
        public string Title { get; }
        public string Category { get; }
        public bool IsAvailable { get; set; }
        public int? Opinion { get; set; }
        public List<int> Opinions { get; } 
        public List <User> Users { get; }
        public Book(int id, string author, string title, string category)
        {
            Author = author;
            Title = title;
            Category = category;
            IsAvailable = true;
            Opinions = new List<int>();
            Users = new List<User>();
        }
    }
}
