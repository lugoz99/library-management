using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Common.Pagination;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await categoryService.GetAllCategoriesAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await categoryService.GetCategoryByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetPaged(
            [FromQuery] PaginationParams pagination,
            CancellationToken cancellationToken)
        {
            var result = await categoryService.GetPagedCategoriesAsync(
                pagination.PageNumber,
                pagination.PageSize,
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(
            [FromBody] CreateCategoryDto createDto,
            CancellationToken cancellationToken)
        {
            var result = await categoryService.CreateCategoryAsync(createDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> Update(
            Guid id,
            [FromBody] UpdateCategoryDto dto,
            CancellationToken cancellationToken)
        {
            var result = await categoryService.UpdateCategoryAsync(id, dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await categoryService.DeleteCategoryAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}