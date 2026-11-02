using FluentResults.Extensions.AspNetCore;
using LibraryManagement.Helpers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Common
{
    // This class converts FluentResults errors to HTTP responses
    // It decides which HTTP status code to return based on error type
    public class FluentResultsEndpointProfile : DefaultAspNetCoreResultEndpointProfile
    {
        public override ActionResult TransformFailedResultToActionResult(
            FailedResultToActionResultTransformationContext context)
        {
            var result = context.Result;

            if (result.HasError<ValidationError>(out var validationErrors))
            {
                return new BadRequestObjectResult(
                    validationErrors.Select(e => new ErrorResponseDto(e.Message, e.ErrorCode)));
            }

            if (result.HasError<NotFoundError>(out var notFoundErrors))
            {
                var notFoundError = notFoundErrors.First();
                return new NotFoundObjectResult(
                    new ErrorResponseDto(notFoundError.Message, notFoundError.ErrorCode));
            }

            if (result.HasError<ConflictError>(out var conflictErrors))
            {
                var conflictError = conflictErrors.First();
                return new ConflictObjectResult(
                    new ErrorResponseDto(conflictError.Message, conflictError.ErrorCode));
            }

            if (result.HasError<UnauthorizedError>(out var unauthorizedErrors))
            {
                var unauthorizedError = unauthorizedErrors.First();
                return new UnauthorizedObjectResult(
                    new ErrorResponseDto(unauthorizedError.Message, unauthorizedError.ErrorCode));
            }

            if (result.HasError<ForbiddenError>(out var forbiddenErrors))
            {
                var forbiddenError = forbiddenErrors.First();
                return new ObjectResult(
                    new ErrorResponseDto(forbiddenError.Message, forbiddenError.ErrorCode))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            if (result.HasError<ThrottlingError>(out var throttlingErrors))
            {
                var error = throttlingErrors.First();
                var response = new ErrorResponseDto(error.Message, error.ErrorCode,
                    new Dictionary<string, object> { { "RetryAfter", error.RetryAfter } });

                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status429TooManyRequests
                };
            }

            if (result.HasError<InternalServerError>(out var serverErrors))
            {
                var error = serverErrors.First();
                return new ObjectResult(
                    new ErrorResponseDto(error.Message, error.ErrorCode))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            if (result.HasError<DomainError>(out var domainErrors))
            {
                var domainError = domainErrors.First();
                return new BadRequestObjectResult(
                    new ErrorResponseDto(domainError.Message, domainError.ErrorCode));
            }

            return new ObjectResult(new ErrorResponseDto("An unexpected error occurred", "500"))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}