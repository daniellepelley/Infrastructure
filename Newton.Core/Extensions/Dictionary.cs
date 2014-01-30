using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merges two dictionaries into a single dictionary
        /// </summary>
        /// <param name="dictionary">The dictionary to merge</param>
        /// <param name="overwrite">Whether the values in the new dictionary will overwrite the existing values</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary)
        {
            return source.Merge(dictionary, true);
        }

        /// <summary>
        /// Merges two dictionaries into a single dictionary
        /// </summary>
        /// <param name="dictionary">The dictionary to merge</param>
        /// <param name="overwrite">Whether the values in the new dictionary will overwrite the existing values</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary, bool overwrite)
        {
            if (source == null ||
                dictionary == null)
            {
                return source;
            }

            foreach(KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (source.ContainsKey(pair.Key))
                {
                    if (overwrite)
                    {
                        source[pair.Key] = pair.Value;
                    }
                }
                else
                {
                    source.Add(pair.Key, pair.Value);
                }
            }
            return source;
        }


        /// Merges two dictionaries into a single dictionary
        /// </summary>
        /// <param name="dictionary">The dictionary to merge</param>
        /// <param name="overwrite">Whether the values in the new dictionary will overwrite the existing values</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source == null ||
                key == null)
            {
                return source;
            }

            if (source.ContainsKey(key))
            {
                source[key] = value;
            }
            else
            {
                source.Add(key, value);
            }
            return source;
        }
    }
}
