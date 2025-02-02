using System;
using System.Collections.Generic;

namespace AdvancedCSharp
{
    #region Book Class
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Published: {PublicationDate:yyyy-MM-dd}";
        }
    }
    #endregion

    #region BookFunctions Class
    public class BookFunctions
    {
        public delegate string BookDelegate(Book book); // User-defined delegate datatype

        public static string GetTitle(Book book)
        {
            return book.Title;
        }

        public static string GetAuthor(Book book)
        {
            return book.Author;
        }
    }
    #endregion

    #region LibraryEngine Class
    public class LibraryEngine
    {
        public void ProcessBooks(List<Book> books, Func<Book, string> fPtr)
        {
            foreach (var book in books)
            {
                Console.WriteLine(fPtr(book));
            }
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book { Title = "C# in Depth", Author = "Jon Skeet", ISBN = "9781617294532", PublicationDate = new DateTime(2019, 3, 23) },
                new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "9780132350884", PublicationDate = new DateTime(2008, 8, 1) }
            };

            LibraryEngine library = new LibraryEngine();

            //Using user-defined delegate
            BookFunctions.BookDelegate titleDelegate = BookFunctions.GetTitle;
            library.ProcessBooks(books, titleDelegate);

            //Using BCL delegate Func<Book, string>
            Func<Book, string> authorFunc = BookFunctions.GetAuthor;
            library.ProcessBooks(books, authorFunc);

            //Using Anonymous Method (GetISBN)
            Func<Book, string> getISBN = delegate (Book book)
            {
                return book.ISBN;
            };
            library.ProcessBooks(books, getISBN);

            //Using Lambda Expression (GetPublicationDate)
            Func<Book, string> getPublicationDate = book => book.PublicationDate.ToString("yyyy-MM-dd");
            library.ProcessBooks(books, getPublicationDate);
        }
    }
}
