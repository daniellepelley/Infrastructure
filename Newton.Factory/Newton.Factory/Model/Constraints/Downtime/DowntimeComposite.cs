using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Factory;

namespace Newton.Factory
{
    public class DowntimeComposite
        : IDowntime
    {
        private string title;
        /// <summary>
        /// The title of the downtime
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private TimeSpan timeSpan = new TimeSpan(1, 0, 0);
        /// <summary>
        /// The timespan for the downtime to be measured over
        /// </summary>
        public TimeSpan TimeSpan
        {
            get { return timeSpan; }
            set { timeSpan = value; }
        }

        private List<IDowntime> downtimes = new List<IDowntime>();

        public List<IDowntime> Downtimes
        {
            get { return downtimes; }
            set { downtimes = value; }
        }

        /// <summary>
        /// The rate of down time over a given timespan
        /// </summary>
        public TimeSpanRate Rate
        {
            get { return downtimes.TotalRate(timeSpan); }
        }

        /// <summary>
        /// The total delta for the contained downtimes
        /// </summary>
        public double Delta
        {
            get
            {
                long total = Rate.GetTicksForTicks(1000000);
                return downtimes.Select(d => d.Delta * (d.Rate.GetTicksForTicks(1000000).Divide(total))).Sum();            
            }
            set
            {
                foreach (IDelta iDelta in downtimes)
                {
                    iDelta.Delta = value;
                }
            }
        }

        public DowntimeComposite()
        {

        }

        public DowntimeComposite(IDowntime[] downtimes)
        {
            this.Downtimes.AddRange(downtimes);
        }

        /// <summary>
        /// The rate in units
        /// </summary>
        public Rate UnitRate(Rate conversionRate)
        {
            return
                new Rate()
                {
                    Quantity = Rate.ToRate(conversionRate).GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The timespan rate
        /// </summary>
        public TimeSpanRate TimeSpanRate(Rate conversionRate)
        {
            return
                new TimeSpanRate()
                {
                    Quantity = Rate.GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The efficiency of the dimension
        /// </summary>
        public double Efficiency(Rate conversionRate)
        {
            return Rate.Efficiency;
        }

        public double DeltaEfficiency(Rate conversionRate)
        {
            return Delta * Efficiency(conversionRate);
        }
    }
}
