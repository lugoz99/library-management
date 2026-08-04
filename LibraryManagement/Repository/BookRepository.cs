using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class BookRepository(ApplicationDbContext context):IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Books.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    { 
        return await context.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task AddRangeAsync(IEnumerable<BookAuthors> booksAuthors, CancellationToken cancellationToken = default)
    {
        await context.BookAuthors.AddRangeAsync(booksAuthors, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

  
    
    public async Task DeleteAsync(Book book, CancellationToken cancellationToken = default)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> AuthorRelationExistsAsync(
        Guid bookId, Guid authorId, CancellationToken ct = default)
    {
        return await context.BookAuthors
            .AnyAsync(x => x.BookId == bookId && x.AuthorId == authorId, ct);
    }
}