namespace Ground.Extensions.Serializers.Abstractions
{
    /// <summary>
    /// Defines a contract for serializing objects to JSON and deserializing JSON data to objects.
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Serializes the specified input object to its string representation.
        /// </summary>
        /// <typeparam name="TInput">The type of the object to serialize.</typeparam>
        /// <param name="input">The object to serialize. Cannot be null.</param>
        /// <returns>A string containing the serialized representation of the input object.</returns>
        string Serialize<TInput>(TInput input);
        /// <summary>
        /// Deserializes the specified input string into an object of type <typeparamref name="TOutput"/>.
        /// </summary>
        /// <typeparam name="TOutput">The type of object to create from the deserialized input string.</typeparam>
        /// <param name="input">The string containing the serialized representation of the object to deserialize. Cannot be null or empty.</param>
        /// <returns>An instance of type <typeparamref name="TOutput"/> created from the input string.</returns>
        TOutput Deserialize<TOutput>(string input);
        /// <summary>
        /// Deserializes the specified input string into an object of the given type.
        /// </summary>
        /// <param name="input">The string containing the serialized representation of the object to deserialize. Cannot be null.</param>
        /// <param name="type">The type of object to create from the input string. Cannot be null.</param>
        /// <returns>An object instance of the specified type, populated with data from the input string.</returns>
        object Deserialize(string input, Type type);
    }
}
