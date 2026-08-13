using Amazon.S3;
using Amazon.S3.Model;
using FluentResults;
using LibraryManagement.Common.Utils;
using LibraryManagement.Helpers.Errors;
using LibraryManagement.Services.Files;

namespace LibraryManagement.Services.Implementation;

public class R2StorageService(IAmazonS3 s3) : IR2StorageService
{
    // Name of the R2 bucket
    private readonly string _bucketName =
        Environment.GetEnvironmentVariable("CLOUDFLARE_R2_BUCKET_NAME")!;

    // Public URL used to access uploaded files
    private readonly string _publicUrl =
        Environment.GetEnvironmentVariable("CLOUDFLARE_R2_PUBLIC_URL")!;

    public async Task<Result<(string url, string key)>> UploadCoverAsync(
        string folder,
        string fileName,
        Stream stream,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate the file extension
            var extensionResult =
                ExtensionValidator.ValidateExtension(fileName);

            if (extensionResult.IsFailed)
                return Result.Fail<(string url, string key)>(
                    extensionResult.Errors);

            // Validate the file Content-Type
            var contentTypeResult =
                ContentTypeValidator.ValidateContentType(
                    fileName,
                    contentType);

            if (contentTypeResult.IsFailed)
                return Result.Fail<(string url, string key)>(
                    contentTypeResult.Errors);

            // Get the file extension
            var extension = Path.GetExtension(fileName);

            // Create a unique key for the file inside the folder
            var key = $"{folder}/{Guid.NewGuid()}{extension}";

            // Reset the stream position before uploading
            if (stream.CanSeek)
                stream.Position = 0;

            // Create the upload request
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                UseChunkEncoding = false // envíalo de una forma compatible con R2
            };

            // Upload the file to Cloudflare R2
            await s3.PutObjectAsync(
                request,
                cancellationToken);

            // Build the public URL of the uploaded file
            var url = $"{_publicUrl}/{key}";

           

            return Result.Ok((url, key));
        }
        catch (AmazonS3Exception ex)
        {
            // Show the real error returned by R2
            

            return Result.Fail<(string url, string key)>(
                new InternalServerError(
                    "Failed to upload file to Cloudflare R2.")
                    .CausedBy(ex.Message));
        }
        catch (Exception ex)
        {

            return Result.Fail<(string url, string key)>(
                new InternalServerError(
                    "An unexpected error occurred while uploading the file.")
                    .CausedBy(ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(
        string key,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Create the delete request
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
                
            };

            // Delete the file from Cloudflare R2
            await s3.DeleteObjectAsync(
                request,
                cancellationToken);


            return Result.Ok();
        }
        catch (AmazonS3Exception ex)
        {
            // Show the real error returned by R2
            

            return Result.Fail(
                new InternalServerError(
                    "Failed to delete file from Cloudflare R2.")
                    .CausedBy(ex));
        }
        catch (Exception ex)
        {
            // Show unexpected errors
            Console.WriteLine("========== UNEXPECTED DELETE ERROR ==========");
            Console.WriteLine($"Type: {ex.GetType().FullName}");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine("=============================================");

            return Result.Fail(
                new InternalServerError(
                    "An unexpected error occurred while deleting the file.")
                    .CausedBy(ex));
        }
    }
}