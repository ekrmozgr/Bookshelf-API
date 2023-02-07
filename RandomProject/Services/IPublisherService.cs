using RandomProject.Models;

namespace RandomProject.Services
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAllPublishers();
        Task<Publisher> GetPublisherById(int id);
        Task<Publisher> RemovePublisher(int id);
        Task<Publisher> UpdatePublisher(int id, Publisher publisher);
        Task<Publisher> AddPublisher(Publisher publisher);
    }
}
