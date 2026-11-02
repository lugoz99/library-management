using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IUserService
{
    Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<Result<UserDto>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}