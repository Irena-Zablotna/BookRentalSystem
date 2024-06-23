using BookRentalSystem.App.Concrete;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Managers
{
   public class AdminManager
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;
        public AdminManager(BookService bookService, UserService userService)
        {
            _bookService = bookService; 
            _userService = userService; 
        }

        public void ViewBooksByUsername ()
        {
            Console.WriteLine("Enter the username of the user whose rental history you want to view.");
            string username = Console.ReadLine();
            List<Book> userBooks = _bookService.SearchBooksByUser(username);
            if (userBooks.Count > 0)
            {
                Console.WriteLine($"The history of rented books of : {username}");
                foreach (var book in userBooks)
                {
                    Console.WriteLine($" author: {book.Author.Name} {book.Author.Surname}, title: {book.Title}, rented on:{book.RentDate.Value.ToShortDateString()}, date of return:{book.ReturnDate.Value.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine($"{username} haven't rented any books yet.");
            }
        }

        public int RetrieveUserId()
        {
            bool isValidInput = false;
            int myId = 0;
            while (!isValidInput)
            {
                Console.WriteLine("Please enter id of the user");
                if (int.TryParse(Console.ReadLine(), out myId))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter only numbers.");
                    isValidInput = false;
                }
            }
            return myId;
        }
        public void RemoveUserById(){
            int userId = RetrieveUserId();
            bool removed = _userService.RemoveUserById(userId);
            if (removed) {
                Console.WriteLine($"The user {userId} removed succesfully");
            }
            else
            {
                Console.WriteLine($"User {userId} not found");
            }
        }
        public void ShowAll()
        {
            var booksToShow = _bookService.GetAll();
            Console.WriteLine("The list of all books:");
            foreach (var book in booksToShow)
            {
                Console.WriteLine(book.ToString());
            }
        }
        public void DisplayBookStatistics()
        {
            List<Book> bookList = _bookService.GetAll();
            if (bookList.Count == 0)
            {
                Console.WriteLine("No books available in the system.");
                return;
            }
            var mostRentedBook = _bookService.MostRented();
            if (mostRentedBook != null)
            {
                Console.WriteLine($"Most rented book: id {mostRentedBook.Id} {mostRentedBook.Title} by {mostRentedBook.Author.Name} {mostRentedBook.Author.Surname}(Rented {mostRentedBook.Users.Count} times)");
            }
            var leastRentedBook = _bookService.LeastRented();
            if (leastRentedBook != null)
            {
                Console.WriteLine($"Least rented book: id {leastRentedBook.Id} {leastRentedBook.Title} by {leastRentedBook.Author.Name} {leastRentedBook.Author.Surname}  (Rented {leastRentedBook.Users.Count} times)");
            }
            var bestRatedBook = _bookService.BestRated();
            if (bestRatedBook != null && bestRatedBook.Ratings.Count > 0)
            {
                Console.WriteLine($"Best rated book: id {bestRatedBook.Id} {bestRatedBook.Title} by {bestRatedBook.Author.Name} {bestRatedBook.Author.Surname} (Average rating {bestRatedBook.Ratings.Average():F2})");
            }
            else
            {
                Console.WriteLine("Best rated book: No ratings available.");
            }
            var worstRatedBook = _bookService.WorstRated();
            if (worstRatedBook != null && worstRatedBook.Ratings.Count > 0)
            {
                Console.WriteLine($"Worst rated book: id {worstRatedBook.Id} {worstRatedBook.Title} by {worstRatedBook.Author.Name} {worstRatedBook.Author.Surname} (Average rating {worstRatedBook.Ratings.Average():F2})");
            }
            else
            {
                Console.WriteLine("Worst rated book: No ratings available.");
            }
        }
        public void DisplayUserStatistics()
        {
            Console.WriteLine("User Statistics:");
            var mostRentingUser = _userService.MostRenting();
            if (mostRentingUser != null)
            {
                Console.WriteLine($"User who rented the most books: id  {mostRentingUser.Id}. {mostRentingUser.Name} (Rented {mostRentingUser.Books.Count} books)");
            }
            var leastRentingUser = _userService.LeastRenting();
            if (leastRentingUser != null)
            {
                Console.WriteLine($"User who rented the least books: id {leastRentingUser.Id}. {leastRentingUser.Name} (Rented {leastRentingUser.Books.Count} books)");
            }
            var zeroRentingUsers = _userService.ZeroRenting();
            if (zeroRentingUsers.Count > 0) {
                Console.WriteLine("These users have not rented any books yet.");
                foreach ( var user in zeroRentingUsers ) 
                { 
                    Console.WriteLine($" id {user.Id}, {user.Name}" ); 
                }
            }
        }
    }
}
