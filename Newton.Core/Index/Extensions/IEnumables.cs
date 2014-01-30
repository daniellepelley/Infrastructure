using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    /// <summary>
    /// A class of IEnumerable extensions
    /// </summary>
    public static class IEnumerables
    {
        #region Strings

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// carriage returns.
        /// </summary>
        public static string AsLines<T>(this IEnumerable<T> source)
        {
            StringBuilder sb = new StringBuilder();
            List<T> list = source.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                sb.AppendLine(list[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static string AsString<T>(this IEnumerable<T> source, string seperater)
        {
            return source.AsString(seperater, seperater);
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static string AsString<T>(this IEnumerable<T> source, string seperater, string lastSeperater)
        {
            StringBuilder sb = new StringBuilder();
            List<string> list = source.Select<T, string>(s => s == null ? string.Empty : s.ToString()).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 2)
                    sb.Append(list[i] + seperater);
                else if (i < list.Count - 1)
                    sb.Append(list[i] + lastSeperater);
                else
                    sb.Append(list[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// carriage returns.
        /// </summary>
        public static IEnumerable<string> AsNotes<T>(this IEnumerable<T> source, int numberOfLines)
        {
            IEnumerable<IEnumerable<T>> notes;

            if (numberOfLines > 0)
                notes = source.Segment(numberOfLines);
            else
                notes = new IEnumerable<T>[] { source };

            List<string> returnList = new List<string>();
            foreach (IEnumerable<string> note in notes)
            {
                returnList.Add(note.AsLines());
            }
            return returnList.AsEnumerable();
        }

        /// <summary>
        /// Does a fuzzy comparison on a string collection returning strings that are like the
        /// the match string.
        /// </summary>
        /// <param name="match">The string to match</param>
        /// <param name="distance">The maximum distance for the match</param>
        public static IEnumerable<string> LevanMatch(this IEnumerable<string> source, string match, int distance)
        {
            List<string> list = new List<string>();
            foreach (string str in source)
            {
                if (str.Distance(match) <= distance)
                    list.Add(match);
            }
            return list.AsEnumerable();
        }

        /// <summary>
        /// Converts a collection of T using the passed converter
        /// </summary>
        public static IEnumerable<T> ConvertAll<T>(this IEnumerable<T> source, Func<T, T> converter)
        {
            if (converter == null)
                return source;

            List<T> list = new List<T>();

            foreach (T item in source)
            {
                list.Add(converter(item));
            }
            return list;
        }

        /// <summary>
        /// Converts a collection of T using the passed converter
        /// </summary>
        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> source)
        {
            List<T> list = new List<T>();

            foreach (T item in source)
            {
                if (item != null)
                    list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Converts a collection of strings all to upper case.
        /// </summary>
        public static IEnumerable<string> ToUpper(this IEnumerable<string> source)
        {
            return source.ConvertAll(s => s.ToUpper());
        }

        /// <summary>
        /// Converts a collection of strings all to lower case.
        /// </summary>
        public static IEnumerable<string> ToLower(this IEnumerable<string> source)
        {
            return source.ConvertAll(s => s.ToLower());
        }


        public static IEnumerable<string> Unique(this IEnumerable<string> source)
        {
            List<string> output = source.ToList();

            for (int i = 0; i < output.Count; i++)
            {
                if (output.Take(i + 1).Duplicates().Count() > 0)
                {
                    string oldValue = output[i];

                    int version = 1;

                    do
                    {
                        version++;
                        output[i] = string.Format("{0} ({1})", oldValue, version);
                    }
                    while (output.Take(i + 1).Duplicates().Count() > 0);
                }
            }
            return output;
        }


        #endregion

        #region Char

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static string AsString(this IEnumerable<char> source)
        {
            return source.AsStringList().AsString(null);
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static string AsString(this IEnumerable<char> source, string seperater)
        {
            return source.AsStringList().AsString(seperater, seperater);
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static string AsString(this IEnumerable<char> source, string seperater, string lastSeperater)
        {
            return source.AsStringList().AsString(seperater, lastSeperater);
        }

        /// <summary>
        /// Converts a collection of strings into a single string seperated by
        /// seperaters.
        /// </summary>
        public static List<string> AsStringList(this IEnumerable<char> source)
        {
            List<string> list = new List<string>();
            foreach (char ch in source)
                list.Add(ch.ToString());

            return list;
        }

        #endregion

        #region Double

        /// <summary>
        /// Converts a collection of doubles into a distribution curve
        /// </summary>
        public static KeyValuePair<string, double>[] DistributionCurve(this IEnumerable<double> source)
        {
            return DistributionCurve(source, source.Min(), source.Max());
        }

        /// <summary>
        /// Converts a collection of doubles into a distribution curve
        /// </summary>
        public static KeyValuePair<string, double>[] DistributionCurve(this IEnumerable<double> source, double min, double max)
        {
            return DistributionCurve(source, min, max, 30);
        }

        /// <summary>
        /// Converts a collection of doubles into a distribution curve
        /// </summary>
        public static KeyValuePair<string, double>[] DistributionCurve(this IEnumerable<double> source, double min, double max, int segments)
        {
            double[] range = Newton.Core.Ranges.DoubleRange(min, max, segments);

            List<double> results = new List<double>();
            results.AddRange(source.BellCurveX(segments));

            List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
            for (int i = 0; i < results.Count; i++)
            {
                list.Add(new KeyValuePair<string, double>(range[i].ToString(), results[i]));
            }
            return list.ToArray();
        }
        
        #endregion

        #region General

        /// <summary>
        ///  Returns any elements from the passed IEnumerable that are missing from this sequence.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Segment<T>(this IEnumerable<T> source, int itemPerSegment)
        {
            List<IEnumerable<T>> groups = new List<IEnumerable<T>>();
            List<T> items = new List<T>();

            foreach (T item in source)
            {
                items.Add(item);
                if (items.Count == itemPerSegment)
                {
                    groups.Add(items);
                    items = new List<T>();
                }
            }
            if (items.Count > 0)
                groups.Add(items);

            return groups.AsEnumerable();
        }

        /// <summary>
        /// Finds all duplicates in the passed IEnumerable
        /// </summary>
        public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> source)
        {
            return source.Duplicates(s => s);
        }

        /// <summary>
        /// Finds all duplicates in the passed IEnumerable
        /// </summary>
        public static IEnumerable<T> Duplicates<T, TKey>(this IEnumerable<T> source, Func<T, TKey> func)
        {
            var groups = source.GroupBy(func);
            return groups.Where(g => g.Count() > 1).SelectMany(g => g.ToArray());
        }

        /// <summary>
        ///  Returns an unbroken block of elements from a sequence as long as a specified condition is true for all.
        /// </summary>
        public static IEnumerable<T> TakeInSequence<T>(this IEnumerable<T> source, int amount, Func<T, bool> predicate)
        {
            List<T> returnList = new List<T>();
            foreach(T item in source)
            {
                if (predicate.Invoke(item))
                {
                    returnList.Add(item);
                }
                else
                {
                    returnList.Clear();
                }
                if (returnList.Count == amount)
                    return returnList.AsEnumerable();
            }
            returnList.Clear();
            return returnList.AsEnumerable();
        }

        /// <summary>
        ///  Returns an unbroken block of elements from a sequence using a predicate to see if the new element should come after the last element
        /// </summary>
        public static IEnumerable<T> TakeInSequence<T>(this IEnumerable<T> source, int amount, Func<T, T, bool> sequencePredicate)
        {
            List<T> returnList = new List<T>();

            foreach (T item in source)
            {
                if (returnList.Count > 0)
                {
                    if (sequencePredicate.Invoke(item, returnList[returnList.Count - 1]))
                    {
                        returnList.Add(item);
                    }
                    else
                    {
                        returnList.Clear();
                    }
                }
                else
                {
                    returnList.Add(item);
                }

                if (returnList.Count == amount)
                    return returnList.AsEnumerable();
            }
            returnList.Clear();
            return returnList.AsEnumerable();
        }

        /// <summary>
        //  Returns an unbroken block of elements from a sequence using a predicate to see if the new element should come after the last element
        /// </summary>
        public static IEnumerable<T> TakeInSequence<T>(this IEnumerable<T> source, int amount, Func<T, bool> predicate, Func<T, T, bool> sequencePredicate)
        {
            List<T> returnList = new List<T>();

            foreach (T item in source)
            {
                //If the element passing the predicate
                if (predicate.Invoke(item))
                {
                    if (returnList.Count > 0)
                    {
                        //Checks if the new element follows the last element
                        if (sequencePredicate.Invoke(returnList[returnList.Count - 1], item))
                        {
                            returnList.Add(item);
                        }
                        else
                        {
                            returnList.Clear();
                        }
                    }
                    else
                    {
                        //If the list is empty adds the element
                        returnList.Add(item);
                    }
                }
                else
                {
                    returnList.Clear();
                }

                if (returnList.Count == amount)
                    return returnList.AsEnumerable();
            }
            returnList.Clear();
            return returnList.AsEnumerable();
        }

        /// <summary>
        //  Returns an unbroken block of elements from a sequence using a predicate to see if the new element should come after the last element
        /// </summary>
        public static IEnumerable<List<T>> TakeSequences<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, T, bool> sequencePredicate, int? shortestSequence)
        {
            List<T> returnList = new List<T>();
            List<List<T>> returnLists = new List<List<T>>();

            foreach (T item in source)
            {
                //If the element passing the predicate
                if (predicate.Invoke(item))
                {
                    if (returnList.Count > 0)
                    {
                        //Checks if the new element follows the last element
                        if (sequencePredicate.Invoke(returnList[returnList.Count - 1], item))
                        {
                            returnList.Add(item);
                        }
                        else
                        {
                            if (!shortestSequence.HasValue ||
                                returnList.Count >= shortestSequence.Value)
                            {
                                returnLists.Add(returnList);
                            }
                            returnList = new List<T>();
                            returnList.Add(item);
                        }
                    }
                    else
                    {
                        //If the list is empty adds the element
                        returnList.Add(item);
                    }
                }
            }
            return returnLists.AsEnumerable();
        }

        /// <summary>
        //  Returns an unbroken block of elements from a sequence using a predicate to see if the new element should come after the last element
        /// </summary>
        public static IEnumerable<List<T>> TakeSequences<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, T, bool> sequencePredicate)
        {
            return source.TakeSequences(predicate, sequencePredicate, null);

            //List<T> returnList = new List<T>();
            //List<List<T>> returnLists = new List<List<T>>();

            //foreach (T item in source)3
            //{
            //    //If the element passing the predicate
            //    if (predicate.Invoke(item))
            //    {
            //        if (returnList.Count > 0)
            //        {
            //            //Checks if the new element follows the last element
            //            if (sequencePredicate.Invoke(returnList[returnList.Count - 1], item))
            //            {
            //                returnList.Add(item);
            //            }
            //            else
            //            {
            //                returnLists.Add(returnList);
            //                returnList = new List<T>();
            //                returnList.Add(item);
            //            }
            //        }
            //        else
            //        {
            //            //If the list is empty adds the element
            //            returnList.Add(item);
            //        }
            //    }
            //}
            //return returnLists.AsEnumerable();
        }

        /// <summary>
        //  Returns the longest unbroken block of elements from a sequence as long as a specified condition is true for all.
        /// </summary>
        public static IEnumerable<T> LongestSequence<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (predicate == null) { return null; }

            List<T> returnList = new List<T>();
            List<T> tempList = new List<T>();

            foreach (T item in source)
            {
                if (predicate.Invoke(item))
                {
                    tempList.Add(item);
                }
                else
                {
                    if (tempList.Count > returnList.Count)
                    {
                        returnList = tempList;
                    }
                    tempList = new List<T>();
                }
            }
            return returnList.AsEnumerable();
        }

        /// <summary>
        ///  Returns any elements from the passed IEnumerable that are missing from this sequence.
        /// </summary>
        public static IEnumerable<T> Missing<T>(this IEnumerable<T> source1, IEnumerable<T> source2, Func<T, T, bool> match)
        {
            List<T> missingItems = new List<T>();
            foreach (T item1 in source1)
            {
                bool isFound = false;
                foreach (T item2 in source2)
                {
                    if (match.Invoke(item1, item2))
                    {
                        isFound = true;
                    }
                }
                if (!isFound)
                {
                    missingItems.Add(item1);
                }
            }
            return missingItems.AsEnumerable();
        }

        /// <summary>
        ///  Updates any matching elements from the passed IEnumerable.
        /// </summary>
        public static void Update<T>(this IEnumerable<T> toSource, IEnumerable<T> fromSource, Func<T, T, bool> match, Action<T, T> action)
        {
            var pairs = (from f in fromSource
                         from t in toSource
                         where match(f, t)
                         select new { f, t });

            foreach (var pair in pairs)
            {
                action(pair.f, pair.t);
            }
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the match function.
        /// </summary>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> match)
        {
            List<T> sourceList = new List<T>();
            sourceList.AddRange(source);
            List<T> distinctList = new List<T>();

            distinctList.Add(sourceList[0]);
            for (int i = 1; i < sourceList.Count(); i++)
            {
                bool isFound = false;
                foreach(T item in distinctList)
                {
                    if (match.Invoke(sourceList[i], item))
                    {
                        isFound = true;
                    }
                }
                if (!isFound)
                {
                    distinctList.Add(sourceList[i]);
                }
            }
            return distinctList.AsEnumerable();
        }

        /// <summary>
        /// Trims the passed number of items off the end of the collection.
        /// </summary>
        public static IEnumerable<T> Trim<T>(this IEnumerable<T> source, int index)
        {
            List<T> list = source.ToList();
            list.RemoveRange(index, list.Count - index);
            return list;
        }

        ///// <summary>
        ///// Groups the items into buckets.
        ///// </summary>
        //public static IEnumerable<IGrouping<BucketHeader, T>> BucketGroup<T>(this IEnumerable<T> source, IEnumerable<Bucket<T>> buckets)
        //{
        //    return BucketEngine<T>.Group(source, buckets);
        //}

        /// <summary>
        /// Converts an IEnumerable<IEnumerable<T>> to a IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            List<T> list = source.ToList();
            List<T> returnList = new List<T>();
            Random random = new Random();

            while (list.Count > 0)
            {
                int j = random.Next(list.Count);
                returnList.Add(list[j]);
                list.RemoveAt(j);
            }

            return returnList.ToArray();
        }

        /// <summary>
        /// Returns the top items
        /// </summary>
        public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> source, int amount, Func<T, TKey> keySelector)
        {
            return source.OrderBy(keySelector).Take(amount);
        }

        /// <summary>
        /// Returns the bottom items
        /// </summary>
        public static IEnumerable<T> Bottom<T, TKey>(this IEnumerable<T> source, int amount, Func<T, TKey> keySelector)
        {
            return source.OrderByDescending(keySelector).Take(amount);
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts a collection of nullables to a collection of non nullables by stripping
        /// out the nulls from the collection
        /// </summary>
        public static IEnumerable<T> ToNonNullable<T>(this IEnumerable<T?> source) where T : struct
        {
            return source.Where(s => s.HasValue).Select(s => s.Value);
        }

        #endregion

        #region OrderBy

        /// <summary>
        /// Converts an IEnumerable<IEnumerable<T>> to a IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, T firstItem)
        {
            List<T> list = source.OrderBy(keySelector).ToList();
            if (firstItem != null)
            {
                list.Remove(firstItem);
                list.Insert(0, firstItem);
            }
            return list;
        }

        /// <summary>
        /// Moves preferred items to the top of the list if found
        /// </summary>
        public static IEnumerable<T> Preferred<T>(this IEnumerable<T> source, IEnumerable<T> preferred)
        {
            return
                preferred.Concat(source)
                         .Duplicates()
                         .Distinct()
                         .Concat(
                         source.Except(preferred));
        }

        #endregion

        #region WhereNot

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (predicate == null)
                return source;

            return source.Where(s => !predicate(s));
        }

        #endregion

    }
}
