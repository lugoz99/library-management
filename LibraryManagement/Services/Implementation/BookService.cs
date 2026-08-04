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
    public async Task<Result<IEnumerable<BookDto>>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }

    public async Task<Result<BookDto>> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);

        return book is null
            ? Result.Fail<BookDto>(new NotFoundError(nameof(Book), id))
            : Result.Ok(mapper.Map<BookDto>(book));
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
            return Result.Fail(new NotFoundError(nameof(Book), id));

        await bookRepository.DeleteAsync(book, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<BookResponseDto>> CreateBookWithAuthors(
        CreateBookAuthorskDto dto,
        CancellationToken token)
    {
        // If the request has authors, check that all of them exist
        if (dto.Authors is { Count: > 0 })
        {
            // Get unique author ids from the list
            var authorIds = dto.Authors
                .Select(a => a.AuthorId)
                .Distinct()
                .ToList();

            // Count how many of these ids exist in the database
            var existingCount = await authorRepository.CountByIdsAsync(authorIds, token);

            // If some authors are missing, stop and return an error
            if (existingCount != authorIds.Count)
                return Result.Fail(new NotFoundError("Author", "one or more ids"));
        }

        // Map the DTO to a Book entity (authors are not mapped here)
        var book = mapper.Map<Book>(dto);

        // If there are authors, build the join rows and add them to the book
        if (dto.Authors is { Count: > 0 })
        {
            var bookAuthors = dto.Authors.Select(a => new BookAuthors
            {
                AuthorId = a.AuthorId,
                Role = a.Role
                // BookId is set by EF Core when it saves the book
            }).ToList();

            book.BookAuthors.AddRange(bookAuthors);
        }

        // Save the book and its author links in one database call
        await bookRepository.AddAsync(book, token);

        // Return the book as a response DTO
        return Result.Ok(mapper.Map<BookResponseDto>(book));
    }
    public async Task<Result> AddAuthorToBookAsync(
        Guid bookId,
        AddBookAuthorDto dto,
        CancellationToken ct = default)
    {
        // Find the book by id
        var book = await bookRepository.GetByIdAsync(bookId, ct);

        // If the book does not exist, return an error
        if (book is null)
            return Result.Fail(new NotFoundError(nameof(Book), bookId));

        // Find the author by id
        var authorExists = await authorRepository.GetByIdAsync(dto.AuthorId, ct);

        // If the author does not exist, return an error
        if (authorExists is null)
            return Result.Fail(new NotFoundError(nameof(Author), dto.AuthorId));

        // Check if this author is already linked to this book
        // We need this because the database has a unique index for BookId + AuthorId
        var relationExists = await bookRepository.AuthorRelationExistsAsync(bookId, dto.AuthorId, ct);

        // If the relation already exists, stop here and return an error
        // This avoids a database exception later
        if (relationExists)
            return Result.Fail(new ConflictError("This author is already linked to this book"));

        // Create the new relation between the book and the author
        var relation = new BookAuthors
        {
            BookId = book.Id,
            AuthorId = dto.AuthorId,
            Role = dto.Role
        };

        // Add the relation to the book's list of authors
        book.BookAuthors.Add(relation);

        // Save the changes in the database
        await bookRepository.UpdateAsync(book, ct);

        // Everything worked well
        return Result.Ok();
    }
}