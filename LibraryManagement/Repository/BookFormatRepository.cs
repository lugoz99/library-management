using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class BookFormatRepository(ApplicationDbContext context):IBookFormatRepository
{
    public async Task<IEnumerable<BookFormat>> GetAllBookFormatsAsync(CancellationToken cancellationToken = default)
    {
        return await context.BookFormats.ToListAsync(cancellationToken);
    }

    public async Task<BookFormat?> GetBookFormatByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.BookFormats
            .AsNoTracking()
            .FirstOrDefaultAsync(bf => bf.Id == id, cancellationToken);
    }

    public async Task AddBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default)
    {
        await context.BookFormats.AddAsync(bookFormat, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default)
    {
        context.BookFormats.Update(bookFormat);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBookFormatAsync(BookFormat bookFormat, CancellationToken cancellationToken = default)
    {
        context.BookFormats.Remove(bookFormat);
        await context.SaveChangesAsync(cancellationToken);
    }
}