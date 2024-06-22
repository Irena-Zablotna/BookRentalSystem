﻿using BookRentalSystem.App.Common;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Concrete
{
    public class UserService:BaseService<User>
    {
        public UserService()
        {
            InitalizeUsers();
        }
        public void InitalizeUsers()
        {
            User user1 = new User(1, "Admin");
            Items.Add(user1);
            user1.IsAdmin = true;

            Items.Add(new(2, "Irena"));
            Items.Add(new(3, "Robert"));
            Items.Add(new(4, "Kajetan"));
        }
        public void GetAll()
        {
            var usersToShow = Items.ToList();
            Console.WriteLine("The list of all users:");
            foreach (var user in usersToShow)
            {
                Console.WriteLine($"{user.Id}, {user.Name}");
            }
        }
        public string RetrieveUsername(string username)
        {
            if (username == null || username.Length == 0)
            {
                return null;
            }
            return username;
        }
        public int VerifyUser( string username)
        {
            foreach (var user in Items)
            {
                if (username == user.Name && user.IsAdmin == true)
                {
                    return 1;
                }
                else if(username == user.Name && user.IsAdmin == false)
                {
                    return 2;
                }
            }
            return 0;
        }


        public User RegisterUser(string name)
        {
                bool usernameExists = false;
                foreach (var user in Items)
                {
                    if (name == user.Name)
                    {
                        usernameExists = true;
                        break;
                    }
                }
                if (!usernameExists)
                {
                    User newUser = new User(name);
                    newUser.IsAdmin = false;
                    newUser.Id = Items.Count + 1;
                    Items.Add(newUser);
                    return newUser; 
                }
                else
                {
                    return null;
                }
        }
         public User GetUserByUsername(string username)
        {
            var user = Items.FirstOrDefault(p=> p.Name == username);
            return user;
        }
    }
}

