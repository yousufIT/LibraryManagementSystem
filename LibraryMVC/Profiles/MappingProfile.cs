using AutoMapper;
using Library.Domain.Models;
using LibraryMVC.Summaries;

namespace LibraryTest.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Book, BookSummary>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.BookAuthors.Any() ? src.BookAuthors.First().AuthorID : (int?)null))
            .ReverseMap()
            .ForMember(dest => dest.BookAuthors, opt => opt.MapFrom(src => src.AuthorId.HasValue ? new List<BookAuthor> { new BookAuthor { AuthorID = src.AuthorId.Value, BookID = src.BookID } } : new List<BookAuthor>()));

            CreateMap<Author, AuthorSummary>()
            .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookAuthors.Any() ? src.BookAuthors.First().BookID : (int?)null))
            .ReverseMap()
            .ForMember(dest => dest.BookAuthors, opt => opt.MapFrom(src => src.BookID.HasValue ? new List<BookAuthor> { new BookAuthor { BookID = src.BookID.Value, AuthorID = src.AuthorID } } : new List<BookAuthor>()));

            CreateMap<Category, CategorySummary>().ReverseMap();
        }
    }
}
