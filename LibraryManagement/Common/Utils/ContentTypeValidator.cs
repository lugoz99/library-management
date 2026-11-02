using LibraryManagement.Helpers.Errors;

namespace LibraryManagement.Common.Utils;

using FluentResults;


public static class ContentTypeValidator
{
    private static readonly Dictionary<string, string> ContentTypesByExtension =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png"
        };

    public static Result ValidateContentType(string fileName, string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return Result.Fail(new ValidationError("contentType", "Content-Type is required."));

        var extension = Path.GetExtension(fileName);

        if (!ContentTypesByExtension.TryGetValue(extension, out var expectedContentType))
            return Result.Fail(new ValidationError("fileName", "Unsupported file extension."));

        if (!string.Equals(contentType, expectedContentType, StringComparison.OrdinalIgnoreCase))
            return Result.Fail(new ValidationError("contentType", "The file Content-Type does not match its extension."));

        return Result.Ok();
    }
}