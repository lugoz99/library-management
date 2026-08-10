using FluentResults;
using LibraryManagement.Common.Errors;
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
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

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


    public async Task DeleteAsync(WishList wishList, CancellationToken cancellationToken = default)
    {
        context.WishLists.Remove(wishList);
        await context.SaveChangesAsync(cancellationToken);
    }
}