namespace RandomProject.Dtos.Author
{
    public class AuthorBookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishDate { get; set; }
        public int Stock { get; set; }
    }
}
