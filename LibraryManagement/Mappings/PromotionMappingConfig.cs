using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class PromotionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePromotionDto, Promotion>()
            .Ignore(promotion => promotion.Id)
            .Ignore(promotion => promotion.Book)
            .Ignore(promotion => promotion.BookId);

        config.NewConfig<Promotion, PromotionDto>()
            .Map(dto => dto.IsCurrentlyActive,
                promotion => promotion.IsActive &&
                             promotion.StartsAt <= DateTime.UtcNow &&
                             DateTime.UtcNow < promotion.EndsAt);
    }
}
