using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class AuthorMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAuthorDto, Author>()
            .Ignore(dest => dest.Id);

        config.NewConfig<Author, AuthorDto>()
            .Map(dest => dest.DateOfBirth,
                src => src.DateOfBirth.HasValue
                    ? src.DateOfBirth.Value.ToString("dd/MM/yyyy")
                    : null)
            .Map(dest => dest.DateOfDeath,
                src => src.DateOfDeath.HasValue
                    ? src.DateOfDeath.Value.ToString("dd/MM/yyyy")
                    : null);

        config.NewConfig<UpdateAuthorDto, Author>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id);
    }
}