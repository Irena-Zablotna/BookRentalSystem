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
        public BookManager(BookService bookService)
        {
            _bookService = bookService;
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
            Book bookToAdd = new Book(author, title, category );
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
    }
}
