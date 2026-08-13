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
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll(
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.GetAllBooksAsync(cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.GetBookByIdAsync(id, cancellationToken);
        return result.ToActionResult();
    }

    // Create a simple book, without authors
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(
        [FromBody] CreateBookDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.CreateBookAsync(dto, cancellationToken);
        return result.ToActionResult();
    }

    // Create a book together with its authors in one request
    [HttpPost("with-authors")]
    public async Task<ActionResult<CreateBookWithAuthorsResponseDto>> CreateWithAuthors(
        [FromBody] CreateBookWithAuthorsDto dto,
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
    
    [HttpPut("id:guiid/cover")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> UpdateCover(
        Guid id,
        [FromForm] UpdateBookCoverDto dto,   
        CancellationToken cancellationToken = default)
    {
        var result = await bookService.UpdateBookCoverAsync(id, dto, cancellationToken);
        return result.ToActionResult();
    }
}