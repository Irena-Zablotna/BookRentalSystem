using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class UserService
    {
        private List<User> users = new List<User>();

        public void InitalizeUsers()
        {
            User user1 = new User(1, "Admin");
            users.Add(user1);
            user1.IsAdmin = true;

            users.Add(new(2, "Irena"));
            users.Add(new(3, "Robert"));
        }
        public bool VerifyAdmin(string username)
        {
            foreach (var user in users)
            {
                if (username == user.Name && user.IsAdmin == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
    }
