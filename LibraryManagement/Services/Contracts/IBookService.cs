using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IBookService
{
    public Task<Result<IEnumerable<BookDto>>> GetAllBooksAsync(CancellationToken cancellationToken);
    
    public Task<Result<BookDto>> GetBookByIdAsync(Guid id,CancellationToken cancellationToken = default);
    
    public Task<Result<BookDto>> CreateBookAsync(CreateBookDto dto,CancellationToken cancellationToken = default);
    
    public Task<Result> DeleteBookAsync(Guid id,CancellationToken cancellationToken = default);
}