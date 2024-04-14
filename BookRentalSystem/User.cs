﻿using System;
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
       

        public User(string name, int id)
        {
            Name = name;
            Id = id;
            IsAdmin = false;
        }
        public List<Book> Books { get; set; }
    }
}
