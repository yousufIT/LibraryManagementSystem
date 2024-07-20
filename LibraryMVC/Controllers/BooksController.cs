using AutoMapper;
using Asp.Versioning;
using Library.Domain.Models;
using Library.infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryMVC.Summaries;

namespace LibraryMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;

        public BooksController(IBookRepository bookRepository, 
            IMapper mapper,ICategoryRepository categoryRepository,
            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSummary>>> GetAll()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookSummaries = _mapper.Map<IEnumerable<BookSummary>>(books);
            return Ok(bookSummaries);
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookSummary>> GetById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookSummary = _mapper.Map<BookSummary>(book);
            return Ok(bookSummary);
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult<BookSummary>> Creat([FromBody] BookSummary bookSummary)
        {

            if (bookSummary == null)
            {
                return NotFound("this book not found.");
            }

            // Map BookSummary to Book
            var book = _mapper.Map<Book>(bookSummary);
            book.MainCategory = await _categoryRepository.GetByIdAsync(bookSummary.MainCategoryID);
            book.SubCategory = await _categoryRepository.GetByIdAsync(bookSummary.SubCategoryID);
            var author=await _authorRepository.GetByIdAsync(bookSummary.AuthorId.Value);
            if(author == null) { return NotFound("this author doesn't exist"); }
            // Add the book to the repository
            await _bookRepository.AddAsync(book);
            BookAuthor bookAuthor = new BookAuthor()
            {
                BookID = book.BookID,
                Book = book,
                AuthorID = author.AuthorID,
                Author = author
            };
            book.MainCategory.Books.Add(book);
            book.SubCategory.Books.Add(book);
            book.BookAuthors.Add(bookAuthor);
            await _bookRepository.UpdateAsync(book);
            // Map the book back to BookSummary for response
            bookSummary.BookID = book.BookID;

            return Ok(bookSummary);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookSummary bookSummary)
        {
            if (bookSummary == null)
            {
                return BadRequest();
            }

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            bookSummary.BookID = book.BookID;
            // Map from BookSummary to Book
            _mapper.Map(bookSummary, book);
            await _bookRepository.UpdateAsync(book);
            return NoContent();
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookSummary>> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(id);
            return Ok(_mapper.Map<BookSummary>(book));
        }

        // GET api/books/category/5/main
        [HttpGet("category/{categoryId}/main")]
        public async Task<ActionResult<IEnumerable<BookSummary>>> GetBooksByMainCategory(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId, true);
            var bookSummaries = _mapper.Map<IEnumerable<BookSummary>>(books);
            return Ok(bookSummaries);
        }

        // GET api/books/category/5/sub
        [HttpGet("category/{categoryId}/sub")]
        public async Task<ActionResult<IEnumerable<BookSummary>>> GetBooksBySubCategory(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId, false);
            var bookSummaries = _mapper.Map<IEnumerable<BookSummary>>(books);
            return Ok(bookSummaries);
        }

        // GET api/books/author/5
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<BookSummary>>> GetBooksByAuthor(int authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
            var bookSummaries = _mapper.Map<IEnumerable<BookSummary>>(books);
            return Ok(bookSummaries);
        }
    }
}
