using BookRentalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.Domain.Entity
{
    public class MenuAction:BaseEntity
    {
        public string Name { get; set; }
        public string ?MenuCategory { get; set; }

        public MenuAction(int id, string name, string menuCategory)
        {
            Id = id;
            Name = name;
            MenuCategory = menuCategory;
        }
    }
   

}
