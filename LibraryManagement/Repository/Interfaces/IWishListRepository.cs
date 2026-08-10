using FluentResults;
using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IWishListRepository
{
    Task<IEnumerable<WishList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(WishList wishList, CancellationToken cancellationToken = default);
    Task DeleteAsync(WishList wishList, CancellationToken cancellationToken = default);
}