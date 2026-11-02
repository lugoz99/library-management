using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Models.Enums;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class PromotionService(
    IPromotionRepository promotionRepository,
    IMapper mapper) : IPromotionService
{
    public async Task<Result<IEnumerable<PromotionDto>>> GetByBookIdAsync(
        Guid bookId, CancellationToken cancellationToken = default)
    {
        if (!await promotionRepository.BookExistsAsync(bookId, cancellationToken))
            return Result.Fail<IEnumerable<PromotionDto>>(new NotFoundError(nameof(Book), bookId));

        var promotions = await promotionRepository.GetByBookIdAsync(bookId, cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<PromotionDto>>(promotions));
    }

    public async Task<Result<PromotionDto>> CreateAsync(
        Guid bookId, CreatePromotionDto dto, CancellationToken cancellationToken = default)
    {
        if (!await promotionRepository.BookExistsAsync(bookId, cancellationToken))
            return Result.Fail<PromotionDto>(new NotFoundError(nameof(Book), bookId));

        var startsAt = NormalizeUtc(dto.StartsAt);
        var endsAt = NormalizeUtc(dto.EndsAt);
        var validation = Validate(dto, startsAt, endsAt);
        if (validation.IsFailed)
            return Result.Fail<PromotionDto>(validation.Errors);

        if (await promotionRepository.HasOverlappingActivePromotionAsync(
                bookId, startsAt, endsAt, cancellationToken))
        {
            return Result.Fail<PromotionDto>(new ConflictError(
                "The book already has an active promotion that overlaps this period."));
        }

        var promotion = mapper.Map<Promotion>(dto);
        promotion.BookId = bookId;
        promotion.StartsAt = startsAt;
        promotion.EndsAt = endsAt;
        await promotionRepository.AddAsync(promotion, cancellationToken);
        return Result.Ok(mapper.Map<PromotionDto>(promotion));
    }

    public async Task<Result> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var promotion = await promotionRepository.GetByIdAsync(id, cancellationToken);
        if (promotion is null)
            return Result.Fail(new NotFoundError(nameof(Promotion), id));

        promotion.IsActive = false;
        await promotionRepository.UpdateAsync(promotion, cancellationToken);
        return Result.Ok();
    }

    private static Result Validate(CreatePromotionDto dto, DateTime startsAt, DateTime endsAt)
    {
        if (endsAt <= startsAt)
            return Result.Fail(new ValidationError(nameof(dto.EndsAt), "must be after StartsAt."));

        if (!Enum.IsDefined(dto.DiscountType))
            return Result.Fail(new ValidationError(nameof(dto.DiscountType), "has an invalid value."));

        if (dto.DiscountType == PromotionDiscountType.Percentage &&
            (dto.DiscountValue <= 0 || dto.DiscountValue > 100))
        {
            return Result.Fail(new ValidationError(nameof(dto.DiscountValue),
                "must be greater than 0 and no greater than 100 for percentage discounts."));
        }

        if (dto.DiscountType == PromotionDiscountType.FixedAmount && dto.DiscountValue <= 0)
            return Result.Fail(new ValidationError(nameof(dto.DiscountValue), "must be greater than 0."));

        return Result.Ok();
    }

    private static DateTime NormalizeUtc(DateTime value) =>
        value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
}
