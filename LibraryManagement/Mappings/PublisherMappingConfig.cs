using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class PublisherMappingConfig
{
    public void Register()
    {
        // 1. CreatePublisherDto to Publisher
        TypeAdapterConfig<CreatePublisherDto, Publisher>
            .NewConfig().Ignore(dest => dest.Id);


        // 2. Publisher to PublisherDto
        TypeAdapterConfig<Publisher, PublisherDto>.NewConfig();

        // 3. UpdatePublisherDto to Publisher
        TypeAdapterConfig<UpdatePublisherDto, Publisher>.NewConfig()
            .IgnoreNullValues(true)
            .Ignore(p => p.Id);
    }
}