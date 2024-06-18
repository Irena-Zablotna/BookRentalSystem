using BookRentalSystem.App.Concrete;
using BookRentalSystem.App.Managers;
using BookRentalSystem.Domain.Entity;

namespace BookRentalSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            BookService bookService = new BookService( userService);
            MenuActionService menuActionService = new MenuActionService();
            UserManager userManager = new UserManager(userService);
            BookManager bookManager = new BookManager(bookService, userService);
            bool adm = false;
            bool user = false;
            int choice;
            menuActionService.PrintWelcomeMessage();
            string usernameNow = userManager.RetrieveUsername();
            User actualUser = new User(usernameNow);
            int checkUser = userService.VerifyUser(usernameNow);

            if (checkUser == 1)
            {
                adm = true;
            }
            if (checkUser == 2)
            {
                user = true;
            }
            else if (checkUser == 0)
            {
               actualUser = userManager.RegisterUser(usernameNow);
                if (actualUser != null)
                {
                    user = true;
                    usernameNow = actualUser.Name;
                }
            }

            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.Clear();
                Console.Write("Let me know what you would like to do.\nPlease, enter Action id : \n");
                menuActionService.DisplayMenuByCategory(adm, user);
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            bookManager.SearchBookByAuthor();
                            break;
                        case 2:
                            bookService.PrintCategories();
                            bookManager.SearchBookByCategory();
                            break;
                        case 3:
                            bookManager.SearchBookByTitle();
                            break;
                        case 4:
                            bookManager.DisplayBookStatus();
                            break;
                        case 5:
                            bookManager.RentBook(usernameNow);
                            break;
                        case 6:
                            bookManager.RateBook(usernameNow);
                            break;
                        case 7:
                            bookManager.ReturnBook(usernameNow);
                            break;
                        case 8:
                            Console.WriteLine("Work in progress");
                            break;
                        case 9:
                            bookManager.AddBook();
                            break;
                        case 10:
                            bookManager.RemoveBookById();
                            break;
                        case 11:
                            Console.WriteLine("Work in progress");
                            break;
                        case 12:
                            bookService.GetAll();
                            break;
                        case 0:
                            Console.WriteLine("Hello, thank you for using our Book Rental System.");
                            Thread.Sleep(1500);
                            exitRequested = true;
                            break;
                        default:
                            Console.WriteLine("Action you entered does not exist");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The value you entered is incorrect, try again");
                }
            }

        }
    }
  }
