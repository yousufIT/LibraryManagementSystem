using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class Book
    {
        public int BookID { get; set; }         // Primary key for the book
        public string Title { get; set; }       // Title of the book
        public DateTime PublishedDate { get; set; }  // Date when the book was published
        public string Description { get; set; } // Description or summary of the book
        public int? MainCategoryID { get; set; } // Foreign key for main category of the book
        public int? SubCategoryID { get; set; }  // Foreign key for subcategory of the book
        public string Publisher { get; set; }   // Name of the publisher
        public int Pages { get; set; }          // Number of pages in the book
        public string Language { get; set; }    // Language in which the book is written
        public decimal Price { get; set; }      // Price of the book
        public string Edition { get; set; }     // Edition of the book
        public string Format { get; set; }      // Format of the book (e.g., hardcover, paperback)
        public string Location { get; set; }    // Physical location of the book in the library

        // Navigation properties
        public  Category MainCategory { get; set; } // Navigation property for main category
        public  Category SubCategory { get; set; }  // Navigation property for subcategory
        public  List<BookAuthor> BookAuthors { get; set; }
        public Book()
        {
            BookAuthors = new List<BookAuthor>();
        }
    }
}
