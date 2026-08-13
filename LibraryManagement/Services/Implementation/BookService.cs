using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using LibraryManagement.Services.Files;
using MapsterMapper;
using NuGet.Packaging;

namespace LibraryManagement.Services.Implementation;

public class BookService(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IMapper mapper,
    IR2StorageService storageService
    ) : IBookService
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

    public async Task<Result<BookCoverResponseDto>> UpdateBookCoverAsync(
        Guid bookId,
        UpdateBookCoverDto dto,
        CancellationToken ct = default)
    {
        // Look up the book; fail early if it doesn't exist
        var book = await bookRepository.GetByIdAsync(bookId, ct);
        if (book is null)
            return Result.Fail<BookCoverResponseDto>(new NotFoundError(nameof(Book), bookId));

        // Keep the old cover's key so we can delete it after the update succeeds
        var oldCoverKey = book.CoverImageKey;

        // Open the uploaded file as a stream to send it to storage
        await using var stream = dto.CoverImage.OpenReadStream();

        // Upload the new cover image to Cloudflare R2
        var uploadResult = await storageService.UploadCoverAsync(
            folder: "books/covers",
            fileName: dto.CoverImage.FileName,
            stream: stream,
            contentType: dto.CoverImage.ContentType,
            cancellationToken: ct);

        // Stop here if the upload failed (validation error or storage error)
        if (uploadResult.IsFailed)
            return Result.Fail<BookCoverResponseDto>(uploadResult.Errors);

        // Save the new cover's url and key on the book
        book.CoverImageUrl = uploadResult.Value.url;
        book.CoverImageKey = uploadResult.Value.key;

        await bookRepository.UpdateAsync(book, ct);

        // Delete the old cover from storage now that the book points to the new one
        if (!string.IsNullOrWhiteSpace(oldCoverKey))
            await storageService.DeleteAsync(oldCoverKey, ct);

        // Return the new cover url so the caller can update the UI without a re-fetch
        return Result.Ok(new BookCoverResponseDto(book.CoverImageUrl));
    }
}