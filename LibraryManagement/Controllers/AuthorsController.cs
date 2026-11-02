using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await authorService.GetAllAuthorsAsync(cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuthorDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await authorService.GetAuthorByIdAsync(id, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create(
        [FromBody] CreateAuthorDto dto,
        CancellationToken cancellationToken)
    {
        var result = await authorService.CreateAuthorAsync(dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AuthorDto>> Update(
        Guid id,
        [FromBody] UpdateAuthorDto dto,
        CancellationToken cancellationToken)
    {
        var result = await authorService.UpdateAuthorAsync(id, dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await authorService.DeleteAuthorAsync(id, cancellationToken);
        return result.ToActionResult();
    }
}