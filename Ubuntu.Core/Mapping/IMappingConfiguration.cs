namespace Ubuntu.Core.Mapping
{
    /// <summary>
    /// Non-generic interface for mapping configurations to allow runtime access.
    /// </summary>
    public interface IMappingConfiguration
    {
        /// <summary>
        /// Retrieves the source property name for a given destination property name.
        /// </summary>
        /// <param name="destinationProperty">The name of the destination property.</param>
        /// <param name="sourceProperty">The source property name, if found.</param>
        /// <returns>True if a custom mapping exists, otherwise false.</returns>
        bool TryGetSourceProperty(string destinationProperty, out string sourceProperty);
    }
}