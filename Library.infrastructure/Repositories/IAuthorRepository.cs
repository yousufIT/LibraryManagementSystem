using Library.Domain.Models;

namespace Library.infrastructure.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsByBookAsync(int bookId);
        Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(string nationality);
    }
}