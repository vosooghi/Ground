namespace Ground.Extensions.ObjectMappers.Abstractions
{
    /// <summary>
    /// Defines a contract for mapping objects.
    /// </summary>
    public interface IMapperAdapter
    {
        /// <summary>
        /// Maps an object of type <typeparamref name="TSource"/> to a new instance of type <typeparamref
        /// name="TDestination"/>.
        /// </summary>
        /// <remarks>The mapping process copies relevant data from the source object to the destination
        /// type. The specific mapping rules depend on the implementation and may require compatible property names or
        /// custom configuration. If <paramref name="source"/> is null, an exception may be thrown.</remarks>
        /// <typeparam name="TSource">The type of the source object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object to map to.</typeparam>
        /// <param name="source">The source object to be mapped. Cannot be null.</param>
        /// <returns>A new instance of <typeparamref name="TDestination"/> populated with values mapped from <paramref
        /// name="source"/>.</returns>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
