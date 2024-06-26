﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
   public class Author
    {
        public String Name { get; set; }
        public String Surname { get; set; }

        public List<String> authorBooks { get; set; }

        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
            authorBooks = new List<String>();
        }
        
        public void AddBook(string title)
        {
            authorBooks.Add(title);
        }
    }
}
