using BookRentalSystem.App.Common;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        private List<MenuAction> actionsToChoose;
        UserService userService = new UserService();
        public MenuActionService()
        {
            actionsToChoose = new List<MenuAction>();
            InitializeMenu();
        }

        public void InitializeMenu()
        {
            actionsToChoose.Add(new(1, "Search book by author", "userCategory"));
            actionsToChoose.Add(new(2, "Search book by category", "userCategory"));
            actionsToChoose.Add(new(3, "Search book by title", "userCategory"));
            actionsToChoose.Add(new(4, "View book status", "userCategory"));
            actionsToChoose.Add(new(5, "Rent the book", "userCategory"));
            actionsToChoose.Add(new(6, "Rate the book", "userCategory"));
            actionsToChoose.Add(new(7, "Return the book", "userCategory"));
            actionsToChoose.Add(new(8, "Read ratings by title", "userCategory"));
            actionsToChoose.Add(new(12, "View your history of rented books", "userCategory"));

            actionsToChoose.Add(new(9, "Add a new book", "adminCategory"));
            actionsToChoose.Add(new(10, "Remove a book", "adminCategory"));
            actionsToChoose.Add(new(11, "Display statistics of books", "adminCategory"));
            actionsToChoose.Add(new(13, "Display statistics of Users", "adminCategory"));
            actionsToChoose.Add(new(14, "View rented books by Username", "adminCategory"));
            actionsToChoose.Add(new(15, "View all books", "adminCategory"));
            actionsToChoose.Add(new(16, "View all users", "adminCategory"));
            actionsToChoose.Add(new(0, "Exit", null));

        }

        public void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to our automatic Book Rental Service!");

        }
        public void DisplayMenuByCategory(bool adm, bool user)
        {
            if (adm)
            {
                foreach (var menuAction in actionsToChoose)
                {
                    if (menuAction.MenuCategory == "adminCategory" || menuAction.MenuCategory == "userCategory" || menuAction.MenuCategory == null)
                    {
                        Console.WriteLine($"{menuAction.Id} {menuAction.Name}");
                    }
                }
            }
            if (user)
            {
                foreach (var menuAction in actionsToChoose)
                {
                    if (menuAction.MenuCategory == "userCategory" || menuAction.MenuCategory == null)
                    {
                        Console.WriteLine($"{menuAction.Id} {menuAction.Name}");
                    }
                }
            }
            if (!adm && !user)
            {
                foreach (var menuAction in actionsToChoose)
                {
                    if (menuAction.MenuCategory == null)
                    {
                        Console.WriteLine($"{menuAction.Id} {menuAction.Name}");
                    }
                }
            }
        }
    }
}
