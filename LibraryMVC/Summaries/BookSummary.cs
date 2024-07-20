namespace LibraryMVC.Summaries
{
    public class BookSummary
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public int MainCategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int? AuthorId { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public string Edition { get; set; }
        public string Format { get; set; }
        public string Location { get; set; }
    }
}
