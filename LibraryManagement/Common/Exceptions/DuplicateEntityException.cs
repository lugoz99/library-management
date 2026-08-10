namespace LibraryManagement.Common.Exceptions;

public class DuplicateEntityException(string entityName, string? fieldName = null) : Exception(fieldName is null
    ? $"Ya existe un/a {entityName} con esos datos."
    : $"Ya existe un/a {entityName} con el mismo valor en '{fieldName}'.")
{
    public string EntityName { get; } = entityName;
    public string? FieldName { get; } = fieldName;
}