using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await reviewService.GetAllReviewsAsync(cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create(
        [FromBody] CreateReviewDto dto,
        CancellationToken cancellationToken)
    {
        var result = await reviewService.CreateReviewAsync(dto, cancellationToken);
        return result.ToActionResult();
    }
}