using Microsoft.EntityFrameworkCore;
using RandomProject.Data;
using RandomProject.Models;
using RandomProject.Pagination;

namespace RandomProject.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public async Task<List<Book>> GetAllBooks()
        {
            return await _entities.Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksPaginated(PaginationFilter filter)
        {
            var pagedBooks = await _entities.Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
            return pagedBooks;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _entities.Include(x => x.Author).Include(x => x.Publisher).FirstAsync(x => x.Id == id);
        }

        public async Task<Book> AddBook(Book book)
        {
            await _entities.AddAsync(book);
            await _context.SaveChangesAsync();
            await _context.Entry(book).Reference(x => x.Author).LoadAsync();
            await _context.Entry(book).Reference(x => x.Publisher).LoadAsync();
            return book;
        }

        public async Task<List<Book>> GetBooksBySearch(string searchString)
        {
            int i = 0;
            bool isOnlyNumeric = int.TryParse(searchString, out i);
            if(isOnlyNumeric)
            {
                return await _entities.Where(x => x.Pages == int.Parse(searchString) ||
                                            x.Stock == int.Parse(searchString))
                                            .Include(x => x.Author)
                                            .Include(x=>x.Publisher).ToListAsync();
            }
            return await _entities.Where(x => x.Title.Contains(searchString) ||
                                x.ISBN.Contains(searchString) ||
                                x.PublishDate.ToString().Contains(searchString))
                                .Include(x => x.Author)
                                .Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByMinStock(int min)
        {
            return await _entities.Where(x => x.Stock >= min).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByMaxStock(int max)
        {
            return await _entities.Where(x => x.Stock <= max).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByMinMaxStock(int min, int max)
        {
            return await _entities.Where(x => x.Stock >= min && x.Stock <= max).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByStartDate(string start)
        {
            DateTime _start = DateTime.Parse(start);
            return await _entities.Where(x => x.PublishDate >= _start).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByEndDate(string end)
        {
            DateTime _end = DateTime.Parse(end);
            return await _entities.Where(x => x.PublishDate <= _end).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByStartEndDate(string start, string end)
        {
            DateTime _start = DateTime.Parse(start);
            DateTime _end = DateTime.Parse(end);
            return await _entities.Where(x => x.PublishDate >= _start && x.PublishDate <= _end).Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
        }
    }
}
