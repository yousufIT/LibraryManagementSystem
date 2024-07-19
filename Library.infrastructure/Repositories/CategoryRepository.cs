using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context)
        {
        }
        //للحصول على الفئات الفرعية لفئة رئيسية معينة
        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int mainCategoryId)
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryID == mainCategoryId)
                .ToListAsync();
        }
        //للحصول على جميع الفئات الرئيسية
        public async Task<IEnumerable<Category>> GetMainCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryID == null)
                .ToListAsync();
        }
        ////للحصول على الفئات مع الكتب المرتبطة بها
        //public async Task<IEnumerable<Category>> GetCategoriesWithBooksAsync()
        //{
        //    return await _context.Categories
        //        .Include(c => c.Books)
        //        .ToListAsync();
        //}


    }
}
