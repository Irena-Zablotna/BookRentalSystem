using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class MenuAction
    {
       
        public int Id { get; set; }
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
