using Library.Domain.Models;
using Library.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LibraryTest.Controllers
{
    public class MoreServicesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public MoreServicesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var mainCategories = await _categoryRepository.GetMainCategoriesAsync();
            ViewBag.MainCategories = mainCategories;

            return View();
        }

        public async Task<IActionResult> LastQuestion()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.stackexchange.com/2.3/questions?order=desc&sort=creation&site=stackoverflow&pagesize=50");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var questions = JsonConvert.DeserializeObject<StackExchangeResponse>(content);

                return View(questions);
            }

            return View("Error");
        }
        [HttpGet]
        public async Task<JsonResult> GetSubCategories([FromQuery]int mainCategoryId)
        {
            var subCategories = await _categoryRepository.GetSubCategoriesAsync(mainCategoryId);
            return Json(subCategories);
        }
        public async Task<IActionResult> QuestionDetails([FromQuery] int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.stackexchange.com/2.3/questions?order=desc&sort=creation&site=stackoverflow&pagesize=50");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var questionDetails = JsonConvert.DeserializeObject<StackExchangeResponse>(content);
                foreach(var item in questionDetails.Items)
                {
                    if (item.QuestionId==Id)
                    {
                        return View(item);
                    }
                }
            }
            return View("Error");
        }
    }
}
