using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomProject.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishDate { get; set; }
        public int Stock { get; set; }
        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
