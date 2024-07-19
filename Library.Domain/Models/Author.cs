namespace Library.Domain.Models
{
    public class Author
    {
        public int AuthorID { get; set; }       // مُعرّف فريد لكل كاتب (المفتاح الأساسي)
        public string Name { get; set; }        // اسم الكاتب
        public DateTime DateOfBirth { get; set; } // تاريخ ميلاد الكاتب
        public string Nationality { get; set; } // جنسية الكاتب
        public string Biography { get; set; }   // سيرة ذاتية للكاتب
        public string Email { get; set; }       // بريد إلكتروني للكاتب
        public List<BookAuthor> BookAuthors { get; set; }
        public Author()
        {
            BookAuthors = new List<BookAuthor>();
        }

    }
}