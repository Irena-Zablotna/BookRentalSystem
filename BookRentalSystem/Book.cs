using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
 public class Book
    {
        public string Author { get; }
        public string Title { get; }
        public string Category { get; }
        public bool IsAvailable { get; set; }
        public int? Opinion { get; set; }
        public List<int> Opinions { get; } 
        public List <User> users { get; }
        public Book(string author, string title, string category)
        {
            Author = author;
            Title = title;
            Category = category;
            IsAvailable = true;
        }
    }
}
