using BookRentalSystem.App.Concrete;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.Write("Insert the name of the author: ");
            string name = Console.ReadLine();
            Console.Write("Insert the surname name of the author: ");
            string surname = Console.ReadLine();
            Author author = new Author(name, surname);
            Console.Write("Insert the title of the book: ");
            string title = Console.ReadLine();
            Console.Write("Insert the category of the book: ");
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
            Console.Write("Enter the surname of the Author: ");
            string author = Console.ReadLine();
            Book foundedBook = _bookService.ShowBooksByAuthor(author);
            if (foundedBook != null)
            {
                Console.WriteLine($"Title: {foundedBook.Title}, Category: {foundedBook.Category}");
            }
            else
            {
                Console.WriteLine("No books found for the specified author.");
            }
            return foundedBook;
        }
        public Book SearchBookByCategory()
        {
            Console.Write("Enter a category: ");
            string category = Console.ReadLine();
            Book foundedBook = _bookService.SearchBookByCategory(category);
            if (foundedBook != null)
            {
                Console.WriteLine($" title {foundedBook.Title}, author {foundedBook.Author.Name}  {foundedBook.Author.Surname}, is book availabile? {foundedBook.IsAvailable}");
            }
            else
            {
                Console.WriteLine("No books found for the specified category.");
            }
            return foundedBook;
        }
        public Book SearchBookByTitle()
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();
            Book foundedBook = _bookService.SearchBookByTitle(title);
            if (foundedBook != null)
            {
                Console.WriteLine(foundedBook.ToString());
            }
            else
            {
                Console.WriteLine("No books found for the specified title.");
            }
            return foundedBook;
        }
        public Book DisplayBookStatus()
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();
            Book foundedBook = _bookService.SearchBookByTitle(title);
            if (foundedBook != null)
            {
                Console.WriteLine($"Book status {foundedBook.Title}: {(foundedBook.IsAvailable ? "Available" : "Rented till ")}{foundedBook.ReturnDate}");
            }
            else
            {
                Console.WriteLine("No books found for the specified title.");
            }
            return foundedBook;
        }
        public int RentBook(string usernameNow)
        {
            bool correctValue = false;
            while (!correctValue)
            {
                Console.Write("Enter the title of the book you want to rent: ");
                string title = Console.ReadLine();
                Book foundedBook = _bookService.SearchBookByTitle(title);
                if (foundedBook != null)
                {
                    bool isValidInput = false;
                    while (!isValidInput)
                    {
                        Console.Write("Enter the number of days you want to rent the book for: ");
                        int numberOfDays;
                        if (int.TryParse(Console.ReadLine(), out numberOfDays))
                        {
                            isValidInput = true;
                            int rentedBookId = _bookService.RentBook(usernameNow, foundedBook, numberOfDays);
                            var actualUser = _userService.GetUserByUsername(usernameNow);
                            foundedBook.Users.Add(actualUser);
                            actualUser.Books.Add(foundedBook);
                            if (foundedBook.ReturnDate != null)
                            {
                               Console.WriteLine($"DEBUG: Return date is set to: {foundedBook.ReturnDate}");
                        Console.WriteLine($"You have rented {foundedBook.Title} for {numberOfDays} days. Return by: {foundedBook.ReturnDate.Value.ToShortDateString()}.");
                            }
                            else
                            {
                                Console.WriteLine($"You have rented {foundedBook.Title} for {numberOfDays} days.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number of days.");
                        }
                    }
                    correctValue = true;
                    return foundedBook.Id;
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
            Book rentedBook = ratingUser.Books.FirstOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
            if (rentedBook == null)
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
            _bookService.AddRating(rating, rentedBook)
            Console.WriteLine($"Rating {rating} added successfully for the book {title}.");
            return rentedBook.Id;
        }

    }
}
