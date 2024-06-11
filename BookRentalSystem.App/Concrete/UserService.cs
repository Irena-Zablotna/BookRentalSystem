using BookRentalSystem.App.Common;
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
        public void InitalizeUsers()
        {
            User user1 = new User(1, "Admin");
            Items.Add(user1);
            user1.IsAdmin = true;

            Items.Add(new(2, "Irena"));
            Items.Add(new(3, "Robert"));
        }

        public string RetrieveUsername()
        {
            Console.WriteLine("Enter you username");
            string username = Console.ReadLine();
            Console.WriteLine($"Hello {username}!");
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


        public bool RegisterUser(string name)
        {
            while (true)
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
                    Console.WriteLine($"Do you want to be registered with username {name}? (y/n): ");
                    if (Console.ReadLine().ToLower() != "y")
                    {
                        Console.WriteLine("Please write the username you would like to register with.");
                        name= Console.ReadLine();
                    }
                    User newUser = new User(name);
                    newUser.IsAdmin = false;
                    newUser.Id = Items.Count + 1;
                    Items.Add(newUser);
                    Console.WriteLine($"You have been successfully registered. Your id is {newUser.Id}, your username is {name}");
                    return true; 
                }
                else
                {
                    Console.WriteLine("This username already exists, try another one");
                }
            }
        }
    }
}

