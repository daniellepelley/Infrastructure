using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Interface representing a rate
    /// </summary>
    public interface ITimeRate
    {
        /// <summary>
        /// The timespan expressed as ticks
        /// </summary>
        long Ticks { get; }

        /// <summary>
        /// The timespan for the rate
        /// </summary>
        TimeSpan TimeSpan { get; }
    }

    public interface ITimeRate<T>
    : ITimeRate
    where T : struct
    {
        T Quantity { get; }
    }

    /// <summary>
    /// Class representing a rate of T over a timespan
    /// </summary>
    public abstract class TimeRate<T>
        : ITimeRate,
        IComparable
    {
        #region Properties

        private long ticks;

        public long Ticks
        {
            get { return ticks; }
            set { ticks = value; }
        }

        /// <summary>
        /// The time span
        /// </summary>
        public TimeSpan TimeSpan
        {
            get { return new TimeSpan(ticks); }
            set { ticks = (long)value.TotalMilliseconds * 10000; }
        }

        private T quantity;
        /// <summary>
        /// The quantity that the timespan represents
        /// </summary>
        public virtual T Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        #endregion

        #region Methods

        public virtual int CompareTo(object obj)
        {
            TimeRate<T> otherRate = (TimeRate<T>)obj;
            return Ticks.CompareTo(otherRate.Ticks);
        }

        /// <summary>
        /// Returns the ratio of rate1 compared to rate2
        /// </summary>
        public abstract double GetRatio(TimeRate<T> rate);

        internal static TimeSpan TimeSpanFromMilliseconds(double numberOfMilliseconds)
        {
            return new TimeSpan((long)numberOfMilliseconds * 10000);
        }

        #endregion
    }

    public static class RateExtensions
    {
        public static Rate IncreaseByRatio(this Rate source, double ratio)
        {
            return new Rate()
            {
                TimeSpan = new TimeSpan((long)(source.TimeSpan.TotalMilliseconds * (1 / ratio)) * 10000),
                Quantity = source.Quantity
            };

        }

        public static double GetQuantityForTimeSpan(this Rate source, TimeSpan timeSpan)
        {
            return
                (timeSpan.TotalMilliseconds / source.TimeSpan.TotalMilliseconds)
                * source.Quantity;
        }

        /// <summary>
        /// Gets the quantity for the passed number of ticks
        /// </summary>
        public static double GetQuantityForTicks(this Rate source, long ticks)
        {
            return
                ((double)ticks / (double)source.Ticks)
                * source.Quantity;
        }

        public static TimeSpan GetTimeSpanForQuantity(this Rate source, double quantity)
        {
            return TimeRate<double>.TimeSpanFromMilliseconds(
                source.TimeSpan.TotalMilliseconds *
                (quantity / source.Quantity));
        }

        public static long GetTicksForQuantity(this Rate source, double quantity)
        {
            return source.TimeSpan.Ticks * (long)(quantity / source.Quantity);
        }

        public static TimeSpanRate IncreaseByRatio(this TimeSpanRate source, double ratio)
        {
            return new TimeSpanRate()
            {
                TimeSpan = new TimeSpan((long)(source.TimeSpan.TotalMilliseconds * (1 / ratio)) * 10000),
                Quantity = source.Quantity
            };

        }

        /// <summary>
        /// Returns the quantity of time for a given timespan for the time span rate
        /// </summary>
        public static TimeSpan GetQuantityForTimeSpan(this TimeSpanRate source, TimeSpan timeSpan)
        {
            return
                new TimeSpan(
                    //The ratio between rate timespan and the passed timespan
                    (long)((timeSpan.TotalMilliseconds / source.TimeSpan.TotalMilliseconds)
                    //Multipled by the number of milliseconds scaled up to ticks
                    * source.Quantity.TotalMilliseconds) * (long)10000);
        }

        /// <summary>
        /// Returns the quantity of time for a given timespan for the time span rate
        /// </summary>
        public static TimeSpan GetQuantityForTicks(this TimeSpanRate source, long ticks)
        {
            return 
                new TimeSpan(source.GetTicksForTicks(ticks));
        }

        /// <summary>
        /// Returns the quantity of time for a given timespan for the time span rate
        /// </summary>
        public static long GetTicksForTicks(this TimeSpanRate source, long ticks)
        {
            return (long)(Divide(ticks, source.TimeSpan.Ticks) * source.Quantity.Ticks);


                ////The ratio between rate timespan and the passed timespan
                //    (((ticks / 1000) / (source.TimeSpan.Ticks / 1000))
                ////Multipled by the number of milliseconds scaled up to ticks
                //    * source.Quantity.Ticks);
        }

        public static TimeSpan GetTimeSpanForQuantity(this TimeSpanRate source, TimeSpan quantity)
        {
            return TimeRate<double>.TimeSpanFromMilliseconds(
                source.TimeSpan.TotalMilliseconds *
                (quantity.TotalMilliseconds / source.Quantity.TotalMilliseconds));
        }

        public static double Divide(this long source, long denominator)
        {
            return (double)source / (double)denominator;
        }
    }

    /// <summary>
    /// Representing a quantity per timespan
    /// </summary>
    public class Rate : TimeRate<double>
    {
        #region Properties

        public double NumberOfMillisecondsPerUnit
        {
            get { return TimeSpan.TotalMilliseconds / Quantity; }
        }

        #endregion

        #region Static Methods

        public static double Calculate(TimeRate<double> rate, TimeSpan timeSpan)
        {
            return (timeSpan.TotalMilliseconds / rate.TimeSpan.TotalMilliseconds) * rate.Quantity;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            Rate rate2 = (Rate)obj;
            return rate2.Ticks * (Quantity / Ticks) == rate2.Quantity;
        }

        public override int CompareTo(object obj)
        {
            Rate otherRate = (Rate)obj;

            return otherRate.NumberOfMillisecondsPerUnit.CompareTo(this.NumberOfMillisecondsPerUnit);
        }

        public override double GetRatio(TimeRate<double> rate)
        {
            var otherRate = (Rate)rate;

            return this.NumberOfMillisecondsPerUnit / otherRate.NumberOfMillisecondsPerUnit;
        }

        //public override Rate<double> IncreaseByRatio(double ratio)
        //{
        //    return new Rate()
        //    {
        //        TimeSpan = new TimeSpan((long)(this.TimeSpan.TotalMilliseconds * (1 /ratio)) * 10000),
        //        QuantityPerTimeSpan = this.QuantityPerTimeSpan
        //    };
        //}

        //public override TimeSpan GetTimeSpanForQuantity(double quantity)
        //{
        //    return TimeSpanFromMilliseconds(
        //        this.TimeSpan.TotalMilliseconds *
        //        (quantity / this.QuantityPerTimeSpan));
        //}

        //public override double GetQuantityForTimeSpan(TimeSpan timeSpan)
        //{
        //    return 
        //        (timeSpan.TotalMilliseconds / this.TimeSpan.TotalMilliseconds)
        //            * QuantityPerTimeSpan;
        //}

        //public override Rate<double> GetRateForTimeSpan(TimeSpan timeSpan)
        //{
        //    return new Rate()
        //    {
        //        TimeSpan = timeSpan,
        //        QuantityPerTimeSpan = GetQuantityForTimeSpan(timeSpan)
        //    };
        //}

        #endregion
    }

    /// <summary>
    /// Representing a quantity per timespan
    /// </summary>
    public class TimeSpanRate : TimeRate<TimeSpan>, ITimeRate
    {
        private long quantityTicks;
        /// <summary>
        /// Quantity of time expressed as ticks
        /// </summary>
        public long QuantityTicks
        {
            get { return quantityTicks; }
            set { quantityTicks = value; }
        }

        /// <summary>
        /// Quantity of time expressed as a timespan
        /// </summary>
        public override TimeSpan Quantity
        {
            get { return new TimeSpan(quantityTicks); }
            set { quantityTicks = (long)value.TotalMilliseconds * 10000; }
        }

        #region Methods

        public override bool Equals(object obj)
        {
            TimeSpanRate rate2 = (TimeSpanRate)obj;
            return rate2.Ticks * (Quantity.TotalMilliseconds / Ticks) == rate2.Quantity.TotalMilliseconds;
        }

        public override double GetRatio(TimeRate<TimeSpan> rate)
        {
            return
                (rate.Ticks / this.QuantityTicks)
                /
                (this.Ticks / this.QuantityTicks);
        }

        /// <summary>
        /// The efficency
        /// </summary>
        public double Efficiency
        {
            get { return 1 - (QuantityTicks / Ticks); }
        }

        #endregion
    }
}
