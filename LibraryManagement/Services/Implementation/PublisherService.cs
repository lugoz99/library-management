using FluentResults;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class PublisherService(
    IPublisherRepository publisherRepository,
    IMapper mapper):IPublisherService
{
    public Task<Result<PublisherDto>> GetAllPublisher(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PublisherDto>> GetPublisherByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PublisherDto>> CreatePublisherAsync(CreatePublisherDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PublisherDto>> UpdatePublisherAsync(Guid id, UpdatePublisherDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteCategoryAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}