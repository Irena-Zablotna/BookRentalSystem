using BookRentalSystem.App.Concrete;
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
       
        public bool RegisterUser (string name)
        {
            bool result = false;
            while (!result) {
                Console.WriteLine($"Do you want to be registered with username {name}? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    Console.WriteLine("Please write the username you would like to register with.");
                    name = Console.ReadLine();
                }
                int registered = _userService.RegisterUser(name);
                if (registered != 0)
                {
                    Console.WriteLine($"You have been successfully registered. Your id is {registered}, your username is {name}");
                    result = true;
                    return true;
                }
                else if (registered == 0)
                {
                    Console.WriteLine("This username already exists, try another one");
                }
            }
            return false;
        }
    }
}
