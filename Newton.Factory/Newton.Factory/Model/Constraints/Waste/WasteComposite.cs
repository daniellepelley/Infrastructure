using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Factory;

namespace Newton.Factory
{
    public class WasteComposite : IWaste
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

        private TimeSpan timeSpan;
        /// <summary>
        /// The timespan for the downtime to be measured over
        /// </summary>
        public TimeSpan TimeSpan
        {
            get { return timeSpan; }
            set { timeSpan = value; }
        }

        private List<IWaste> wastes = new List<IWaste>();

        public List<IWaste> Wastes
        {
            get { return wastes; }
            set { wastes = value; }
        }

        /// <summary>
        /// The amount of waste over a given timespan
        /// </summary>
        public Rate Rate
        {
            get { return wastes.TotalRate(timeSpan); }
        }

        public WasteComposite()
        {

        }

        public WasteComposite(IWaste[] wastes)
        {
            this.Wastes.AddRange(wastes);
        }

        /// <summary>
        /// The rate in units
        /// </summary>
        public Rate UnitRate(Rate conversionRate)
        {
            return
                new Rate()
                {
                    Quantity = Rate.GetQuantityForTimeSpan(conversionRate.TimeSpan),
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
                    Quantity = Rate.ToTimeSpanRate(conversionRate).GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The efficiency of the dimension
        /// </summary>
        public double Efficiency(Rate conversionRate)
        {
            return TimeSpanRate(conversionRate).Efficiency;
        }
    }
}
