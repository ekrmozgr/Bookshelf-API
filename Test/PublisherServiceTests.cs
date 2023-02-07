using Moq;
using RandomProject.Models;
using RandomProject.Repositories;
using RandomProject.Services;
using System.Reflection;

namespace Test
{
    public class PublisherServiceTests
    {
        private readonly PublisherManager _sut;
        private readonly Mock<IPublisherRepository> _publisherRepoMock = new Mock<IPublisherRepository>();
        public PublisherServiceTests()
        {
            _sut = new PublisherManager(_publisherRepoMock.Object);
        }


        [Fact]
        public async Task GetPublisherById_ShouldReturnPublisher_WhenPublisherExists()
        {
            // Arrange
            int publisherId = 1;
            string publisherName = "John";
            List<Book> publisherBooks = new List<Book>();
            var publisherEntity = new Publisher
            {
                Id = publisherId,
                Name = publisherName,
                Books = publisherBooks
            };
            _publisherRepoMock.Setup(x=> x.GetByIdAsync(publisherId)).ReturnsAsync(publisherEntity);

            // Act
            var publisher = await _sut.GetPublisherById(publisherId);

            // Assert
            Assert.Equal(publisherId, publisher.Id);
            Assert.Equal(publisherName, publisher.Name);
            Assert.Equal(publisherBooks, publisher.Books);
        }

        [Fact]
        public async Task GetPublisherById_ShouldReturnNothing_WhenPublisherDoesNotExists()
        {
            // Arrange
            _publisherRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            
            // Act
            var publisher = await _sut.GetPublisherById(new Random().Next());

            // Assert
            Assert.Null(publisher);
        }

        [Fact]
        public async Task GetAllPublishers_ShouldReturnAllPublisher()
        {
            // Arrange
            List<Publisher> publisherList = new List<Publisher>();
            publisherList.Add(new Publisher { Id = 1, Name = "John", Books = new List<Book>() });
            publisherList.Add(new Publisher { Id = 2, Name = "Kelly", Books = new List<Book>() });
            _publisherRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publisherList);

            // Act
            var publishers = await _sut.GetAllPublishers();

            // Assert
            Assert.Equal(publisherList, publishers);
        }
    }
}