using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var bookFormats = await bookService.GetAllBooksAsync(cancellationToken);
        return bookFormats.ToActionResult();
    }

    [HttpGet("id:guid")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll(Guid id)
    {
        var result = await bookService.GetBookByIdAsync(id);
        return result.ToActionResult();
    }

    // Create a simple book, without authors
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
    {
        var result = await bookService.CreateBookAsync(dto);
        return result.ToActionResult();
    }

    // Create a book together with its authors in one request
    [HttpPost("with-authors")]
    public async Task<ActionResult<BookResponseDto>> CreateWithAuthors(
        [FromBody] CreateBookAuthorskDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.CreateBookWithAuthors(dto, cancellationToken);
        return result.ToActionResult();
    }

    // Add an existing author to an existing book
    [HttpPost("{id:guid}/authors")]
    public async Task<ActionResult> AddAuthor(
        Guid id,
        [FromBody] AddBookAuthorDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.AddAuthorToBookAsync(id, dto, cancellationToken);
        return result.ToActionResult();
    }
}