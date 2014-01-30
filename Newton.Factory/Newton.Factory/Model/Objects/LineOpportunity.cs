using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class LineOpportunity : ConstraintCube
    {
        public LineOpportunity()
        {
            this.Dimensions.Add(Downtime);
            this.Dimensions.Add(Waste);
            this.Dimensions.Add(SlowRunning);
        }


        public IDowntime Downtime { get; set; }
        public IWaste Waste { get; set; }
        public ISlowRunning SlowRunning { get; set; }

        public Rate BottleNeckRate { get; set; }

        public Rate ActualRate { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public double ValuePerUnit { get; set; }

        public double WasteDelta { get; set; }
        public double DowntimeDelta { get; set; }
        public double SpeedDelta { get; set; }

        public double TotalValue()
        {
            double potentialUnits = BottleNeckRate.GetQuantityForTimeSpan(TimeSpan);

            double actualUnits = ActualRate.GetQuantityForTimeSpan(TimeSpan) -
                                 Downtime.ToRate(BottleNeckRate).GetQuantityForTimeSpan(TimeSpan) -
                                 Waste.Rate.GetQuantityForTimeSpan(TimeSpan);

            return (potentialUnits - actualUnits) * ValuePerUnit;
        }

        public double Availibility
        {
            get
            {
                return (TimeSpan - Downtime.Rate.IncreaseByRatio(1 - DowntimeDelta).GetQuantityForTimeSpan(TimeSpan)).TotalMilliseconds / TimeSpan.TotalMilliseconds;
            }
        }

        public double Quality
        {
            get
            {
                return (TimeSpan - Waste.Rate.IncreaseByRatio(1 - WasteDelta).ToTimeSpanRate(ActualRate).GetQuantityForTimeSpan(TimeSpan)).TotalMilliseconds / TimeSpan.TotalMilliseconds;
            }
        }

        public double Performance
        {
            get
            {
                double actual = ActualRate.GetQuantityForTimeSpan(TimeSpan);
                double bns = BottleNeckRate.GetQuantityForTimeSpan(TimeSpan);

                return (actual + ((bns - actual) * SpeedDelta)) / bns;
            }
        }

        public double DowntimeUtilization
        {
            get { return Quality * Performance; }
        }

        public double WasteUtilization
        {
            get { return Availibility * Performance; }
        }

        public double SpeedUtilization
        {
            get { return Availibility * Quality; }
        }

        public Dictionary<string, TimeSpan> TimeSpanPareto()
        {
            Dictionary<string, TimeSpan> dictionary = new Dictionary<string, TimeSpan>();

            if (Downtime is DowntimeComposite)
            {
                DowntimeComposite downtimeComposite = (DowntimeComposite)Downtime;

                foreach (var downtime in downtimeComposite.Downtimes)
                {
                    dictionary.Add(downtime.Title, downtime.Rate.GetQuantityForTimeSpan(TimeSpan.FromMilliseconds(DowntimeUtilization * TimeSpan.TotalMilliseconds)));
                }
            }

            if (Waste is WasteComposite)
            {
                WasteComposite wasteComposite = (WasteComposite)Waste;

                foreach (var waste in wasteComposite.Wastes)
                {
                    dictionary.Add(waste.Title, waste.Rate.ToTimeSpanRate(BottleNeckRate).GetQuantityForTimeSpan(TimeSpan.FromMilliseconds(WasteUtilization * TimeSpan.TotalMilliseconds)));
                }
            }

            double d = 1 - (ActualRate.GetQuantityForTimeSpan(TimeSpan) / BottleNeckRate.GetQuantityForTimeSpan(TimeSpan));

            dictionary.Add("Slow Running", TimeSpan.FromMilliseconds(d * SpeedUtilization * TimeSpan.TotalMilliseconds));

            dictionary = dictionary.OrderByDescending(k => k.Value).ToDictionary(k => k.Key, k => k.Value);
            return dictionary;
        }

    }
}
