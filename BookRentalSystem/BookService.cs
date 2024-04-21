using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem
{
    public class BookService
    {
        private List<Book> books = new List<Book>();

        public void InitializeBooks()
        {
            books.Add(new Book(1,"Author1", "Title1", "Category1" ));
            books.Add(new Book(2,"Author2", "Title2", "Category2" ));
            books.Add(new Book(3,"Author3", "Title3", "Category1"));
            books.Add(new Book(4,"Author4", "Title4", "Category3"));
        }

        public void AddBook()
        {
            Console.Write("Insert the author of the book: ");
            string author = Console.ReadLine();
            Console.Write("Insert the title of the book: ");
            string title = Console.ReadLine();
            Console.Write("Insert the category of the book: ");
            string category = Console.ReadLine();
            int id = books.Count + 1;

            books.Add(new Book(id, author, title, category));
            Console.WriteLine("Book added succesfully.");
        }


        public void RemoveBook(int id)
        {
            // Implementazione...
        }

        public void SearchBookByAuthor()
        {
            // Implementazione...
        }

        public void SearchBookByCategory()
        {
            // Implementazione...
        }

        public void SearchBookByTitle()
        {
            // Implementazione...
        }

        public void DisplayBookStatus()
        {
            // Implementazione...
        }

        public void RentBook()
        {
            // Implementazione...
        }

        public void WriteBookReview()
        {
            // Implementazione...
        }

        public void ReadBookReviews()
        {
            // Implementazione...
        }

      


        public void DisplayStatistics()
        {
            // Implementazione...
        }
    }
}

