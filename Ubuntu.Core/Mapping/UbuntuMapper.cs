using System.Reflection;

namespace Ubuntu.Core.Mapping
{
    /// <summary>
    /// A custom object mapper that implements the <see cref="IMapper"/> interface.
    /// It supports property mapping by name and a configurable custom mapping using reflection.
    /// </summary>
    public sealed class UbuntuMapper : IMapper
    {
        /// <summary>
        /// Maps the properties of a source object to a new destination object.
        /// Properties with matching names and types are automatically mapped.
        /// If a custom mapping is configured, it will be used instead of the default name matching.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>A new object of the destination type with mapped properties.</returns>
        public TDestination Map<TDestination>(object source) where TDestination : new()
        {
            TDestination destination = new();
            Type sourceType = source.GetType();
            Type destinationType = typeof(TDestination);

            // Get the configuration using the non-generic method
            var config = MapperConfiguration.GetConfiguration(sourceType, destinationType);

            foreach (var destinationProperty in destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                PropertyInfo? sourceProperty = null;

                if (config != null && config.TryGetSourceProperty(destinationProperty.Name, out var sourcePropertyName))
                    sourceProperty = sourceType.GetProperty(sourcePropertyName);

                if (sourceProperty is null)
                    sourceProperty = sourceType.GetProperty(destinationProperty.Name);

                if (sourceProperty != null && destinationProperty.CanWrite && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    object value = sourceProperty!.GetValue(source)!;
                    destinationProperty.SetValue(destination, value);
                }
            }
            return destination;
        }
    }
}