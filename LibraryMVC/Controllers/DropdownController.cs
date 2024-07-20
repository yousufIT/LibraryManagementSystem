using Library.Domain.Models;
using Library.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryTest.Controllers
{
    public class DropdownController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public DropdownController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetMainCategories()
        {
            var mainCategories = await _categoryRepository.GetMainCategoriesAsync();
            return Json(mainCategories);
        }

        [HttpGet("GetSubCategories")] // تغيير المسار إلى "GetSubCategories"
        public async Task<JsonResult> GetSubCategories(int mainCategoryId)
        {
            var subCategories = await _categoryRepository.GetSubCategoriesAsync(mainCategoryId);
            return Json(subCategories);
        }
    }
}
