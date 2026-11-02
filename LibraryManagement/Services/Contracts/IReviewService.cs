using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IReviewService
{
    Task<Result<IEnumerable<ReviewDto>>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
    
    Task<Result<ReviewDto>> CreateReviewAsync(CreateReviewDto dto, CancellationToken cancellationToken = default);
    
}