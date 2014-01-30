using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Dates;

namespace Newton.Reporting
{
    public static class ReportingStaticMethods
    {
        /// <summary>
        /// Groups and sorts the data as a pareto
        /// </summary>
        /// <param name="grouper">A grouper used to group the data</param>
        /// <param name="aggregator">An aggregator to calculate a value for each group</param>
        public static IEnumerable<PatetoDataSetItem<TEntity>> Pareto<TEntity>(this IEnumerable<TEntity> source, Func<TEntity, string> grouper, Func<IEnumerable<TEntity>, double> aggregator)
        {
            return source
                .GroupBy(grouper)
                .Select(g =>
                    new PatetoDataSetItem<TEntity>(
                        g.Key,
                        aggregator(g),
                        g.ToArray()))
                .OrderByDescending(g => g.Percentage)
                .ToArray();
        }

        ///// <summary>
        ///// Groups and sorts the data as a pareto
        ///// </summary>
        ///// <param name="grouper">A grouper used to group the data</param>
        ///// <param name="aggregator">An aggregator to calculate a value for each group</param>
        //public static IEnumerable<PatetoDataSetItem<T>> Pareto<T>(this IEnumerable<T> source, Func<T, string> grouper, Func<IEnumerable<T>, int> aggregator)
        //{
        //    return source.Pareto(grouper, t => Convert.ToDouble(aggregator(t)));
        //}
    }
}
