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
            .Ignore(dest => dest.BooksFormats
            );

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
        
        
        
        
        //BookAuthors -> BookAuthorResponseDto
        config.NewConfig<BookAuthors, BookAuthorResponseDto>()
            .Map(
                dest => dest.AuthorId,
                src => src.Author.Id
            )
            .Map(
                dest => dest.FirstName,
                src => src.Author.FirstName
            )
            .Map(
                dest => dest.LastNames,
                src => src.Author.LastNames
            )
            .Map(
                dest => dest.Role,
                src => src.Role
            );


        // Book -> BookResponseDto
        config.NewConfig<Book, BookResponseDto>()
            .Map(
                dest => dest.Authors,
                src => src.BookAuthors
            );
        
        
        config.NewConfig<CreateBookAuthorskDto, Book>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CoverImageUrl!)
            .Ignore(dest => dest.CoverImageKey!)
            .Ignore(dest => dest.Category!)
            .Ignore(dest => dest.Publisher!)
            .Ignore(dest => dest.BooksFormats)
            .Ignore(dest => dest.BookAuthors);
    }
}