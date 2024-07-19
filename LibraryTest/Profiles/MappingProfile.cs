using AutoMapper;
using Library.Domain.Models;
using LibraryTest.Summaries;

namespace LibraryTest.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookSummary>().ReverseMap();

            // Author mappings
            CreateMap<Author, AuthorSummary>().ReverseMap();

            // Category mappings
            CreateMap<Category, CategorySummary>().ReverseMap();
        }
    }
}
