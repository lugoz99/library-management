using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryDto, Category>()
            .Ignore(dest => dest.Id);

        config.NewConfig<Category, CategoryDto>();

        config.NewConfig<UpdateCategoryDto, Category>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id);

        config.NewConfig<Category, CategoryDetailDto>();
        config.NewConfig<Category, CategorySummaryDto>();
    }
}