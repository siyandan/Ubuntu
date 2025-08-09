namespace Ubuntu.Core.Mapping
{
    /// <summary>
    /// A static class to manage all mapping configurations.
    /// </summary>
    public static class MapperConfiguration
    {
        internal static readonly Dictionary<(Type, Type), IMappingConfiguration> _configurations = new Dictionary<(Type, Type), IMappingConfiguration>();

        /// <summary>
        /// Creates a new mapping configuration for the specified source and destination types.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <returns>A new MappingConfiguration instance.</returns>
        public static MappingConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var key = (typeof(TSource), typeof(TDestination));
            var config = new MappingConfiguration<TSource, TDestination>();
            _configurations[key] = config;
            return config;
        }

        /// <summary>
        /// Retrieves the mapping configuration for a given source and destination type pair.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="sourceType">The type of the source object.</param>
        /// <param name="destinationType">The type of the destination object.</param>
        /// <returns>The MappingConfiguration instance, or null if not found.</returns>
        public static MappingConfiguration<TSource, TDestination>? GetConfiguration<TSource, TDestination>(Type sourceType, Type destinationType)
        {
            var key = (sourceType, destinationType);
            _configurations.TryGetValue(key, out var config);
            return (MappingConfiguration<TSource, TDestination>)config!;
        }

        /// <summary>
        /// Retrieves the mapping configuration for a given source and destination type pair using runtime types.
        /// </summary>
        /// <param name="sourceType">The type of the source object.</param>
        /// <param name="destinationType">The type of the destination object.</param>
        /// <returns>The configuration object, or null if not found.</returns>
        public static IMappingConfiguration? GetConfiguration(Type sourceType, Type destinationType)
        {
            var key = (sourceType, destinationType);
            _configurations.TryGetValue(key, out var config);
            return config;
        }
    }
}