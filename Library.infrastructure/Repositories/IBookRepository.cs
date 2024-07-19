using Library.Domain.Models;

namespace Library.infrastructure.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId, bool isMainCategory);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    }
}