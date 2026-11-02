using Amazon.S3;
using LibraryManagement.Services.Files;
using LibraryManagement.Services.Implementation;

namespace LibraryManagement.Extensions;

public static class CloudlfareServiceExtension
{
    /// <summary>
    /// Adds Cloudflare R2 services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ClouldflareServiceExtension(this IServiceCollection services)
    {
        var accessKeyId = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_ACCESS_KEY_ID");
        var secretAccessKey = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_SECRET_ACCESS_KEY");

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var config = new AmazonS3Config
            {
                ServiceURL = Environment.GetEnvironmentVariable("CLOUDFLARE_R2_SERVICE_URL"),
                ForcePathStyle = true,
                AuthenticationRegion = "auto"
            };

            return new AmazonS3Client(accessKeyId, secretAccessKey, config);
        });

        services.AddScoped<IR2StorageService, R2StorageService>();
        return services;
    }

}