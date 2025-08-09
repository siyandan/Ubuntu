using Microsoft.Extensions.DependencyInjection;
using Ubuntu.Core.Mapping;

namespace Ubuntu.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for IServiceCollection to register Ubuntu.Core services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the IMapper and UbuntuMapper services with the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance for chaining.</returns>
        public static IServiceCollection AddUbuntuMappers(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, UbuntuMapper>();
            return services;
        }
    }
}