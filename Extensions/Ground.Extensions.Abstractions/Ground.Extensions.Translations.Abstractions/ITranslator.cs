namespace Ground.Extensions.Translations.Abstractions
{
    /// <summary>
    /// Defines a contract for retrieving and formatting localized strings by name, pattern, or a set of names. 
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="name">The key whose value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        string this[string name] { get; set; }
        /// <summary>
        /// Gets or sets the value associated with the specified name and arguments.
        /// </summary>
        /// <param name="name">The key or identifier used to retrieve or assign the value.</param>
        /// <param name="arguments">An optional array of additional arguments that further specify the value to get or set.</param>
        /// <returns>The value associated with the specified name and arguments.</returns>

        string this[string name, params string[] arguments] { get; set; }
        /// <summary>
        /// Gets or sets the string value associated with the specified sequence of names, joined by the given separator character.
        /// </summary>
        /// <param name="separator">The character used to separate the elements in the sequence of names when accessing the value.</param>
        /// <param name="names">An array of strings representing the sequence of names that identify the value. Cannot be null or contain
        /// null elements.</param>
        /// <returns>The string value associated with the specified sequence of names and separator.</returns>

        string this[char separator, params string[] names] { get; set; }
        /// <summary>
        /// Retrieves the string value associated with the specified name.
        /// </summary>
        /// <param name="name">The key or identifier used to locate the desired string.</param>
        /// <returns>The string value corresponding to the specified name</returns>
        string GetString(string name);
        /// <summary>
        /// Retrieves the string value associated with the pattern.
        /// </summary>
        /// from <paramref name="arguments"/>.</returns>
        string GetString(string pattern, params string[] arguments);

        string GetConcateString(char separator = ' ', params string[] names);

    }
}
