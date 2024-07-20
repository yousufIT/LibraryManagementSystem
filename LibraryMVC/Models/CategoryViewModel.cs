using LibraryMVC.Summaries;

namespace LibraryMVC.Models
{
    public class CategoryViewModel
    {
        public int SelectedMainCategoryId { get; set; }
        public int SelectedSubCategoryId { get; set; }
        public IEnumerable<CategorySummary> MainCategories { get; set; }
        public IEnumerable<CategorySummary> SubCategories { get; set; }
    }
}
