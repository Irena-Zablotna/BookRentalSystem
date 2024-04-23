using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class UserService
    {
        public List<User> users = new List<User>();

        public void InitalizeUsers()
        {
            User user1 = new User(1, "Admin");
            users.Add(user1);
            user1.IsAdmin = true;

            users.Add(new(2, "Irena"));
            users.Add(new(3, "Robert"));
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
            foreach (var user in users)
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
       

        public void RegisterUser()
        {
            bool IsRegistered = false;
            while (!IsRegistered)
            {
                Console.WriteLine("Enter your username ");
                string username = Console.ReadLine();
                bool usernameExists = false;
                foreach (var user in users)
                {
                    if (username == user.Name)
                    {
                        usernameExists = true;
                        break;
                    }
                }
                if (!usernameExists)
                {
                    User newUser = new User(username);
                    newUser.IsAdmin = false;
                    newUser.Id = users.Count + 1;
                    users.Add(newUser);
                    Console.WriteLine($"You have been successfully registered. Your id is {newUser.Id}");
                    IsRegistered = true;
                }
                else
                {
                    Console.WriteLine("This username already exists, try another one");
                    IsRegistered = false;
                }
            }
        }
    }
}

