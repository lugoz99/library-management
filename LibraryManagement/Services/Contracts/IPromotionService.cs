using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IPromotionService
{
    Task<Result<IEnumerable<PromotionDto>>> GetByBookIdAsync(
        Guid bookId, CancellationToken cancellationToken = default);
    Task<Result<PromotionDto>> CreateAsync(
        Guid bookId, CreatePromotionDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
}
