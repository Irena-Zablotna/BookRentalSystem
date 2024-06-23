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
            AdminManager adminManager = new AdminManager(bookService,userService);
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
                else
                {
                    user = false;
                }
            }

            bool exitRequested = false;
            while (!exitRequested)
            {   
                Console.WriteLine("Let me know what you would like to do.\nPlease, enter Action id : \n");
                menuActionService.DisplayMenuByCategory(adm, user);
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            bookManager.SearchBookByAuthor();
                            Console.WriteLine();
                            break;
                        case 2:
                            bookService.PrintCategories();
                            bookManager.SearchBookByCategory();
                            Console.WriteLine();
                            break;
                        case 3:
                            bookManager.SearchBookByTitle();
                            Console.WriteLine();
                            break;
                        case 4:
                            bookManager.DisplayBookStatus();
                            Console.WriteLine();
                            break;
                        case 5:
                            bookManager.RentBook(usernameNow);
                            Console.WriteLine();
                            break;
                        case 6:
                            bookManager.ReturnBook(usernameNow);
                            Console.WriteLine();
                            break;
                        case 7:
                            bookManager.RateBook(usernameNow);
                            Console.WriteLine();
                            break;
                        case 8:
                            bookManager.ShowRatingByTitle();
                            Console.WriteLine();
                            break;
                        case 9:
                            bookManager.ShowMyBooks(usernameNow);
                            Console.WriteLine();
                            break;
                        case 10:
                            bookManager.AddBook();
                            Console.WriteLine();
                            break;
                        case 11:
                            bookManager.RemoveBookById();
                            break;
                        case 12:
                            adminManager.DisplayBookStatistics();
                            Console.WriteLine();
                            break;
                        case 13:
                            adminManager.DisplayUserStatistics();
                            Console.WriteLine();
                            break;
                        case 14:
                            adminManager.ViewBooksByUsername();
                            Console.WriteLine();
                            break;
                        case 15:
                            bookService.GetAll();
                            Console.WriteLine();
                            break;
                        case 16:
                            userService.GetAll();
                            Console.WriteLine();
                            break;
                        case 17:
                            adminManager.RemoveUserById();
                            Console.WriteLine();
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
