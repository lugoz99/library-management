using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IAuthorService
{
    Task<Result<IEnumerable<AuthorDto>>> GetAllAuthorsAsync(CancellationToken cancellationToken);
    
    Task<Result<AuthorDto>> GetAuthorByIdAsync(Guid id,CancellationToken cancellationToken);
    
    Task<Result<AuthorDto>> CreateAuthorAsync(CreateAuthorDto dto,CancellationToken cancellationToken);

    Task<Result<AuthorDto>> UpdateAuthorAsync(Guid id, UpdateAuthorDto dto, CancellationToken  cancellationToken);

    Task<Result> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);
}