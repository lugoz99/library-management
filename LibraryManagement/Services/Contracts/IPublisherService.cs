using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IPublisherService
{
    Task<Result<PublisherDto>> GetAllPublisher(CancellationToken cancellationToken);
    
    Task<Result<PublisherDto>> GetPublisherByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Result<PublisherDto>> CreatePublisherAsync(CreatePublisherDto dto, CancellationToken cancellationToken);
    
    Task<Result<PublisherDto>> UpdatePublisherAsync(Guid id, UpdatePublisherDto dto, CancellationToken cancellationToken);

    Task<Result> DeleteCategoryAsync(Guid id);
    
    
    // TODO: Pagination is missing

}