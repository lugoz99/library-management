using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IBookService
{
    Task<Result<IEnumerable<BookDto>>> GetAllBooksAsync(
        CancellationToken cancellationToken = default);

    Task<Result<BookResponseDto>> GetBookByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Result<BookDto>> CreateBookAsync(
        CreateBookDto dto,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteBookAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Result<CreateBookWithAuthorsResponseDto>> CreateBookWithAuthors(
        CreateBookWithAuthorsDto dto,
        CancellationToken token = default);

    Task<Result> AddAuthorToBookAsync(
        Guid bookId,
        AddBookAuthorDto dto,
        CancellationToken ct = default);
    
    
    Task<Result<BookCoverResponseDto>> UpdateBookCoverAsync(
        Guid bookId,
        UpdateBookCoverDto dto,
        CancellationToken ct = default);
}