using AutoMapper;
using Asp.Versioning;
using Library.Domain.Models;
using Library.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryMVC.Summaries;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorySummary>>> GetMainCategories()
        {
            var mainCategories = await _categoryRepository.GetMainCategoriesAsync();
            var categorySummaries = _mapper.Map<IEnumerable<CategorySummary>>(mainCategories);
            return Ok(categorySummaries);
        }

        // GET api/categories/{mainCategoryId}/subcategories
        [HttpGet("{mainCategoryId}/subcategories")]
        public async Task<ActionResult<IEnumerable<CategorySummary>>> GetSubCategories(int mainCategoryId)
        {
            var subCategories = await _categoryRepository.GetSubCategoriesAsync(mainCategoryId);
            var categorySummaries = _mapper.Map<IEnumerable<CategorySummary>>(subCategories);
            return Ok(categorySummaries);
        }

        // GET api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategorySummary>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categorySummary = _mapper.Map<CategorySummary>(category);
            return Ok(categorySummary);
        }

        // POST api/categories
        [HttpPost("{ParentId}")]
        public async Task<ActionResult<CategorySummary>> Create(int? ParentId,[FromBody] CategorySummary categorySummary)
        {
            if (categorySummary == null) { return NotFound("this cahegory not found"); }
            var category = _mapper.Map<Category>(categorySummary);
            if (ParentId != null)
            { 
                var parent = await _categoryRepository.GetByIdAsync(ParentId.Value);
                if (parent == null) { return NotFound("this maincategory dosn't exist"); }
                category.ParentCategory = parent;
                category.ParentCategoryID = ParentId;

                await _categoryRepository.AddAsync(category);
                parent.SubCategories.Add(category);
                await _categoryRepository.UpdateAsync(parent);
            }
            else
            {
                await _categoryRepository.AddAsync(category);
            }
                categorySummary.CategoryID = category.CategoryID;
            return Ok(categorySummary);
        }

        // PUT api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategorySummary categorySummary)
        {
            if (categorySummary == null)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            categorySummary.CategoryID = category.CategoryID;
            _mapper.Map(categorySummary, category);
            await _categoryRepository.UpdateAsync(category);
            return Ok();
        }


        // DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategorySummary>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);
            return Ok(_mapper.Map<CategorySummary>(category));
        }

     
    }
}
