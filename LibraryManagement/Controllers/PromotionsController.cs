using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api")]
[ApiController]
public class PromotionsController(IPromotionService promotionService) : ControllerBase
{
    [HttpGet("Books/{bookId:guid}/promotions")]
    public async Task<ActionResult<IEnumerable<PromotionDto>>> GetByBook(
        Guid bookId, CancellationToken cancellationToken) =>
        (await promotionService.GetByBookIdAsync(bookId, cancellationToken)).ToActionResult();

    [HttpPost("Books/{bookId:guid}/promotions")]
    public async Task<ActionResult<PromotionDto>> Create(
        Guid bookId,
        [FromBody] CreatePromotionDto dto,
        CancellationToken cancellationToken) =>
        (await promotionService.CreateAsync(bookId, dto, cancellationToken)).ToActionResult();

    [HttpDelete("Promotions/{id:guid}")]
    public async Task<IActionResult> Deactivate(
        Guid id, CancellationToken cancellationToken) =>
        (await promotionService.DeactivateAsync(id, cancellationToken)).ToActionResult();
}
