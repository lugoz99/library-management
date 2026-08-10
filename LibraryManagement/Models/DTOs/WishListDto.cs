namespace LibraryManagement.Models.DTOs;

public record WishListDto(
    int Id,
    string Title,
    string Description
){}

public record CreateWishListDto(
    string Title,
    string Description
);