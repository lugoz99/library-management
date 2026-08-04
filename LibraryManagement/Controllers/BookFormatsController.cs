using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookFormatsController(IBookFormatService bookFormatService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookFormatDto>>> GetAll()
        {
            var bookFormats = await bookFormatService.GetAllBookFormatAsync();
            return bookFormats.ToActionResult();
        }
        
        [HttpGet("id:guid")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll(Guid id)
        {
            var result = await bookFormatService.GetBookFormatByIdAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<BookFormatDto>> Create([FromBody] CreateBookFormatDto dto)
        {
            var result = await bookFormatService.CreateBookFormatAsync(dto);
            return result.ToActionResult();

        }
    }
}