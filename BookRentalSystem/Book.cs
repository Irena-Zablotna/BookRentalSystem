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
        public  Author Author{ get; set;}
        public string Title { get; }
        public string Category { get; }
        public bool IsAvailable { get; set; }
        public List<int>? Ratings { get; } 
        public List <User>? Users { get; }
        public Book(int id, Author author, string title, string category)
        {
            Author = author;
            Title = title;
            Category = category;
            IsAvailable = true;
            Ratings = new List<int>();
            Users = new List<User>();
        }
        public override string ToString()
        {
            return $"{Title} by {Author.Name} {Author.Surname} ({Category}) {(IsAvailable ? "- Available" : "- Rented")}";
        }
        public Book()
        {
        }
        public Book(string title)
        {
            Title = title;  
        }
    }
}
