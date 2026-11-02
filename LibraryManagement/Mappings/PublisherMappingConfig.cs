using LibraryManagement.Models;
using LibraryManagement.Models.DTOs;
using Mapster;

namespace LibraryManagement.Mappings;

public class PublisherMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePublisherDto, Publisher>()
            .Ignore(dest => dest.Id);

        config.NewConfig<Publisher, PublisherDto>();

        config.NewConfig<UpdatePublisherDto, Publisher>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Id);
    }
}