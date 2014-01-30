using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns the potential labour cost saving over a period of time
        /// </summary>
        public static double PotentialSaving(this ILabourPotential labourPotential, Rate wageRate, TimeSpan timeSpan)
        {
            return labourPotential.ActualCost(wageRate, timeSpan)
                - labourPotential.PotentialCost(wageRate, timeSpan);
        }

        /// <summary>
        /// Returns the potential cost of labour of the passed time period
        /// </summary>
        public static double PotentialCost(this ILabourPotential labourPotential, Rate wageRate, TimeSpan timeSpan)
        {
            double costPerPerson = wageRate.GetQuantityForTimeSpan(timeSpan);
            return labourPotential.PotentialLabour * costPerPerson;
        }

        /// <summary>
        /// Returns the actual cost of labour of the passed time period
        /// </summary>
        public static double ActualCost(this ILabourPotential labourPotential, Rate wageRate, TimeSpan timeSpan)
        {
            double costPerPerson = wageRate.GetQuantityForTimeSpan(timeSpan);
            return labourPotential.ActualLabour * costPerPerson;
        }

        public static TimeSpanRate TotalRate(this IEnumerable<IDowntime> downTimes, TimeSpan timeSpan)
        {
            IEnumerable<TimeSpanRate> rates = downTimes.Select(d => d.Rate);

            double totalMilliseconds = rates.Select(r => r.GetQuantityForTimeSpan(timeSpan).TotalMilliseconds).Sum();

            return new TimeSpanRate()
            {
                Quantity = new TimeSpan(
                    (long)(
                        rates.Select(r => r.GetQuantityForTimeSpan(timeSpan)
                            .TotalMilliseconds)
                            .Sum() * 10000

                    )),
                TimeSpan = timeSpan
            };
        }

        public static TimeSpanRate AverageRate(this IEnumerable<IDowntime> downTimes, TimeSpan timeSpan)
        {
            IEnumerable<TimeSpanRate> rates = downTimes.Select(d => d.Rate);

            double totalMilliseconds = rates.Select(r => r.GetQuantityForTimeSpan(timeSpan).TotalMilliseconds).Sum();

            return new TimeSpanRate()
            {
                Quantity = new TimeSpan(
                    (long)(
                        rates.Select(r => r.GetQuantityForTimeSpan(timeSpan)
                            .TotalMilliseconds)
                            .Average() * 10000

                    )),
                TimeSpan = timeSpan
            };
        }

        public static Rate TotalRate(this IEnumerable<IWaste> wastes, TimeSpan timeSpan)
        {
            if (timeSpan.Ticks == 0)
                timeSpan = new TimeSpan(1, 0, 0);

            IEnumerable<Rate> rates = wastes.Select(d => d.Rate);

            return new Rate()
            {
                Quantity = rates.Select(r => r.GetQuantityForTimeSpan(timeSpan)).Sum(),
                TimeSpan = timeSpan
            };
        }

        public static Rate AverageRate(this IEnumerable<IWaste> wastes, TimeSpan timeSpan)
        {
            IEnumerable<Rate> rates = wastes.Select(d => d.Rate);

            return new Rate()
            {
                Quantity = rates.Select(r => r.GetQuantityForTimeSpan(timeSpan)).Average(),
                TimeSpan = timeSpan
            };
        }

        public static Rate ToRate(this TimeSpanRate timeSpanRate, Rate quantityRate)
        {
            //Quantity from the quantity rate for the timespanrate quantity
            //Demoninated by the timespan of the timeSpanRate

            return new Rate()
            {
                Quantity = quantityRate.GetQuantityForTicks(timeSpanRate.QuantityTicks),
                Ticks = timeSpanRate.Ticks
            };
        }

        public static TimeSpanRate ToTimeSpanRate(this Rate rate, Rate quantityRate)
        {
            return new TimeSpanRate()
            {
                Quantity = quantityRate.GetTimeSpanForQuantity(rate.Quantity),
                Ticks = rate.Ticks
            };
        }

        public static Rate ToRate(this ITimeSpanRateMeasure timeSpanRateMeasure, Rate quantityRate)
        {
            return timeSpanRateMeasure.Rate.ToRate(quantityRate);
        }

        public static TimeSpanRate ToTimeSpanRate(this IRateMeasure rateMeasure, Rate quantityRate)
        {
            return rateMeasure.Rate.ToTimeSpanRate(quantityRate);
        }

    }
}
