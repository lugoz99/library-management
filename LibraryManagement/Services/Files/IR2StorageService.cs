using FluentResults;

namespace LibraryManagement.Services.Files;

public interface IR2StorageService
{
    Task<Result<(string url, string key)>> UploadCoverAsync(
        string folder,
        string fileName,
        Stream stream, 
        string contentType,
        CancellationToken cancellationToken = default);
    
    Task<Result> DeleteAsync(
        string key,
        CancellationToken cancellationToken = default);

}