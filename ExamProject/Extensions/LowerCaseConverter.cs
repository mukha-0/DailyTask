using System.Text.RegularExpressions;

namespace ExamProject.Extensions
{
    public static class LowerCaseConverter
    {
        public static string? Convert(object? value)
        {
            return value == null
                ? null
                : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
