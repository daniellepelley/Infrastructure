using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Dev
{
    public static class Functions
    {
        public static double[] ProportionValues(double[] values, double total)
        {
            double ratio = values.Sum() / total;
            return values.Select(v => v * ratio).ToArray();
        }
    }
}
