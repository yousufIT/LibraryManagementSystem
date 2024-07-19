using Library.infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class DropdownController : Controller
{
    private readonly LibraryDbContext _context;

    public DropdownController(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
                        .Where(c=>c.ParentCategoryID!=null)
                        .ToListAsync();
        return View(categories);
    }

    public async Task<JsonResult> GetSubcategories(int categoryId)
    {
        var subcategories = await _context.Categories
                                .Where(s => s.CategoryID == categoryId && s.ParentCategoryID!=null)
                                .ToListAsync();
        return Json(subcategories);
    }
}
