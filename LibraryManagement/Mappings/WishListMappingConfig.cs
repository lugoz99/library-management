using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class WishListMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateWishListDto, WishList>()
            .Ignore(wishlist => wishlist.Id)
            .Ignore(wishlist => wishlist.User!)
            .Ignore(wishlist => wishlist.Items);

        config.NewConfig<WishList, WishListDto>()
            .Map(dto => dto.Books, wishlist => wishlist.Items
                .Where(item => true)
                .Select(item => new WishListBookDto(item.BookId, item.Book.Title)));
    }
}
