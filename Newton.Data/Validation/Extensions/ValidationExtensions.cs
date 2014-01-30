using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// Entension methods for Newton.Validation
    /// </summary>
    public static partial class ValidationExtensions
    {
        /// <summary>
        /// Return whether or not the entity is valid
        /// </summary>
        public static bool IsValid<T>(this IEntityRuleProvider<T> source, T entity)
        {
            return source.Validate(entity).Count == 0;
        }
    }
}
