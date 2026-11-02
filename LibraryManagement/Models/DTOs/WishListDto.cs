// ReSharper disable ClassNeverInstantiated.Global
namespace LibraryManagement.Models.DTOs;

public record WishListBookDto(
    Guid Id,
    string Name
);

public record WishListDto(
    Guid Id,
    Guid UserId,
    string Title,
    string? Description,
    IReadOnlyCollection<WishListBookDto> Books
);

public record CreateWishListDto(
    Guid UserId,
    string Title,
    string? Description
);

public record AddWishListItemDto(Guid BookId);