namespace BookRentalSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
           UserService userService = new UserService();
           BookService bookService = new BookService();
           MenuActionService menuActionService = new MenuActionService();
            bookService.InitializeBooks();
            userService.InitalizeUsers();
            menuActionService.InitializeMenu();
            bool adm = false;
            bool user = false;
           
            menuActionService.PrintWelcomeMessage();
            string usernameNow;    
           
                while (true)
            {
                menuActionService.DisplayMenuByCategory(adm, user);
                Console.WriteLine("More features are available only for registered users, do you want to proceed (y/n)" );
                if (Console.ReadLine().ToLower() != "y")
                {
                    break;
                }
                usernameNow = userService.RetrieveUsername();
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
                    string result = userService.RegisterUser(usernameNow);
                    if (!String.IsNullOrEmpty(result)){
                        user = true;
                        usernameNow = result;
                    }
                }
                menuActionService.DisplayMenuByCategory(adm, user);
                Console.Write("\nPlease, enter Action id : ");
                int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                        bookService.SearchBookByAuthor(); 
                            break;
                        case 2:
                        bookService.PrintCategories();
                        bookService.SearchBookByCategory();
                            break;
                        case 3:bookService.SearchBookByTitle();
                            break;
                        case 4:
                        bookService.DisplayBookStatus();
                            break;
                        case 5:
                         string rentedBook = bookService.RentBook(checkUser, usernameNow);
                        if (!String.IsNullOrEmpty(rentedBook))
                        {
                            User actualUser = new User(usernameNow);
                            actualUser.Books.Add(new Book(rentedBook));
                        }
                        break;
                        case 6:
                        bookService.RateBook(usernameNow);
                        break;
                        case 7:
                        Console.WriteLine("Work in progress");
                        break;
                        case 8:
                        Console.WriteLine("Work in progress");
                        break;
                        case 9:
                        bookService.AddBook();
                        break;
                        case 10:
                        Console.WriteLine("Work in progress");
                        break;
                        case 11:
                        Console.WriteLine("Work in progress");
                        break;
                         case 12:
                        Console.WriteLine("Work in progress");
                        break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                    }
                if (choice == 0)
                    break;

                if (choice >= 12)
                    continue;

                Console.Write("Would you like to return to the main menu? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                    break;
            }
        }
    }
}
