using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RandomProject.Dtos.Book;
using RandomProject.Models;
using RandomProject.Pagination;
using RandomProject.Services;

namespace RandomProject.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            var booksReadDto = _mapper.Map<List<BookReadDto>>(books);
            return Ok(booksReadDto);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult> GetAllBooksPaged([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _bookService.GetBooksPaginated(validFilter);
            var booksReadDto = _mapper.Map<List<BookReadDto>>(pagedData);
            var paginatedResponse = new PagedResponse<List<BookReadDto>>(booksReadDto, validFilter.PageNumber, validFilter.PageSize);
            return Ok(paginatedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            var bookReadDto = _mapper.Map<BookReadDto>(book);
            return Ok(bookReadDto);
        }

        [HttpGet("stockFilter")]
        public async Task<ActionResult> FilterBooksByStock([FromQuery]int? min, [FromQuery]int? max, [FromQuery] string? sortOrder)
        {
            List<Book> books;
            if(min.HasValue && max.HasValue)
            {
                books = await _bookService.GetBooksByMinMaxStock((int)min, (int)max);
            }
            else if (min.HasValue)
            {
                books = await _bookService.GetBooksByMinStock((int)min);
            }
            else if(max.HasValue)
            {
                books = await _bookService.GetBooksByMaxStock((int)max);
            }
            else
            {
                books = await _bookService.GetAllBooks();
            }

            if(!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "stock":
                        books = books.OrderBy(x => x.Stock).ToList();
                        break;
                    case "stock_desc":
                        books = books.OrderByDescending(x => x.Stock).ToList();
                        break;
                    default:
                        books = books.OrderBy(x => x.Id).ToList();
                        break;
                }
            }
            var booksReadDto = _mapper.Map<List<BookReadDto>>(books);
            return Ok(booksReadDto);
        }
        
        [HttpGet("dateFilter")]
        public async Task<ActionResult> FilterBooksByDate([FromQuery] string? start, [FromQuery] string? end, [FromQuery] string? sortOrder)
        {
            List<Book> books;
            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                books = await _bookService.GetBooksByStartEndDate((string)start, (string)end);
            }
            else if (!string.IsNullOrEmpty(start))
            {
                books = await _bookService.GetBooksByStartDate(start);
            }
            else if (!string.IsNullOrEmpty(end))
            {
                books = await _bookService.GetBooksByEndDate(end);
            }
            else
            {
                books = await _bookService.GetAllBooks();
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "date":
                        books = books.OrderBy(x => x.PublishDate).ToList();
                        break;
                    case "date_desc":
                        books = books.OrderByDescending(x => x.PublishDate).ToList();
                        break;
                    default:
                        books = books.OrderBy(x => x.Id).ToList();
                        break;
                }
            }

            var booksReadDto = _mapper.Map<List<BookReadDto>>(books);
            return Ok(booksReadDto);
        }
        
        [HttpGet("find")]
        public async Task<ActionResult> GetBooksBySearch([FromQuery]string search)
        {
            var book = await _bookService.GetBooksBySearch(search);
            var bookReadDto = _mapper.Map<List<BookReadDto>>(book);
            return Ok(bookReadDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(BookCreateUpdateDto cubook)
        {
            var book = _mapper.Map<Book>(cubook);
            var entity = await _bookService.AddBook(book);
            var bookReadDto = _mapper.Map<BookReadDto>(entity);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookCreateUpdateDto cubook)
        {
            var existingBook = await _bookService.GetBookById(id);
            var updatedBook = _mapper.Map(cubook, existingBook);
            await _bookService.UpdateBook(id, updatedBook);
            var book = await _bookService.GetBookById(id);
            var bookReadDto = _mapper.Map<BookReadDto>(book);
            return Ok(bookReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveBook(int id)
        {
            await _bookService.RemoveBook(id);
            return Ok();
        }
        
    }
}
