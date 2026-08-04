using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IBookFormatService
{
    public Task<Result<IEnumerable<BookFormatDto>>> GetAllBookFormatAsync(CancellationToken cancellationToken = default);
    public Task<Result<BookFormatDto>> GetBookFormatByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<BookFormatDto>> CreateBookFormatAsync(CreateBookFormatDto dto, CancellationToken cancellationToken = default);
}