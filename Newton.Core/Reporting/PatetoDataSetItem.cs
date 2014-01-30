using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Reporting
{
    public class PatetoDataSetItem<T>
        : PatetoDataSetItem,
        IReportItem<T>
    {
        public IEnumerable<T> Items { set; get; }

        public PatetoDataSetItem(string header, double percentage, IEnumerable<T> items)
            : base(header, percentage)
        {
            this.Items = items;
        }
    }

    public class PatetoDataSetItem
    {
        public double Percentage { set; get; }
        public string Header { set; get; }

        public PatetoDataSetItem(string header, double percentage)
        {
            this.Header = header;
            this.Percentage = percentage;
        }
    }

}
