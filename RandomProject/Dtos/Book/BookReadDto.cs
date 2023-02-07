using RandomProject.Models;

namespace RandomProject.Dtos.Book
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishDate { get; set; }
        public int Stock { get; set; }
        public BookPublisherReadDto? Publisher { get; set; }
        public BookAuthorReadDto? Author { get; set; }
    }
}
