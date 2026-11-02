using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class AuthorService(IAuthorRepository authorRepository, IMapper mapper) : IAuthorService
{
    public async Task<Result<IEnumerable<AuthorDto>>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        var authors = await authorRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<AuthorDto>>(authors));
    }

    public async Task<Result<AuthorDto>> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(id, cancellationToken);
        if (author is null)
        {
            return Result.Fail<AuthorDto>(new NotFoundError(nameof(Author), id));
        }

        return Result.Ok(mapper.Map<AuthorDto>(author));
    }

    public async Task<Result<AuthorDto>> CreateAuthorAsync(CreateAuthorDto dto, CancellationToken cancellationToken)
    {
        var author = mapper.Map<Author>(dto);
        await authorRepository.AddAsync(author, cancellationToken);
        return Result.Ok(mapper.Map<AuthorDto>(author));
    }

    public async Task<Result<AuthorDto>> UpdateAuthorAsync(Guid id, UpdateAuthorDto dto, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(id, cancellationToken);
        if (author is null)
        {
            return Result.Fail<AuthorDto>(new NotFoundError(nameof(Author), id));
        }

        mapper.Map(dto, author);
        await authorRepository.UpdateAsync(author, cancellationToken);
        return Result.Ok(mapper.Map<AuthorDto>(author));
    }

    public async Task<Result> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(id, cancellationToken);
        if (author is null)
        {
            return Result.Fail(new NotFoundError(nameof(Author), id));
        }

        await authorRepository.DeleteAsync(author, cancellationToken);
        return Result.Ok();
    }
}