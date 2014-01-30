using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class SalesContrainedOpportuntityCalculator
    {
        private double costPerHour;

        public double Calculate(double savedHours)
        {
            return savedHours * costPerHour;
        }
    }

    public class ProductionContrainedOpportuntityCalculator
    {
        private double unitMarginalPrice;

        public double Calculate(double extraOutput)
        {
            return extraOutput * unitMarginalPrice;
        }
    }
}
