using Microsoft.EntityFrameworkCore;
using RandomProject.Data;
using RandomProject.Models;

namespace RandomProject.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
