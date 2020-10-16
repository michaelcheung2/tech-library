using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private const int RESULTS_PER_PAGE = 10;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int currentOffset, string search)
        {
            _logger.LogInformation("Get all books");

            var books = await _bookService.GetBooksAsync();

            books = books.Skip(currentOffset).Take(RESULTS_PER_PAGE).ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                var filteredBooks = books.Where(x => x.Title.ToLower().Contains(search) || (x.ShortDescr?.ToLower().Contains(search) == true));

                books = (!filteredBooks.Count().Equals(0)) ? filteredBooks.ToList() : null;
            }

            var bookResponse = _mapper.Map<List<BookResponse>>(books);

            return Ok(bookResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SaveDescription(int id, [FromBody] BookRequest bookRequest)
        {
            _logger.LogInformation($"Update book description by id {id}");

            var book = await _bookService.SaveBookDescriptionAsync(id, bookRequest.Descr);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }
    }
}
