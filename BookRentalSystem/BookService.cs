using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class BookService
    {
        private List<Book> books = new List<Book>();
        

        public void InitializeBooks()
        {
            Author author1 = new("Stephen", "King");
            Book book1 = new Book(1, author1, "It", "Horror");
            books.Add(book1);
            author1.authorBooks.Add(book1.Title);

            Author author2 = new("Ken", "Follet");
            Book book2 = new Book(2, author2, "The Pillars of the Earth", "Historical");
            books.Add(book2);
            author2.authorBooks.Add(book2.Title);

            Author author3 = new("Rachel", "Abbot");
            Book book3 = new Book(3, author3, "Right Behind You", "Detective");
            books.Add(book3);
            author3.authorBooks.Add(book3.Title);

        }

        public void AddBook()
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
                int id = books.Count + 1;
                Book addedBook = new(id, author, title, category);
                books.Add(addedBook);
                author.authorBooks.Add(addedBook.Title);
                Console.WriteLine($"Book added succesfully. It's id = {id}");
                Console.WriteLine($"Title: {addedBook.Title}");
                Console.WriteLine($"Author: {addedBook.Author.Name} {addedBook.Author.Surname}");
                Console.WriteLine($"Category: {addedBook.Category}");
                Console.WriteLine($"Is Available: {addedBook.IsAvailable}");
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

        public void RemoveBook()
        {
                int removeId = RetrieveBookId();
                Book bookToRemove = null;
                foreach (Book book in books)
                {
                    if (book.Id == removeId)
                    {
                        bookToRemove = book;
                        break;
                    }
                }
                if (bookToRemove != null)
                {
                    books.Remove(bookToRemove);
                    Console.WriteLine($"Book {bookToRemove.Id} - {bookToRemove.Title} removed");
                }
                else
                {
                    Console.WriteLine("Book not found");
                }
             }

        public void SearchBookByAuthor()
        {
            Console.Write("Enter the surname of the Author: ");
            string author = Console.ReadLine();
            bool found = false; 

            foreach (var book in books)
            {
                if (book.Author.Surname.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Title: {book.Title}, Category: {book.Category}");
                    found = true; 
                }
            }
            if (!found)
            {
                Console.WriteLine("No books found for the specified author.");
            }
        }
        public void SearchBookByCategory()
        {
            Console.Write("Enter a category: ");
            string category = Console.ReadLine();
            bool found = false;
            foreach (var book in books)
            {   
                
                if (book.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($" title {book.Title}, author {book.Author.Name}  {book.Author.Surname}, is book availabile? {book.IsAvailable}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("No books found for the specified category.");
            }
        }
        public void PrintCategories()
        {
            List<string> categories = new List<string>();   
            foreach (var book in books)
            {
                categories.Add(book.Category);
            }
            for (int i = 0; i < categories.Count; i++) {
                Console.WriteLine(categories[i]);
            }
        }
        public void SearchBookByTitle()
        {
           Console.WriteLine("to do");
        }

        public void DisplayBookStatus()
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();

            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Book status '{book.Title}': {(book.IsAvailable ? "Available" : "Rented")}");
                    return;
                }
            }
            Console.WriteLine("Book not found");
        }

            public void RentBook(bool checkuser, string username)
            {
                Console.Write("Enter the title of the book you want to rent: ");
                string title = Console.ReadLine();

                foreach (var book in books)
                {
                    if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (book.IsAvailable)
                        {
                            book.IsAvailable = false;
                            Console.WriteLine($"You have rented {book.Title}.");

                        }
                        else

                            Console.WriteLine($"{book.Title} is not available.");
                    }
                    return;
                }
                Console.WriteLine("Book not found.");
            }
            
        

        public void RateBook()
        {
            Console.WriteLine("to do");
        }

        public void ReadBookRatings()
        {
            Console.WriteLine("to do");
        }


        public void ReturnBook()
        {
                Book book1 = new Book();
                int myId = RetrieveBookId();
                foreach (var book in books)
                {
                    if (myId == book1.Id)
                    {
                        book.IsAvailable = true;
                    }
                }
            }

        public void DisplayStatistics()
        {
            Console.WriteLine("to do");
        }
    }
}

