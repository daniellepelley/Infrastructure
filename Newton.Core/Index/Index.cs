using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Newton.Extensions;
//using Pluto.BI;

namespace Newton.Core
{
    /// <summary>
    /// A class representing an index
    /// </summary>
    public class Index
    {
        #region Parameters

        private List<IIndexItem> items = new List<IIndexItem>();
        /// <summary>
        /// List of items
        /// </summary>
        public List<IIndexItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The indexed average
        /// </summary>
        public double? Average()
        {
            if (items.Count == 0)
                return null;

            return items.Select(i => i.Average * i.Weighting).Sum() /
                items.Select(i => i.Weighting).Sum();
        }

        #endregion
    }
}
