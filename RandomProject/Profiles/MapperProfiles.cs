using AutoMapper;
using RandomProject.Dtos.Author;
using RandomProject.Dtos.Book;
using RandomProject.Dtos.Publisher;
using RandomProject.Models;

namespace RandomProject.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<BookCreateUpdateDto, Book>();
            CreateMap<Publisher, BookPublisherReadDto>();
            CreateMap<Author, BookAuthorReadDto>();
            CreateMap<Book, BookReadDto>();

            CreateMap<AuthorCreateUpdateDto, Author>();
            CreateMap<Book, BookAuthorReadDto>();
            CreateMap<Book, AuthorBookReadDto>();
            CreateMap<Author, AuthorReadDto>();

            CreateMap<PublisherCreateUpdateDto, Publisher>();
            CreateMap<Book, BookPublisherReadDto>();
            CreateMap<Book, PublisherBookReadDto>();
            CreateMap<Publisher, PublisherReadDto>();
        }
    }
}
