using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Reporting
{
    /// <summary>
    /// Interface representing a item on a report
    /// </summary>
    public interface IReportItem<T>
    {
        /// <summary>
        /// The items of type T behind the report item
        /// </summary>
        IEnumerable<T> Items { set; get; }
    }
}
