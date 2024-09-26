namespace Pavas.Patterns.UnitOfWork.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="string"/> type.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Determines whether a string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><see langword="true"/> if the string is null, empty, or consists only of white-space characters; otherwise, <see langword="false"/>.</returns>
    public static bool IsNullOrEmptyOrWhiteSpace(this string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}