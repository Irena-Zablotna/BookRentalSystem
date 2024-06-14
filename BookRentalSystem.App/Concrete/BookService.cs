using BookRentalSystem.App.Common;
using BookRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.App.Concrete
{
    public class BookService:BaseService<Book>
    {
        public BookService()
        {
            InitializeBooks();
        }
        public void InitializeBooks()
        {
            Author author1 = new("Stephen", "King");
            Book book1 = new Book(1, author1, "It", "horror");
            Items.Add(book1);
            author1.authorBooks.Add(book1.Title);
            
            Book book5 = new Book(5, author1, "It", "horror");
            Items.Add(book5);
            author1.authorBooks.Add(book1.Title);

            Author author2 = new("Ken", "Follet");
            Book book2 = new Book(2, author2, "The Pillars of the Earth", "historical");
            Items.Add(book2);
            author2.authorBooks.Add(book2.Title);

            Author author3 = new("Rachel", "Abbot");
            Book book3 = new Book(3, author3, "Right Behind You", "detective");
            Items.Add(book3);
            author3.authorBooks.Add(book3.Title);

            Author author4 = new("Anthony", "De Barros");
            Book book4 = new Book(4, author4, "Practical SQL", "tecnical manual");
            Items.Add(book4);
            author4.authorBooks.Add(book4.Title);

        }

        public int AddBook(Book book)
        {
             string name = book.Author.Name;
             string surname = book.Author.Surname;
             Author author = new Author(name, surname);
             string title = book.Title;
             string category = book.Category;
             int id = Items.Count + 1;
             Book addedBook = new (id, author, title, category);
                Items.Add(addedBook);
                author.authorBooks.Add(addedBook.Title);
            return addedBook.Id;
            }


        public void RemoveBook(int removeId)
        {
                Book bookToRemove = null;
                foreach (Book book in Items)
                {
                    if (book.Id == removeId)
                    {
                        bookToRemove = book;
                        break;
                    }
                }
                if (bookToRemove != null)
                {
                    Items.Remove(bookToRemove);
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
            bool found = false; 

            foreach (var book in Items)
            {
                if (book.Author.Surname.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Title: {book.Title}, Category: {book.Category}");
                    found = true;
                    return book;
                }
            }
            if (!found)
            {
                Console.WriteLine("No books found for the specified author.");
            }
            return null;
        }
        public Book SearchBookByCategory()
        {
            Console.Write("Enter a category: ");
            string category = Console.ReadLine();
            bool found = false;
            foreach (var book in Items)
            {   
                
                if (book.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($" title {book.Title}, author {book.Author.Name}  {book.Author.Surname}, is book availabile? {book.IsAvailable}");
                    found = true;
                    return book;
                }
            }
            if (!found)
            {
                Console.WriteLine("No books found for the specified category.");
            }
            return null;
        }

        public void PrintCategories()
        {
            List<string> categories = new List<string>();
            Console.WriteLine("List of categories:");
            foreach (var book in Items)
            {
                categories.Add(book.Category.ToLower());
            }
            for (int i = 0; i < categories.Count; i++) {
                Console.WriteLine(categories[i]);
            }
        }
        public Book SearchBookByTitle()
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();
            bool found = false;

            foreach (var book in Items)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(book.ToString());
                    found = true;
                    return book;
                }
            }
            if (!found)
            {
                Console.WriteLine("No books found for the specified title.");
            }
            return null;
        }

        public void DisplayBookStatus()
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();

            foreach (var book in Items)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Book status {book.Title}: {(book.IsAvailable ? "Available" : "Rented till ")}{book.ReturnDate}");
                    return;
                }
            }
            Console.WriteLine("Book not found");
        }

        public int RentBook(string usernameNow)
        {
            Console.Write("Enter the title of the book you want to rent: ");
            string title = Console.ReadLine();

            foreach (var book in Items)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    if (book.IsAvailable)
                    {
                        int numberOfDays;
                        bool isValidInput = false;
                        while (!isValidInput)
                        {
                            Console.Write("Enter the number of days you want to rent the book for: ");
                            if (int.TryParse(Console.ReadLine(), out numberOfDays))
                            {
                                DateTime returnDate = DateTime.Now.AddDays(numberOfDays);
                                book.ReturnDate = returnDate;
                                book.IsAvailable = false;
                                Console.WriteLine($"You have rented {book.Title} for {numberOfDays} days. Return by: {returnDate.ToShortDateString()}.");
                                book.Users.Add(new User(usernameNow));
                                return book.Id;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number of days.");
                            }
                        }
                        isValidInput = true; 
                    }
                    else
                    {
                        Console.WriteLine($"{book.Title} is not available.");
                    }
                }
            }
            Console.WriteLine("Book not found.");
            return 0;
        }


        public int RateBook(User ratingUser)
        {
            Console.WriteLine("Please, enter a title of the book you want to rate");
            string title = Console.ReadLine();

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
            rentedBook.Ratings.Add(rating);
            Console.WriteLine($"Rating {rating} added successfully for the book {title}.");
            return rentedBook.Id;
        }


        public void ReadBookRatings()
        {
            //to do
        }


        public int ReturnBook()
        {
                Book book1 = new Book();
                int myId = RetrieveBookId();
                foreach (var book in Items)
                {
                    if (myId == book1.Id)
                    {
                        book.IsAvailable = true;
                    return book1.Id;
                    }
               else {
                    Console.WriteLine("Book not found.");
                    }
                }
            return 0;
           }

        public void DisplayStatistics()
        {
            //to do
        }

    }
}

