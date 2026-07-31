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
            TypeAdapterConfig<Category,CategoryDto>
                .NewConfig().Ignore(dest => dest.Id);

            config.NewConfig<Category, CategoryDto>();

            // 3. UpdateCategoryDto a Category
            config.NewConfig<UpdateCategoryDto, Category>()
                .IgnoreNullValues(true)
                .Ignore(c => c.Id);
            
            
            config.NewConfig<Category, CategoryDetailDto>();
        }
    }
}
