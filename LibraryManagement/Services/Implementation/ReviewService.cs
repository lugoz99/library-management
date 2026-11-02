using FluentResults;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class ReviewService(IReviewRepository reviewRepository,IMapper mapper):IReviewService
{
    public async Task<Result<IEnumerable<ReviewDto>>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
    {
        var reviews = await reviewRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<ReviewDto>>(reviews));
    }

    public async Task<Result<ReviewDto>> CreateReviewAsync(CreateReviewDto dto, CancellationToken cancellationToken = default)
    {
        var newReview = mapper.Map<Review>(dto);
        await reviewRepository.AddAsync(newReview, cancellationToken);
        return Result.Ok(mapper.Map<ReviewDto>(newReview));
    }

    
}