using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserDto, User>()
            .Ignore(dest => dest.Id);

        config.NewConfig<User, UserDto>();

        config.NewConfig<UpdateUserDto, User>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id);
    }
}