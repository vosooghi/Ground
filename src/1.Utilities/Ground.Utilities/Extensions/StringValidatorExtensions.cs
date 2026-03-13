using System.Text.RegularExpressions;

namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// Provides extension methods for validating string values.
    /// </summary>
    public static class StringValidatorExtensions
    {
        /// <summary>
        /// Determines whether the length of the input string is between the specified minimum and maximum lengths (inclusive).
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>True if the string length is between the specified bounds; otherwise, false.</returns>
        public static bool IsLengthBetween(this string input, int minLength, int maxLength)
        {
            if (input.Length <= maxLength && input.Length >= minLength)
                return true;
            return false;
        }
    }
}
