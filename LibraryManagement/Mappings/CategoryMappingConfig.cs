using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings
{
    public class CategoryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // 1. CreateCategoryDto a Category
            config.NewConfig<CreateCategoryDto, Category>();

            config.NewConfig<Category, CategoryResponseDto>();

            // 3. UpdateCategoryDto a Category
            config.NewConfig<UpdateCategoryDto, Category>()
                .IgnoreNullValues(true)
                .Ignore(c => c.Id);
        }
    }
}
