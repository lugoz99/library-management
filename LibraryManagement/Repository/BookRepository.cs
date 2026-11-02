using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class BookRepository(ApplicationDbContext context) : IBookRepository
{
    // Light list: no includes (maps to BookDto)
    public async Task<IEnumerable<Book>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Books
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    // Detail: category, publisher, authors (maps to BookResponseDto)
    public async Task<Book?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await context.Books
            .AsNoTracking()
            .Include(b => b.Category)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Book book,
        CancellationToken cancellationToken = default)
    {
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Book book,
        CancellationToken cancellationToken = default)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(
        IEnumerable<BookAuthors> booksAuthors,
        CancellationToken cancellationToken = default)
    {
        await context.BookAuthors.AddRangeAsync(booksAuthors, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Book book,
        CancellationToken cancellationToken = default)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AuthorRelationExistsAsync(
        Guid bookId,
        Guid authorId,
        CancellationToken ct = default)
    {
        return await context.BookAuthors
            .AnyAsync(
                x => x.BookId == bookId && x.AuthorId == authorId,
                ct);
    }
}