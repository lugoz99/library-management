using FluentResults;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Common.Pagination;

namespace LibraryManagement.Services.Contracts
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
        Task<Result<CategoryDetailDto>> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<PagedResult<CategoryDto>>> GetPagedCategoriesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        Task<Result<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<Result<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<Result> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default);
    }
}