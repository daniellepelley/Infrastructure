using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public static class Functions
    {
        public static double GetPotentialHours(double actualHours, double bottleNeckSpeed, double potentialSpeed)
        {
            return actualHours * (potentialSpeed / bottleNeckSpeed);
        }

        public static TimeSpan TimeSpanFromHours(double hours)
        {
            int h = (int)hours;

            double hRemainder = (hours - h);

            int m = (int)((hRemainder) * 60);

            double sRemainder = (hRemainder - ((double)m / 60));

            int s = (int)((sRemainder) * 3600);

            return new TimeSpan(h, m, s);
        }
        

        public static double GetOutputFromHours(double hours, Rate rate)
        {
            return rate.GetQuantityForTimeSpan(TimeSpanFromHours(hours));
        }

        public static double GetEfficency(double actualRate, double potentialRate)
        {
            return actualRate / potentialRate;
        }

        //public static double GetOutput(Rate<double> actualRate, Rate<double> potentialRate, TimeSpan downTime, double waste)
        //{
        //    //actualRate.GetRateForTimeSpan


        //}
    }
}
