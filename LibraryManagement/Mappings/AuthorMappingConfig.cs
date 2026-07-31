using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class AuthorMappingConfig
{
    public void Register(TypeAdapterConfig typeAdapterConfig)
    {
        TypeAdapterConfig<CreateAuthorDto,Author>
            .NewConfig().Ignore(dest => dest.Id);
        
        typeAdapterConfig.NewConfig<Author, AuthorDto>()
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth.HasValue 
                ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") 
                : null)
            .Map(dest => dest.DateOfDeath, src => src.DateOfDeath.HasValue 
                ? src.DateOfDeath.Value.ToString("dd/MM/yyyy") 
                : null);
        typeAdapterConfig.NewConfig<UpdateAuthorDto, Author>()
            .IgnoreNullValues(true)
            .Ignore(c => c.Id);
    }
    
    
}

// Le dices exactamente qué atributo del objeto anidado quieres extraer
//.Map(dest => dest.Nacionalidad, src => src.Pais.Nombre);