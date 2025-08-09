namespace Ubuntu.Core.Mapping
{
    /// <summary>
    /// Defines the contract for an object mapper.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps the properties of a source object to a new destination object.
        /// Properties with matching names and types are automatically mapped.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>A new object of the destination type with mapped properties.</returns>
        TDestination Map<TDestination>(object source) where TDestination : new();
    }
}