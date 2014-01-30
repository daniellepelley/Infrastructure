using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Newton.Extensions;
//using Pluto.BI;

namespace Newton.Core
{
    /// <summary>
    /// An item that makes up an index
    /// </summary>
    public class IndexItem : IIndexItem
    {
        #region Parameters

        private double average;
        /// <summary>
        /// The average
        /// </summary>
        public double Average
        {
            get { return average; }
            set { average = value; }
        }

        private double weighting = 1;
        /// <summary>
        /// Volume of items
        /// </summary>
        public double Weighting
        {
            get { return weighting; }
            set { weighting = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// An item that makes up an index
        /// </summary>
        public IndexItem()
        { }

        /// <summary>
        /// An item that makes up an index
        /// </summary>
        public IndexItem(double average)
        {
            this.average = average;
        }

        /// <summary>
        /// An item that makes up an index
        /// </summary>
        public IndexItem(double average, double weighting)
            : this(average)
        {
            this.weighting = weighting;
        }

        #endregion
    }
}
