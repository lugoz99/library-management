using System.Globalization;

namespace LibraryManagement.Common.Utils;

public static class Normalize
{
    public static string ToCapitalizeCase(string value)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Trim().ToLower());
    }
}