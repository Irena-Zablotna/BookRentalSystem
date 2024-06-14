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
            BookService bookService = new BookService();
            MenuActionService menuActionService = new MenuActionService();
            UserManager userManager = new UserManager(userService);
            BookManager bookManager = new BookManager(bookService);
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
                bool isRegistered = userManager.RegisterUser(usernameNow);
                if (isRegistered)
                {
                    user = true;
                }
            }

            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.Write("Let me know what you would like to do.\nPlease, enter Action id : \n");
                menuActionService.DisplayMenuByCategory(adm, user);
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            bookService.SearchBookByAuthor();
                            break;
                        case 2:
                            bookService.PrintCategories();
                            bookService.SearchBookByCategory();
                            break;
                        case 3:
                            bookService.SearchBookByTitle();
                            break;
                        case 4:
                            bookService.DisplayBookStatus();
                            break;
                        case 5:
                            
                            int rentedBook = bookService.RentBook(usernameNow);
                            if (rentedBook != null)
                            {
                                actualUser.Books.Add(new Book(rentedBook));
                            }
                            break;
                        case 6:
                            bookService.RateBook(actualUser);
                            break;
                        case 7:
                            Console.WriteLine("Work in progress");
                            break;
                        case 8:
                            Console.WriteLine("Work in progress");
                            break;
                        case 9:
                            bookManager.AddBook();
                            break;
                        case 10:
                           bookService.RemoveBook();
                            break;
                        case 11:
                            Console.WriteLine("Work in progress");
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
