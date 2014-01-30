using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public static class ConstraintCalculations
    {
        public static double CumlativeEfficiency(double efficiency1, double efficiency2, double delta)
        {
            var e1 = efficiency1 * efficiency2;
            var e2 = 1 - ((1 - efficiency1) + (1 - efficiency2));

            var range = e1 - e2;

            return e2 + (range * delta);
        }
    }
}
