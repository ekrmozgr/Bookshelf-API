using RandomProject.Models;

namespace RandomProject.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> RemoveAuthor(int id);
        Task<Author> UpdateAuthor(int id, Author author);
        Task<Author> AddAuthor(Author author);
    }
}
