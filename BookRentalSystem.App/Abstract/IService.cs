using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Abstract
{
    public interface IService <T>   
    {
        List <T> Items { get; set; }

        List <T> GetAll ();

         T getItemById (int id); 

        int AddItem(T item);

        int UpgradeItem(T item);

        void RemoveItem (T item);


    }
}
