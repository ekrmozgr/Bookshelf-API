using RandomProject.Models;
using RandomProject.Pagination;
using RandomProject.Repositories;

namespace RandomProject.Services
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<List<Book>> GetBooksPaginated(PaginationFilter filter)
        {
            return await _bookRepository.GetBooksPaginated(filter);
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetBookById(id);
        }

        public async Task<Book> RemoveBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            await _bookRepository.Remove(book);
            return book;
        }

        public async Task<Book> UpdateBook(int id, Book updatedBook)
        {
            return await _bookRepository.Update(id, updatedBook);
        }

        public async Task<Book> AddBook(Book book)
        {
            return await _bookRepository.AddBook(book);
        }

        public async Task<List<Book>> GetBooksByAuthorId(int id)
        {
            return await _bookRepository.FindListByConditionAsync(x => x.AuthorId == id);
        }

        public async Task<List<Book>> GetBooksByPublisherId(int id)
        {
            return await _bookRepository.FindListByConditionAsync(x => x.PublisherId == id);
        }

        public async Task<List<Book>> GetBooksBySearch(string searchString)
        {
            return await _bookRepository.GetBooksBySearch(searchString);
        }

        public async Task<List<Book>> GetBooksByMinStock(int min)
        {
            return await _bookRepository.GetBooksByMinStock(min);
        }

        public async Task<List<Book>> GetBooksByMaxStock(int max)
        {
            return await _bookRepository.GetBooksByMaxStock(max);
        }

        public async Task<List<Book>> GetBooksByMinMaxStock(int min, int max)
        {
            return await _bookRepository.GetBooksByMinMaxStock(min, max);
        }

        public async Task<List<Book>> GetBooksByStartDate(string start)
        {
            return await _bookRepository.GetBooksByStartDate(start);
        }

        public async Task<List<Book>> GetBooksByEndDate(string end)
        {
            return await _bookRepository.GetBooksByEndDate(end);
        }

        public async Task<List<Book>> GetBooksByStartEndDate(string start, string end)
        {
            return await _bookRepository.GetBooksByStartEndDate(start,end);
        }
    }
}
