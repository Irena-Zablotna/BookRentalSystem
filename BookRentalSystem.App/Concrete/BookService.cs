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
        private readonly UserService _userService;
        public BookService(UserService userService)
        {
            _userService = userService;
            Items = new List<Book>();
            InitializeBooks();
        }
        public void InitializeBooks()
        {
            Author author1 = new("Stephen", "King");
            Book book1 = new Book(author1, "It", "horror");
            book1.Ratings.Add(4);
            book1.Ratings.Add(2);
            book1.Ratings.Add(5);
            book1.Id = 1;
            book1.Users.Add(new(1, "Irena"));
            book1.Users.Add(new(2, "Robert"));
            Items.Add(book1);
            author1.authorBooks.Add(book1.Title);

            Book book5 = new Book(author1, "It", "horror");
            book5.IsAvailable = false;
            book5.Id = 2;
            Items.Add(book5);
            author1.authorBooks.Add(book5.Title); 

            Author author2 = new("Ken", "Follet");
            Book book2 = new Book(author2, "The Pillars of the Earth", "historical");
            book2.Id = 3;
            Items.Add(book2);
            author2.authorBooks.Add(book2.Title);

            Author author3 = new("Rachel", "Abbot");
            Book book3 = new Book(author3, "Right Behind You", "detective");
            book3.Id = 4;
            book1.Users.Add(new(1, "Irena"));
            Items.Add(book3);
            author3.authorBooks.Add(book3.Title);

            Author author4 = new("Anthony", "De Barros");
            Book book4 = new Book(author4, "Practical SQL", "tecnical manual");
            book4.Ratings.Add(4);
            book4.Ratings.Add(5);
            book4.Ratings.Add(5);
            book4.Id = 5;
            book4.Users.Add(new(1, "Irena"));
            book4.Users.Add(new(4, "Kajetan"));
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
             Book addedBook = new (author, title, category);
             addedBook.Id = Items.Count + 1;
            Items.Add(addedBook);
              author.authorBooks.Add(addedBook.Title);
            return addedBook.Id;
            }

        public bool RemoveBook(Book bookToRemove)
        {
                foreach (Book book in Items)
                {
                    if (book.Id == bookToRemove.Id)
                    {
                        bookToRemove = book;
                        break;
                    }
                }
                if (bookToRemove != null)
                {
                    Items.Remove(bookToRemove);
                    return true;
                }
            else
            {
                return false;
            }
          }

        public List<Book> SearchBookByAuthor(string author)
        {
            var matchingBooks = Items
                .Where(b => b.Author.Surname.Equals(author, StringComparison.OrdinalIgnoreCase))
                .GroupBy(b => b.Title) 
                .Select(g => g.First()) 
                .ToList();
            return matchingBooks;
        }

        public Book ShowBooksByAuthor(string author)
        {
            List<Book> foundBooks =SearchBookByAuthor(author);
            if (foundBooks.Any())
            {
                foreach (var book in foundBooks)
                {
                    return book;
                }
            }
            return null;
        }
        public Book SearchBookByCategory(string category)
        {
            foreach (var book in Items)
            {   
                if (book.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            } 
            return null;
        }

        public void PrintCategories()
        {
            HashSet<string> categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("List of categories:");
            foreach (var book in Items)
            {
                categories.Add(book.Category.ToLower());
            }
            int counter = 1;
            foreach (var category in categories)
            {
                Console.WriteLine($"{counter}. {category}");
                counter++;
            }
        }


        public List <Book> GetAll()
        {
           var  booksToShow = Items.ToList();
            return booksToShow;
        }
        public Book SearchBookByTitle(string title)
        {
            var matchingBooks = Items.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            var availableBook = matchingBooks.FirstOrDefault(b => b.IsAvailable);
            if (availableBook != null)
            {
                return availableBook;
            }
            return matchingBooks.FirstOrDefault();
        }

        public List <Book> SearchBooksByUser(string username) { 
        User currentUser = _userService.GetUserByUsername(username);
        var userBooks = currentUser.Books.ToList();
        return userBooks;
        }

        public int RentBook(string usernameNow, Book bookToRent, int numberOfDays)
        {
            if (bookToRent.IsAvailable)
            {
                DateTime returnDate = DateTime.Now.AddDays(numberOfDays);
                bookToRent.ReturnDate = returnDate;
                bookToRent.RentDate = DateTime.Now;
                bookToRent.IsAvailable = false;
                return bookToRent.Id;
            }
            return 0;
        }


        public int AddRating(int rating, Book book)
        {
            if (book == null)
            {
                var ratings = book.Ratings.ToList();
                ratings.Add(rating);
            }
           return book.Id;
        }



        public List<int> ReadBookRatings(Book book)
        {
            var ratings = book.Ratings.ToList();
            return ratings;
        }


        public int ReturnBook(Book bookToReturn)
        {
            if (bookToReturn != null)
            {
                bookToReturn.IsAvailable = true;
                bookToReturn.ReturnDate= DateTime.Now;
                return bookToReturn.Id;
            }
            else {
                Console.WriteLine("Book not found.");
            }
            return 0;
           }

        public Book MostRented()
        {
            var mostRentedBook = Items.OrderByDescending(b => b.Users.Count).FirstOrDefault();
            return mostRentedBook;
        }

        public Book LeastRented()
        {
            var leastRentedBook = Items.OrderBy(b => b.Users.Count).FirstOrDefault();
            return leastRentedBook;
        }

        public Book BestRated()
        {
            var bestRatedBook = Items
                .Where(b => b.Ratings.Count > 0)
                .OrderByDescending(b => b.Ratings.Average())
                .FirstOrDefault();
            return bestRatedBook;
        }
        public Book WorstRated()
        {
            var worstRatedBook = Items
                .Where(b => b.Ratings.Count > 0)
                .OrderBy(b => b.Ratings.Average())
                .FirstOrDefault();
            return worstRatedBook;
        }
    }
}

