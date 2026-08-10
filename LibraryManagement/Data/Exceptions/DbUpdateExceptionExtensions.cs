namespace LibraryManagement.Data.Exceptions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


public static class DbUpdateExceptionExtensions
{
    public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
    {
        if (ex.InnerException is SqlException sqlEx)
            return sqlEx.Number is 2601 or 2627;

        return false;
    }
}