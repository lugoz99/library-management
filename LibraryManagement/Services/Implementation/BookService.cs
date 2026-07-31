using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class BookService(IBookRepository bookRepository, IMapper mapper) : IBookService
{
    public async Task<Result<IEnumerable<BookDto>>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }

    public async Task<Result<BookDto>> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);
        return book is null ? Result.Fail<BookDto>(new NotFoundError(nameof(Book), id)) : Result.Ok(mapper.Map<BookDto>(book));
    }

    public async Task<Result<BookDto>> CreateBookAsync(CreateBookDto dto, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(dto);
        await bookRepository.AddAsync(book, cancellationToken);
        return Result.Ok(mapper.Map<BookDto>(book)); 
    }

    public async Task<Result> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);
        if (book is null)
        {
            return Result.Fail(new NotFoundError(nameof(Book), id));
        }

        await bookRepository.DeleteAsync(book, cancellationToken);
        return Result.Ok();
    }
}