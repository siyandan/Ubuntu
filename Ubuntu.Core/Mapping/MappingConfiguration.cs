using System.Linq.Expressions;

namespace Ubuntu.Core.Mapping
{
    /// <summary>
    /// Represents the custom mapping rules for a specific source and destination type pair.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    public sealed class MappingConfiguration<TSource, TDestination> : IMappingConfiguration
    {
        private readonly Dictionary<string, string> _customMappings = new Dictionary<string, string>();

        /// <summary>
        /// Registers a custom mapping from a source property to a destination property using lambda expressions.
        /// This provides compile-time safety.
        /// </summary>
        /// <param name="destinationProperty">The destination property expression (e.g., dest => dest.FirstName).</param>
        /// <param name="sourceProperty">The source property expression (e.g., src => src.Name).</param>
        /// <returns>The current MappingConfiguration instance.</returns>
        public MappingConfiguration<TSource, TDestination> ForMember(
            Expression<Func<TDestination, object?>> destinationProperty,
            Expression<Func<TSource, object?>> sourceProperty)
        {
            var destinationName = GetPropertyName(destinationProperty);
            var sourceName = GetPropertyName(sourceProperty);

            _customMappings[destinationName] = sourceName;
            return this;
        }

        /// <summary>
        /// Registers a custom mapping from a source property to a destination property using strings.
        /// </summary>
        /// <param name="destinationProperty">The name of the destination property.</param>
        /// <param name="sourceProperty">The name of the source property.</param>
        public void ForMember(string destinationProperty, string sourceProperty)
        {
            _customMappings[destinationProperty] = sourceProperty;
        }

        /// <summary>
        /// Retrieves the source property name for a given destination property name.
        /// </summary>
        /// <param name="destinationProperty">The name of the destination property.</param>
        /// <param name="sourceProperty">The source property name, if found.</param>
        /// <returns>True if a custom mapping exists, otherwise false.</returns>
        public bool TryGetSourceProperty(string destinationProperty, out string sourceProperty)
        {
            return _customMappings.TryGetValue(destinationProperty, out sourceProperty!);
        }

        private string GetPropertyName<T>(Expression<Func<T, object?>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                return unaryMemberExpression.Member.Name;
            }

            throw new ArgumentException("Expression is not a member access expression.", nameof(expression));
        }
    }
}