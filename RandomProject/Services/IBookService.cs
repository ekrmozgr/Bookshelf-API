using RandomProject.Models;
using RandomProject.Pagination;

namespace RandomProject.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooksPaginated(PaginationFilter filter);
        Task<Book> GetBookById(int id);
        Task<Book> RemoveBook(int id);
        Task<Book> UpdateBook(int id, Book book);
        Task<Book> AddBook(Book book);
        Task<List<Book>> GetBooksByAuthorId(int id);
        Task<List<Book>> GetBooksByPublisherId(int id);
        Task<List<Book>> GetBooksBySearch(string searchString);
        Task<List<Book>> GetBooksByMinStock(int min);
        Task<List<Book>> GetBooksByMaxStock(int max);
        Task<List<Book>> GetBooksByMinMaxStock(int min, int max);
        Task<List<Book>> GetBooksByStartEndDate(string start, string end);
        Task<List<Book>> GetBooksByStartDate(string start);
        Task<List<Book>> GetBooksByEndDate(string end);
    }
}
