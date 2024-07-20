namespace LibraryMVC.Summaries
{
    public class CategorySummary
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
    }
}