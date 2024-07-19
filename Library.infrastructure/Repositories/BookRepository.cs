using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>,IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
        //للحصول على الكتب استنادًا إلى الفئة الرئيسية أو الفرعية
        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId, bool isMainCategory)
        {
            if (isMainCategory)
            {
                var books = await _context.Books
                            .Where(b => b.MainCategoryID == categoryId)
                            .ToListAsync();
                return books;

            }
            else
            {
                var books = await _context.Books
                            .Where(b => b.SubCategoryID == categoryId)
                            .ToListAsync();
                return books;
            }

        }
        //للحصول على الكتب التي كتبها مؤلف معين
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            var books= await _context.Books
                       .Where(b => b.BookAuthors.Any(ba => ba.AuthorID == authorId))
                       .ToListAsync();
            return books;
        }

    }
}
