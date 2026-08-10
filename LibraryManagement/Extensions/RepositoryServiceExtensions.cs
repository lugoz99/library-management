using LibraryManagement.Repository;
using LibraryManagement.Repository.Interfaces;

namespace LibraryManagement.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookFormatRepository, BookFormatRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}