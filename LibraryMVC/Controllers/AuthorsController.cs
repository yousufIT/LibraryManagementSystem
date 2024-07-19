using AutoMapper;
using Asp.Versioning;
using Library.Domain.Models;
using Library.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryTest.Summaries;

namespace LibraryTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorSummary>>> GetAll()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorSummaries = _mapper.Map<IEnumerable<AuthorSummary>>(authors);
            return Ok(authorSummaries);
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorSummary>> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorSummary = _mapper.Map<AuthorSummary>(author);
            return Ok(authorSummary);
        }

        // POST api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorSummary>> Creat([FromBody] AuthorSummary authorSummary)
        {
            if (authorSummary == null) { return BadRequest(); }
            var author = _mapper.Map<Author>(authorSummary);
            await _authorRepository.AddAsync(author);
            authorSummary.AuthorID = author.AuthorID;
            return Ok(authorSummary);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorSummary authorSummary)
        {
            if (authorSummary == null) { return BadRequest(); }

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            authorSummary.AuthorID = author.AuthorID;
            _mapper.Map(authorSummary, author);
            await _authorRepository.UpdateAsync(author);
            return Ok();
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            await _authorRepository.DeleteAsync(id);
            return Ok(_mapper.Map<AuthorSummary>(author));
        }

        // GET api/authors/book/{bookId}
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<AuthorSummary>>> GetAuthorsByBook(int bookId)
        {
            var authors = await _authorRepository.GetAuthorsByBookAsync(bookId);
            var authorSummaries = _mapper.Map<IEnumerable<AuthorSummary>>(authors);
            return Ok(authorSummaries);
        }

        // GET api/authors/nationality/{nationality}
        [HttpGet("nationality/{nationality}")]
        public async Task<ActionResult<IEnumerable<AuthorSummary>>> GetAuthorsByNationality(string nationality)
        {
            var authors = await _authorRepository.GetAuthorsByNationalityAsync(nationality);
            var authorSummaries = _mapper.Map<IEnumerable<AuthorSummary>>(authors);
            return Ok(authorSummaries);
        }
    }
}
