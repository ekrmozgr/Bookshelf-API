using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RandomProject.Dtos.Author;
using RandomProject.Dtos.Book;
using RandomProject.Models;
using RandomProject.Services;

namespace RandomProject.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService authorService, IBookService bookService, IMapper mapper)
        {
            _authorService = authorService;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            var authorsReadDto = _mapper.Map<List<AuthorReadDto>>(authors);
            return Ok(authorsReadDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            var authorReadDto = _mapper.Map<AuthorReadDto>(author);
            return Ok(authorReadDto);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult> GetAuthorsBooks(int id)
        {
            var books = await _bookService.GetBooksByAuthorId(id);
            var booksReadDto = _mapper.Map<List<AuthorBookReadDto>>(books);
            return Ok(booksReadDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor(AuthorCreateUpdateDto cuauthor)
        {
            var author = _mapper.Map<Author>(cuauthor);
            var entity = await _authorService.AddAuthor(author);
            var authorReadDto = _mapper.Map<AuthorReadDto>(entity);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, authorReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorCreateUpdateDto cuauthor)
        {
            var existingAuthor = await _authorService.GetAuthorById(id);
            var updatedAuthor = _mapper.Map(cuauthor, existingAuthor);
            await _authorService.UpdateAuthor(id, updatedAuthor);
            var author = await _authorService.GetAuthorById(id);
            var authorReadDto = _mapper.Map<AuthorReadDto>(author);
            return Ok(authorReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAuthor(int id)
        {
            await _authorService.RemoveAuthor(id);
            return Ok();
        }
    }
}
