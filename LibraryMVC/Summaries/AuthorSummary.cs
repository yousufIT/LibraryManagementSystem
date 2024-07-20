namespace LibraryMVC.Summaries
{
    public class AuthorSummary
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? BookID { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
    }
}