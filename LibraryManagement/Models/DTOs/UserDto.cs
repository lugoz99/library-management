// ReSharper disable ClassNeverInstantiated.Global
namespace LibraryManagement.Models.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string Name);

public record CreateUserDto(
    string Email,
    string Name
    );


public record UpdateUserDto(
    Guid Id,
    string? Email,
    string? Name
    );
    
    