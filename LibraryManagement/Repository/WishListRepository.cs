using LibraryManagement.Common.Exceptions;
using LibraryManagement.Data;
using LibraryManagement.Data.Exceptions;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class WishListRepository(ApplicationDbContext context) : IWishListRepository
{
    public async Task<IEnumerable<WishList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.WishLists
            .Include(w => w.Items)
            .ThenInclude(item => item.Book)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<WishList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.WishLists
            .Include(w => w.Items)
            .ThenInclude(item => item.Book)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

    public Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default) =>
        context.Users.AnyAsync(u => u.Id == userId, cancellationToken);

    public Task<bool> BookExistsAsync(Guid bookId, CancellationToken cancellationToken = default) =>
        context.Books.AnyAsync(b => b.Id == bookId, cancellationToken);

    public async Task AddAsync(WishList wishList, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.WishLists.AddAsync(wishList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
        {
            throw new DuplicateEntityException("Wishlist", "Title");
        }
    }

    public async Task AddItemAsync(WishListItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Set<WishListItem>().AddAsync(item, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation())
        {
            throw new DuplicateEntityException("Wishlist item", "BookId");
        }
    }

    public async Task RemoveItemAsync(WishListItem item, CancellationToken cancellationToken = default)
    {
        context.Set<WishListItem>().Remove(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<WishListItem?> GetItemAsync(Guid wishListId, Guid bookId, CancellationToken cancellationToken = default) =>
        context.Set<WishListItem>().FirstOrDefaultAsync(
            item => item.WishListId == wishListId && item.BookId == bookId, cancellationToken);


    public async Task DeleteAsync(WishList wishList, CancellationToken cancellationToken = default)
    {
        context.WishLists.Remove(wishList);
        await context.SaveChangesAsync(cancellationToken);
    }
}