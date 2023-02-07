using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RandomProject.Dtos.Author;
using RandomProject.Dtos.Publisher;
using RandomProject.Models;
using RandomProject.Services;

namespace RandomProject.Controllers
{
    [ApiController]
    [Route("api/publishers")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public PublisherController(IPublisherService publisherService, IBookService bookService, IMapper mapper)
        {
            _publisherService = publisherService;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPublishers()
        {
            var publishers = await _publisherService.GetAllPublishers();
            var publishersReadDto = _mapper.Map<List<PublisherReadDto>>(publishers);
            return Ok(publishersReadDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPublisherById(int id)
        {
            var publisher = await _publisherService.GetPublisherById(id);
            var publisherReadDto = _mapper.Map<PublisherReadDto>(publisher);
            return Ok(publisherReadDto);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult> GetPublishersBooks(int id)
        {
            var books = await _bookService.GetBooksByPublisherId(id);
            var booksReadDto = _mapper.Map<List<PublisherBookReadDto>>(books);
            return Ok(booksReadDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddPublisher(PublisherCreateUpdateDto cupublisher)
        {
            var publisher = _mapper.Map<Publisher>(cupublisher);
            var entity = await _publisherService.AddPublisher(publisher);
            var publisherReadDto = _mapper.Map<PublisherReadDto>(entity);
            return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisherReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisher(int id, PublisherCreateUpdateDto cupublisher)
        {
            var existingPublisher = await _publisherService.GetPublisherById(id);
            var updatedPublisher = _mapper.Map(cupublisher, existingPublisher);
            await _publisherService.UpdatePublisher(id, updatedPublisher);
            var publisher = await _publisherService.GetPublisherById(id);
            var publisherReadDto = _mapper.Map<PublisherReadDto>(publisher);
            return Ok(publisherReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePublisher(int id)
        {
            await _publisherService.RemovePublisher(id);
            return Ok();
        }
    }
}
