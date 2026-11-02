using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class BookFormatService(IBookFormatRepository bookFormatRepository, IMapper mapper) : IBookFormatService
{
    public async Task<Result<IEnumerable<BookFormatDto>>> GetAllBookFormatAsync(CancellationToken cancellationToken = default)
    {
        var items = await bookFormatRepository.GetAllAsync(cancellationToken);
        var dtos = items.Select(mapper.Map<BookFormatDto>);
        return Result.Ok(dtos);
    }

    public async Task<Result<BookFormatDto>> GetBookFormatByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var bookFormat = await bookFormatRepository.GetByIdAsync(id, cancellationToken);
        return bookFormat is null
            ? Result.Fail<BookFormatDto>(new NotFoundError(nameof(BookFormatDto), id))
            : Result.Ok(mapper.Map<BookFormatDto>(bookFormat));
    }

    public async Task<Result<BookFormatDto>> CreateBookFormatAsync(CreateBookFormatDto dto, CancellationToken cancellationToken = default)
    {
        var bookFormat = mapper.Map<BookFormat>(dto);
        await bookFormatRepository.AddAsync(bookFormat, cancellationToken);
        return Result.Ok(mapper.Map<BookFormatDto>(bookFormat));
    }
}