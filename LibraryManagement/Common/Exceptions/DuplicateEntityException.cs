namespace LibraryManagement.Common.Exceptions;

public class DuplicateEntityException(string entityName, string? fieldName = null) : Exception(fieldName == null
    ? $"There is already a {entityName} with these data."
    : $"There is already a {entityName} with the same value in '{fieldName}'.")
{
    public string EntityName { get; } = entityName;
    public string? FieldName { get; } = fieldName;
}