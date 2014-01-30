using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    /// <summary>
    /// A class of IEnumerable extensions
    /// </summary>
    public static class Nullables
    {
        /// <summary>
        /// Converts a nullable type into another type
        /// </summary>
        public static TOutput Convert<TInput, TOutput>(this Nullable<TInput> source, Func<TInput, TOutput> converter)
            where TInput : struct
        {
            return source.Convert(converter, default(TOutput));
        }

        /// <summary>
        /// Converts a nullable type into another type
        /// </summary>
        public static TOutput Convert<TInput, TOutput>(this Nullable<TInput> source, Func<TInput, TOutput> converter, TOutput defaultValue)
            where TInput : struct
        {
            if (source.HasValue &&
                converter != null)
            {
                return converter(source.Value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Converts a nullable type which inherites from IFormatable into a string
        /// </summary>
        public static string ToString<T>(this Nullable<T> source, string format)
            where T : struct
        {
            return source.ToString(format, null);
        }

        /// <summary>
        /// Converts a nullable type which inherites from IFormatable into a string
        /// </summary>
        public static string ToString<T>(this Nullable<T> source, string format, IFormatProvider formatProvider)
            where T : struct
        {
            if (source.HasValue &&
                typeof(IFormattable).IsAssignableFrom(typeof(T)))
            {
                return ((IFormattable)source.Value).ToString(format, formatProvider);
            }
            return string.Empty;
        }
    }
}
