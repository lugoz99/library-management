using LibraryManagement.Models.DTOs;
using FluentResults;

namespace LibraryManagement.Services.Contracts;

public interface IWishListService
{
    Task<Result<IEnumerable<WishListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<WishListDto>>AddAsync(CreateWishListDto wishList, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}