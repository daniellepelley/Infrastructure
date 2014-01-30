using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory.Model.Base
{
    public static class TimeSpanBuilder
    {
        public static TimeSpan GetSecond()
        {
            return new TimeSpan(0, 0, 1);
        }

        public static TimeSpan GetMinute()
        {
            return new TimeSpan(0, 1, 0);
        }

        public static TimeSpan GetHour()
        {
            return new TimeSpan(1, 0, 0);
        }

        public static TimeSpan GetDay()
        {
            return new TimeSpan(1, 0, 0, 0);
        }

        public static TimeSpan GetWeek()
        {
            return new TimeSpan(7, 0, 0, 0);
        }

        public static TimeSpan GetMonth()
        {
            return new TimeSpan(30, 10, 30, 0);
        }

        public static TimeSpan GetYear()
        {
            return new TimeSpan(365, 6, 0, 0);
        }
    }
}
