using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    public interface IPublicHolidays
    {
        DateTime[] GetPublicHolidays(int year);
        DateTime[] GetPublicHolidays(int fromYear, int toYear);
    }

}