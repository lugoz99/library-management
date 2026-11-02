using FluentResults;
using LibraryManagement.Models.DTOs;

namespace LibraryManagement.Services.Contracts;

public interface IPublisherService
{
    Task<Result<IEnumerable<PublisherDto>>> GetAllPublisher(CancellationToken cancellationToken = default);
    
    Task<Result<PublisherDto>> GetPublisherByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<PublisherDto>> CreatePublisherAsync(CreatePublisherDto dto, CancellationToken cancellationToken = default);
    
    Task<Result<PublisherDto>> UpdatePublisherAsync(Guid id, UpdatePublisherDto dto, CancellationToken cancellationToken = default);

    Task<Result> DeletePublisherAsync(Guid id, CancellationToken cancellationToken = default);
    
    
    // todo: pagination is missing
}