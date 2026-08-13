using FluentResults;
using LibraryManagement.Helpers.Errors;

namespace LibraryManagement.Common.Utils;

public static class ExtensionValidator
{
    private static readonly HashSet<string> AllowedExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png"
        };

    public static Result ValidateExtension(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Result.Fail(new ValidationError("fileName", "File name is required."));

        var extension = Path.GetExtension(fileName);

        if (!AllowedExtensions.Contains(extension))
            return Result.Fail(new ValidationError("fileName", "Only JPG, JPEG and PNG files are allowed."));

        return Result.Ok();
    }
}