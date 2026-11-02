using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await userService.GetAllUsersAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await userService.GetUserByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(
            [FromBody] CreateUserDto dto,
            CancellationToken cancellationToken)
        {
            var result = await userService.CreateUserAsync(dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await userService.DeleteUserAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}