using RandomProject.Models;
using RandomProject.Repositories;

namespace RandomProject.Services
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> RemoveAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            await _authorRepository.Remove(author);
            return author;
        }

        public async Task<Author> UpdateAuthor(int id, Author updatedAuthor)
        {
            return await _authorRepository.Update(id, updatedAuthor);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            return await _authorRepository.Add(author);
        }
    }
}
