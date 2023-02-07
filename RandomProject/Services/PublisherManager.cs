using RandomProject.Models;
using RandomProject.Repositories;

namespace RandomProject.Services
{
    public class PublisherManager : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherManager(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<List<Publisher>> GetAllPublishers()
        {
            return await _publisherRepository.GetAllAsync();
        }

        public async Task<Publisher> GetPublisherById(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null)
                return null;
            return publisher;
        }

        public async Task<Publisher> RemovePublisher(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            await _publisherRepository.Remove(publisher);
            return publisher;
        }

        public async Task<Publisher> UpdatePublisher(int id, Publisher updatedPublisher)
        {
            return await _publisherRepository.Update(id, updatedPublisher);
        }

        public async Task<Publisher> AddPublisher(Publisher publisher)
        {
            return await _publisherRepository.Add(publisher);
        }
    }
}
