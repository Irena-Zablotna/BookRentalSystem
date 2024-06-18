using BookRentalSystem.App.Concrete;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Managers
{
    public class BookManager
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;
        public BookManager(BookService bookService, UserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }
        public int AddBook()
        {
            Console.WriteLine("Insert the name of the author: ");
            string name = Console.ReadLine();
            Console.WriteLine("Insert the surname name of the author: ");
            string surname = Console.ReadLine();
            Author author = new Author(name, surname);
            Console.WriteLine("Insert the title of the book: ");
            string title = Console.ReadLine();
            Console.WriteLine("Insert the category of the book: ");
            string category = Console.ReadLine();
            Book bookToAdd = new Book(author, title, category);
            _bookService.AddBook(bookToAdd);
            Console.WriteLine($"Book added succesfully. It's id = {bookToAdd.Id}");
            Console.WriteLine($"Title: {bookToAdd.Title}");
            Console.WriteLine($"Author: {bookToAdd.Author.Name} {bookToAdd.Author.Surname}");
            Console.WriteLine($"Category: {bookToAdd.Category}");
            Console.WriteLine($"Is Available: {bookToAdd.IsAvailable}");
            return bookToAdd.Id;
        }
        public int RetrieveBookId()
        {
            bool isValidInput = false;
            int myId = 0;
            while (!isValidInput)
            {
                Console.WriteLine("Please enter id of the book");
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
        public void RemoveBookById()
        {
            int bookId = RetrieveBookId();
            Book bookToRemove = _bookService.getItemById(bookId);
            bool result = _bookService.RemoveBook(bookToRemove);
            if (result)
            {
                Console.WriteLine($"Book {bookToRemove.Id} - {bookToRemove.Title} removed");
            }
            else
            {
                Console.WriteLine("Book not found");
            }
        }

        public Book SearchBookByAuthor()
        {
            Console.WriteLine("Enter the surname of the Author: ");
            string author = Console.ReadLine();
            Book foundBook = _bookService.ShowBooksByAuthor(author);
            if (foundBook != null)
            {
                Console.WriteLine($"Title: {foundBook.Title}, Category: {foundBook.Category}");
            }
            else
            {
                Console.WriteLine("No books found for the specified author.");
            }
            return foundBook;
        }
        public Book SearchBookByCategory()
        {
            Console.WriteLine("Enter a category: ");
            string category = Console.ReadLine();
            Book foundBook = _bookService.SearchBookByCategory(category);
            if (foundBook != null)
            {
                Console.WriteLine($" title {foundBook.Title}, author {foundBook.Author.Name}  {foundBook.Author.Surname}, is book availabile? {foundBook.IsAvailable}");
            }
            else
            {
                Console.WriteLine("No books found for the specified category.");
            }
            return foundBook;
        }
        public Book SearchBookByTitle()
        {
            Console.WriteLine("Enter the title of the book: ");
            string title = Console.ReadLine();
            Book foundBook = _bookService.SearchBookByTitle(title);
           
            if (foundBook != null)
            {
                Console.WriteLine(foundBook.ToString());
            }
            else
            {
                Console.WriteLine("No books found for the specified title.");
            }
            return foundBook;
        }
        public Book DisplayBookStatus()
        {
            Console.WriteLine("Enter the title of the book: ");
            string title = Console.ReadLine();
            Book foundBook = _bookService.SearchBookByTitle(title);
            if (foundBook != null)
            {
                Console.WriteLine($"Book status {foundBook.Title}: {(foundBook.IsAvailable ? "Available" : "Rented till ")}{foundBook.ReturnDate}");
            }
            else
            {
                Console.WriteLine("No books found for the specified title.");
            }
            return foundBook;
        }
        public int RentBook(string usernameNow)
        {
            bool correctValue = false;
            while (!correctValue)
            {
                Console.WriteLine("Enter the title of the book you want to rent: ");
                string title = Console.ReadLine();
                Book foundBook = _bookService.SearchBookByTitle(title);
                if (foundBook != null)
                {
                    bool isValidInput = false;
                    while (!isValidInput)
                    {
                        Console.WriteLine("Enter the number of days you want to rent the book for: ");
                        int numberOfDays;
                        if (int.TryParse(Console.ReadLine(), out numberOfDays))
                        {
                            isValidInput = true;
                            int rentedBookId = _bookService.RentBook(usernameNow, foundBook, numberOfDays);
                            var actualUser = _userService.GetUserByUsername(usernameNow);
                            foundBook.Users.Add(actualUser);
                            actualUser.Books.Add(foundBook);
                            if (foundBook.ReturnDate != null)
                            {
                                 Console.WriteLine($"You have rented {foundBook.Title} for {numberOfDays} days. Return by: {foundBook.ReturnDate.Value.ToShortDateString()}.");
                            }
                            else
                            {
                                Console.WriteLine($"You have rented {foundBook.Title} for {numberOfDays} days.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number of days.");
                        }
                    }
                    correctValue = true;
                    return foundBook.Id;
                }
                else
                {
                    Console.WriteLine("No books found for the specified title.");
                }
            }
            return 0;
        }
        public int RateBook(string username)
        {
            Console.WriteLine("Please, enter a title of the book you want to rate");
            string title = Console.ReadLine();
            var ratingUser = _userService.GetUserByUsername(username);
            Book rentedBook = _bookService.SearchBookByTitle(title);
            Book usersBook = ratingUser.Books.FirstOrDefault(b =>string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));

            if (rentedBook == null && string.Equals(rentedBook.Title, usersBook.Title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"User {ratingUser.Name} hasn't rented the book {title}.");
                return 0;
            }
            Console.WriteLine($"Please rate the book '{title}' (from 0 to 5): ");
            int rating = 0;
            bool isValidInput = false;
            while (!isValidInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out rating) && rating >= 0 && rating <= 5)
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid rating. Please enter a number from 0 to 5.");
                }
            }
            _bookService.AddRating(rating, rentedBook);
            Console.WriteLine($"Rating {rating} added successfully for the book {usersBook.Title}.");
            return rentedBook.Id;
        }

        public int ReturnBook(string username) { 
            Console.WriteLine("Please, enter a title of the book you want to return");
            string title = Console.ReadLine();
            var currentUser = _userService.GetUserByUsername(username);
            Book rentedBook = _bookService.SearchBookByTitle(title);
            Book usersBook = currentUser.Books.FirstOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
            if (usersBook == null && string.Equals(rentedBook.Title, usersBook.Title, StringComparison.OrdinalIgnoreCase) && !usersBook.IsAvailable ) {
                if (usersBook.ReturnDate< DateTime.Now) {
                    Console.WriteLine($"You're returning the book late. You should have returned the book on {usersBook.ReturnDate}" +
                        $"Next time, please adhere to the return date.");
                }
            _bookService.ReturnBook(rentedBook);
                Console.WriteLine($"Thank you for returning the book \" {usersBook.Title}\"");
                return rentedBook.Id;
            }
            return 0;
        }
    }
}
