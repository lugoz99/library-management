using System.Globalization;
using FluentResults;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Services.Contracts;
using MapsterMapper;

namespace LibraryManagement.Services.Implementation;

public class PublisherService(
    IPublisherRepository publisherRepository,
    IMapper mapper) : IPublisherService
{
    public async Task<Result<IEnumerable<PublisherDto>>> GetAllPublisher(CancellationToken cancellationToken)
    {
        var publishers = await publisherRepository.GetAllAsync(cancellationToken);
        return Result.Ok(mapper.Map<IEnumerable<PublisherDto>>(publishers));
    }

    public async Task<Result<PublisherDto>> GetPublisherByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(id, cancellationToken);
        return publisher is null ? 
            Result.Fail<PublisherDto>(new NotFoundError(nameof(PublisherDto), id)) 
            : Result.Ok(mapper.Map<PublisherDto>(publisher));
    }

    public async Task<Result<PublisherDto>> CreatePublisherAsync(CreatePublisherDto dto, CancellationToken cancellationToken)
    {
        var name = CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(dto.Name.Trim().ToLower());
            
        var exists = await publisherRepository.ExistsByNameAsync(name, null, cancellationToken);
        if (exists)
        {
            return Result.Fail<PublisherDto>(new ConflictError("cannot create publisher, the publisher already exists!"));
        }
        
        var publisher = mapper.Map<Publisher>(dto);
        publisher.Name = name;
        
        await publisherRepository.AddAsync(publisher, cancellationToken);
        return Result.Ok(mapper.Map<PublisherDto>(publisher));
    }

    public async Task<Result<PublisherDto>> UpdatePublisherAsync(Guid id, UpdatePublisherDto dto, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(id, cancellationToken);
        if (publisher is null)
        {
            return Result.Fail<PublisherDto>(new NotFoundError(nameof(Publisher), id));
        }
    
        // Normalizamos el nombre
        var formattedName = CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(dto.Name.Trim().ToLower());

        // Verificamos duplicados ignorando el registro actual
        var exists = await publisherRepository.ExistsByNameAsync(formattedName, id, cancellationToken);
        if (exists)
        { 
            return Result.Fail<PublisherDto>(new ConflictError("Cannot update publisher, a publisher with this name already exists."));
        }
    
        // Mapeamos las propiedades y asignamos el nombre limpio
        mapper.Map(dto, publisher);
        publisher.Name = formattedName;
    
        await publisherRepository.UpdateAsync(publisher, cancellationToken);
        return Result.Ok(mapper.Map<PublisherDto>(publisher));
    }

    public async Task<Result> DeletePublisherAsync(Guid id, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(id, cancellationToken);
        if (publisher is null)
        {
            return Result.Fail(new NotFoundError(nameof(PublisherDto), id));
        }

        await publisherRepository.DeleteAsync(publisher, cancellationToken);
        return Result.Ok();
    }
}