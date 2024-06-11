using BookRentalSystem.App.Abstract;
using BookRentalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private object item;

        public List<T> Items { get ; set; }
        public BaseService() { 
            Items = new List<T>();
        }

        public List<T> GetAll()
        {
            return Items;
        }

        public T getItemById(int id)
        {
            var entity = Items.FirstOrDefault(p => p.Id == id);
            return entity;
        }

        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(p=>p.Id == item.Id);
            if(entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }
    }

    }

        
    
