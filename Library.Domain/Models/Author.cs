namespace Library.Domain.Models
{
    public class Author
    {
        public int AuthorID { get; set; }       
        public string Name { get; set; }   
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; } 
        public string Biography { get; set; } 
        public string Email { get; set; }       
        public List<BookAuthor> BookAuthors { get; set; }
        public Author()
        {
            BookAuthors = new List<BookAuthor>();
        }

    }
}