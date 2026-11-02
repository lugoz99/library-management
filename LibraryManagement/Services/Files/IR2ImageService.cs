namespace LibraryManagement.Services.Files;

public interface IR2ImageService
{
    Task<string> ConvertR2ToImageAsync(string r2FilePath, string outputDirectory, CancellationToken cancellationToken);
}