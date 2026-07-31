using FluentResults;
using LibraryManagement.Common.Pagination;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services.Implementation
{
    public class CategoryService(
        ICategoryRepository repository,
        IMapper mapper)
        : ICategoryService
    {
        #region Métodos de Lectura (Queries)

        public async Task<Result<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var items = await repository
                .GetAllQueryable()
                .ToListAsync(cancellationToken);

            return Result.Ok(items.Adapt<IEnumerable<CategoryDto>>());
        }

        public async Task<Result<CategoryDetailDto>> GetCategoryByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            var category = await repository
                .GetByIdAsync(id, cancellationToken);

            if (category is null)
            {
                return Result.Fail(new NotFoundError(nameof(Category), id));
            }

            return Result.Ok(mapper.Map<CategoryDetailDto>(category));
        }

        public async Task<Result<PagedResult<CategoryDto>>> GetPagedCategoriesAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = repository.GetAllQueryable();

            var paged = await query.ToPagedAsync(pageNumber, pageSize, cancellationToken);

            var response = new PagedResult<CategoryDto>()
            {
                Items = paged.Items.Adapt<IEnumerable<CategoryDto>>(),
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount
            };

            return Result.Ok(response);
        }

        #endregion

        #region Métodos de Escritura (Commands)

        public async Task<Result<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            if (await repository.ExistsByNameAsync(dto.Name, cancellationToken: cancellationToken))
            {
                return Result.Fail(new ConflictError($"A category with the name '{dto.Name}' already exists."));
            }

            var category = dto.Adapt<Category>();

            await repository.AddAsync(category, cancellationToken);

            return Result.Ok(category.Adapt<CategoryDto>());
        }

        public async Task<Result<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            var category = await repository.GetByIdAsync(id, cancellationToken);

            if (category is null)
            {
                return Result.Fail(new NotFoundError(nameof(Category), id));
            }

            if (!string.Equals(category.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (await repository.ExistsByNameAsync(dto.Name, id, cancellationToken))
                {
                    return Result.Fail(new ConflictError($"Another category with the name '{dto.Name}' already exists."));
                }
            }

            mapper.Map(dto, category);
            category.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(category, cancellationToken);

            return Result.Ok(category.Adapt<CategoryDto>());
        }

        public async Task<Result> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await repository.GetByIdAsync(id, cancellationToken);

            if (category is null)
            {
                return Result.Fail(new NotFoundError(nameof(Category), id));
            }

            await repository.DeleteAsync(category, cancellationToken);

            return Result.Ok();
        }

        #endregion
    }
}