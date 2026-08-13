using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;
using NuGet.Packaging;

namespace LibraryManagement.Services.Implementation;

public class BookService(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IMapper mapper) : IBookService
{
    public async Task<Result<IEnumerable<BookDto>>> GetAllBooksAsync(
        CancellationToken cancellationToken = default)
    {
        var books = await bookRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }

    public async Task<Result<BookResponseDto>> GetBookByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);

        return book is null
            ? Result.Fail<BookResponseDto>(new NotFoundError(nameof(Book), id))
            : Result.Ok(mapper.Map<BookResponseDto>(book));
    }

    public async Task<Result<BookDto>> CreateBookAsync(
        CreateBookDto dto,
        CancellationToken cancellationToken = default)
    {
        var book = mapper.Map<Book>(dto);
        await bookRepository.AddAsync(book, cancellationToken);
        return Result.Ok(mapper.Map<BookDto>(book));
    }

    public async Task<Result> DeleteBookAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);

        if (book is null)
            return Result.Fail(new NotFoundError(nameof(Book), id));

        await bookRepository.DeleteAsync(book, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<CreateBookWithAuthorsResponseDto>> CreateBookWithAuthors(
        CreateBookWithAuthorsDto dto,
        CancellationToken token = default)
    {
        // TODO: ISBS ES UNIQUE - MISSING VALIDATION 
        // If authors were sent, check that all exist
        if (dto.Authors is { Count: > 0 })
        {
            var authorIds = dto.Authors
                .Select(a => a.AuthorId)
                .Distinct()
                .ToList();

            var existingCount = await authorRepository.CountByIdsAsync(authorIds, token);

            if (existingCount != authorIds.Count)
                return Result.Fail(new NotFoundError("Author", "one or more ids"));
        }

        // Map book only (authors ignored in mapping)
        var book = mapper.Map<Book>(dto);

        // Attach author relations in memory
        if (dto.Authors is { Count: > 0 })
        {
            var bookAuthors = dto.Authors.Select(a => new BookAuthors
            {
                AuthorId = a.AuthorId,
                Role = a.Role
            }).ToList();

            book.BookAuthors.AddRange(bookAuthors);
        }

        // Save book + relations
        await bookRepository.AddAsync(book, token);

        // Response with ids + same authors from request (no Category/Publisher objects)
        var response = mapper.Map<CreateBookWithAuthorsResponseDto>(book) with
        {
            Authors = dto.Authors ?? []
        };

        return Result.Ok(response);
    }

    public async Task<Result> AddAuthorToBookAsync(
        Guid bookId,
        AddBookAuthorDto dto,
        CancellationToken ct = default)
    {
        var book = await bookRepository.GetByIdAsync(bookId, ct);
        if (book is null)
            return Result.Fail(new NotFoundError(nameof(Book), bookId));

        var author = await authorRepository.GetByIdAsync(dto.AuthorId, ct);
        if (author is null)
            return Result.Fail(new NotFoundError(nameof(Author), dto.AuthorId));

        if (await bookRepository.AuthorRelationExistsAsync(bookId, dto.AuthorId, ct))
            return Result.Fail(new ConflictError("This author is already linked to this book"));

        var relation = new BookAuthors
        {
            BookId = book.Id,
            AuthorId = dto.AuthorId,
            Role = dto.Role
        };

        // Prefer a dedicated repo method if GetById uses AsNoTracking
        await bookRepository.AddRangeAsync([relation], ct);

        return Result.Ok();
    }
}