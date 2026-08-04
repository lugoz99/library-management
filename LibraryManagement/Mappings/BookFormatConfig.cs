using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class BookFormatConfig
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<CreateBookFormatDto, BookFormat>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.KeyUrl!)
            .Ignore(dest => dest.Book!);

        TypeAdapterConfig<UpdateBookFormatDto, BookFormat>
            .NewConfig()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.BookId)
            .Ignore(dest => dest.KeyUrl!)
            .Ignore(dest => dest.Book!);

        TypeAdapterConfig<BookFormat, BookFormatDto>
            .NewConfig();



       
        
      
    }
}