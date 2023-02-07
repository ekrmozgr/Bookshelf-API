using RandomProject.Data;
using RandomProject.Models;

namespace RandomProject.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
