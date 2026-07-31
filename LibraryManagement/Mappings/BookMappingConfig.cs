using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class BookMappingConfig
{
    public void Register(TypeAdapterConfig config)
    {
        // 1. CreateBookDto to Book
        TypeAdapterConfig<CreateBookDto, Book>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats);

        // 2. Book to BookResponseDto
        config.NewConfig<Book, BookDto>();

        // 3. UpdateBookDto to Book
        TypeAdapterConfig<UpdateBookDto, Book>
            .NewConfig()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats);
    }
}