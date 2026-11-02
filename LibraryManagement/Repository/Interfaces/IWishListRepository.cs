using FluentResults;
using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interfaces;

public interface IWishListRepository
{
    Task<IEnumerable<WishList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WishList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> BookExistsAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task AddAsync(WishList wishList, CancellationToken cancellationToken = default);
    Task AddItemAsync(WishListItem item, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(WishListItem item, CancellationToken cancellationToken = default);
    Task<WishListItem?> GetItemAsync(Guid wishListId, Guid bookId, CancellationToken cancellationToken = default);
    Task DeleteAsync(WishList wishList, CancellationToken cancellationToken = default);
}