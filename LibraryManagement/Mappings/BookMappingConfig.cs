using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBookDto, Book>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats);

        config.NewConfig<UpdateBookDto, Book>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats);

        config.NewConfig<CreateBookWithAuthorsDto, Book>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats)
            .Ignore(dest => dest.BookAuthors);

        config.NewConfig<Book, BookDto>();

        config.NewConfig<Book, CreateBookWithAuthorsResponseDto>()
            .Ignore(dest => dest.Authors);

        config.NewConfig<Category, CategorySummaryDto>();
        config.NewConfig<Publisher, PublisherSummaryDto>();

        config.NewConfig<BookAuthors, BookAuthorResponseDto>()
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.FirstName,
                src => src.Author.FirstName)
            .Map(dest => dest.LastNames,
                src => src.Author.LastNames)
            .Map(dest => dest.Role, src => src.Role);

        // Authors != BookAuthors → must map explicitly
        config.NewConfig<Book, BookResponseDto>()
            .Map(dest => dest.Category, src => src.Category)
            .Map(dest => dest.Publisher, src => src.Publisher)
            .Map(dest => dest.Authors,
                src => src.BookAuthors ?? new List<BookAuthors>());
    }
}