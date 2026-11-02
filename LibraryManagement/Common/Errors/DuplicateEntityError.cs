using FluentResults;

namespace LibraryManagement.Common.Errors;

public class DuplicateEntityError : Error
{
    public DuplicateEntityError(string entityName, string? fieldName = null)
        : base(fieldName is null
            ? $"Ya existe un/a {entityName} con esos datos."
            : $"Ya existe un/a {entityName} con el mismo valor en '{fieldName}'.")
    {
        Metadata.Add("ErrorCode", "DuplicateEntity");
        Metadata.Add("Entity", entityName);
        if (fieldName is not null)
            Metadata.Add("Field", fieldName);
    }
}