using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Reporting
{
    /// <summary>
    /// A class that organises data for a pareto
    /// </summary>
    public class Pareto<TEntity>
    {
        #region Properties

        private Func<TEntity, string> grouper;
        /// <summary>
        /// The grouper that grouper the data for the pareto
        /// </summary>
        public Func<TEntity, string> Grouper
        {
            get { return grouper; }
            set { grouper = value; }
        }

        Func<IEnumerable<TEntity>, double> aggregator;
        /// <summary>
        /// The aggregator that calculates the measures
        /// </summary>
        public Func<IEnumerable<TEntity>, double> Aggregator
        {
            get { return aggregator; }
            set { aggregator = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the data for a pateto
        /// </summary>
        public IEnumerable<PatetoDataSetItem<TEntity>> Create(IEnumerable<TEntity> source)
        {
            return source.Pareto<TEntity>(grouper, aggregator);
        }

        #endregion
    }
}
