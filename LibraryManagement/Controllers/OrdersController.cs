using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutResponseDto>> CreateCheckout(
        [FromBody] CreateCheckoutDto dto,
        CancellationToken cancellationToken) =>
        (await orderService.CreateCheckoutAsync(dto, cancellationToken)).ToActionResult();

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(
        Guid id, CancellationToken cancellationToken) =>
        (await orderService.GetByIdAsync(id, cancellationToken)).ToActionResult();
}
