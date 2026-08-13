using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class BookFormatConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBookFormatDto, BookFormat>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.KeyUrl!)
            .Ignore(dest => dest.Book!);

        config.NewConfig<UpdateBookFormatDto, BookFormat>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.BookId)
            .Ignore(dest => dest.KeyUrl!)
            .Ignore(dest => dest.Book!);

        config.NewConfig<BookFormat, BookFormatDto>();
    }
}