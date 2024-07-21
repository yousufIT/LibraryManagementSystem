namespace Library.Domain.Models
{
    public class Category
    {
        public int CategoryID { get; set; }    
        public string Name { get; set; }        
        public int? ParentCategoryID { get; set; }

        // التنقل بين العلاقات
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Book> Books { get; set; }

        public Category()
        {
            Books = new List<Book>();
            SubCategories = new List<Category>();
        }
    }
}