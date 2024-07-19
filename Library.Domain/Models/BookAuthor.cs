namespace Library.Domain.Models
{
    public class BookAuthor
    {
        public int BookID { get; set; }     // Foreign key to Book entity
        public Book Book { get; set; }      // Navigation property to Book

        public int AuthorID { get; set; }   // Foreign key to Author entity
        public Author Author { get; set; }  // Navigation property to Author
    }
}