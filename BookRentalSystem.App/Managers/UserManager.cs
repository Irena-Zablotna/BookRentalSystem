using BookRentalSystem.App.Concrete;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookRentalSystem.App.Managers
{
    public class UserManager
    {
        private readonly UserService _userService;
        public UserManager(UserService userService)
        {
            _userService = userService;
        }

        public string RetrieveUsername() {
            Console.WriteLine("Enter you username");
            string username = Console.ReadLine();
            Console.WriteLine($"Hello {username}!");
            return _userService.RetrieveUsername(username); 
        } 
       
        public User RegisterUser (string name)
        {
            Console.WriteLine("Do you want to sign up to use our service? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                bool result = false;
                while (!result)
                {
                    Console.WriteLine($"Do you want to be registered with username {name}? (y/n): ");
                    if (Console.ReadLine().ToLower() != "y")
                    {
                        Console.WriteLine("Please write the username you would like to register with.");
                        name = Console.ReadLine();
                    }
                    User registered = _userService.RegisterUser(name);
                    if (registered != null)
                    {
                        Console.WriteLine($"You have been successfully registered. Your id is {registered}, your username is {name}");
                        result = true;
                        return registered;
                    }
                    else if (registered == null)
                    {
                        Console.WriteLine("This username already exists, try another one");
                    }
                }
            }
            else
            {
                Console.WriteLine("We hope you will become our user soon. Goodbye!");
            }
                return null;
        }
    }
}
