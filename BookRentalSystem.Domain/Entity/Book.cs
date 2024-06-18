using BookRentalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.Domain.Entity

{
 public class Book:BaseEntity
    {
        public  Author Author{ get; set;}
        public string Title { get; }
        public string Category { get; }
        public bool IsAvailable { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<int> Ratings { get; } 
        public List <User> Users { get; }
      
        public Book(Author author, string title, string category)
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
            return $"{Id}. {Title} by {Author.Name} {Author.Surname} ({Category}) {(IsAvailable ? "- Available" : "- Rented until ")}{ReturnDate}";
        }
        /*public Book()
        {
        }*/
    }
}
