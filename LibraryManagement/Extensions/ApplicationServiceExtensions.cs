using LibraryManagement.Services.Contracts;
using LibraryManagement.Services.Implementation;

namespace LibraryManagement.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookFormatService, BookFormatService>();
        services.AddScoped<IReviewService, ReviewService>();

        return services;
    }
}