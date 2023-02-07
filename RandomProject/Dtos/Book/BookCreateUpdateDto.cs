using RandomProject.Models;

namespace RandomProject.Dtos.Book
{
    public class BookCreateUpdateDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishDate { get; set; }
        public int Stock { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
    }
}
