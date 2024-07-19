namespace Library.Domain.Models
{
    public class Category
    {
        public int CategoryID { get; set; }     // Unique identifier for each category (Primary Key)
        public string Name { get; set; }        // Name of the category
        public int? ParentCategoryID { get; set; } // معرف الفئة الرئيسية إذا كانت هذه فئة فرعية

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