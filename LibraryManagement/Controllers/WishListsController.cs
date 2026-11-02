using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishListsController(IWishListService wishListService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishListDto>>> GetAll(CancellationToken cancellationToken) =>
        (await wishListService.GetAllAsync(cancellationToken)).ToActionResult();

    [HttpPost]
    public async Task<ActionResult<WishListDto>> Create(
        [FromBody] CreateWishListDto dto, CancellationToken cancellationToken) =>
        (await wishListService.AddAsync(dto, cancellationToken)).ToActionResult();

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(
        Guid id, [FromBody] AddWishListItemDto dto, CancellationToken cancellationToken) =>
        (await wishListService.AddItemAsync(id, dto, cancellationToken)).ToActionResult();

    [HttpDelete("{id:guid}/items/{bookId:guid}")]
    public async Task<IActionResult> RemoveItem(
        Guid id, Guid bookId, CancellationToken cancellationToken) =>
        (await wishListService.RemoveItemAsync(id, bookId, cancellationToken)).ToActionResult();
}
