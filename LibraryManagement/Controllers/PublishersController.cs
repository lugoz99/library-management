using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController(IPublisherService publisherService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PublisherDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await publisherService.GetAllPublisher(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PublisherDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await publisherService.GetPublisherByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDto>> Create(
            [FromBody] CreatePublisherDto dto,
            CancellationToken cancellationToken)
        {
            var result = await publisherService.CreatePublisherAsync(dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PublisherDto>> Update(
            Guid id,
            [FromBody] UpdatePublisherDto dto,
            CancellationToken cancellationToken)
        {
            var result = await publisherService.UpdatePublisherAsync(id, dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            // Corregido: Llamada al método correcto y pasando el cancellationToken
            var result = await publisherService.DeletePublisherAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}