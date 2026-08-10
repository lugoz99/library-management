using FluentResults;
using LibraryManagement.Common.Errors;
using LibraryManagement.Common.Exceptions;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class WishListService(IWishListRepository wishlistRepository,IMapper mapper):IWishListService
{
    public async Task<Result<IEnumerable<WishListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await wishlistRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<WishListDto>>(items));
    }

    public async Task<Result<WishListDto>> AddAsync(CreateWishListDto wishList, CancellationToken cancellationToken = default)
    {
        var wishlist = mapper.Map<WishList>(wishList);

        try
        {
            await wishlistRepository.AddAsync(wishlist, cancellationToken);
            return Result.Ok(mapper.Map<WishListDto>(wishlist));
        }
        catch (DuplicateEntityException ex)
        {
            return Result.Fail(new DuplicateEntityError(ex.EntityName, ex.FieldName).CausedBy(ex));
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Result.Ok();
    }
}