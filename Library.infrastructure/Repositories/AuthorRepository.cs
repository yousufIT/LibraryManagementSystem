using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }
        //للحصول على المؤلفين الذين كتبوا كتابًا معينًا
        public async Task<IEnumerable<Author>> GetAuthorsByBookAsync(int bookId)
        {
            return await _context.Authors
                .Where(a => a.BookAuthors.Any(ba => ba.BookID == bookId))
                .ToListAsync();
        }
        //للحصول على المؤلفين استنادًا إلى الجنسية
        public async Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(string nationality)
        {
            return await _context.Authors.Where(a => a.Nationality == nationality).ToListAsync();
        }


    }
}
