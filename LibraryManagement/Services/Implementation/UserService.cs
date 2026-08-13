using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class UserService(IUserRepository userRepository,IMapper mapper):IUserService
{
    public async Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var items = await userRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<UserDto>>(items));
    }

    public async Task<Result<UserDto>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return Result.Fail(new NotFoundError(nameof(User), id));
        }
        var userConvert = mapper.Map<UserDto>(user);
        return Result.Ok(userConvert);
    }

    public async Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        // todo: correo unique
       var newUser = mapper.Map<User>(dto);
       await userRepository.Addsync(newUser, cancellationToken);
       return Result.Ok(mapper.Map<UserDto>(newUser));
    }

  

    public async Task<Result> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if(user is null)
        {
            return Result.Fail("User not found");
        }
        await userRepository.DeleteAsync(user, cancellationToken);
        return Result.Ok();
    }
}