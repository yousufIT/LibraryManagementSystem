using Library.Domain.Models;

namespace Library.infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int mainCategoryId);
        Task<IEnumerable<Category>> GetMainCategoriesAsync();
    }
}