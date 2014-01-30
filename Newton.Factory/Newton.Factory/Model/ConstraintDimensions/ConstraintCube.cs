using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// A cube representing a collection of constrant dimensions
    /// </summary>
    public class ConstraintCube
    {
        #region Properties

        private List<IConstraintDimension> dimensions = new List<IConstraintDimension>();
        /// <summary>
        /// The dimensions of the cube
        /// </summary>
        public List<IConstraintDimension> Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The maximum ultilisation available for a particular dimension
        /// </summary>
        public double MaximumUtilisation(IConstraintDimension dimension, Rate optimumRate)
        {
            return dimensions
                .Where(d => d != dimension)
                .Aggregate((double)1F, (a1, d1) => a1 * Utilisation(d1, optimumRate));
        }


        private double Utilisation(IConstraintDimension dimension, Rate optimumRate)
        {
            return optimumRate.GetQuantityForTicks(100000) / dimension.UnitRate(optimumRate).GetQuantityForTicks(100000);
        }

        #endregion
    }

    /// <summary>
    /// A cube representing a collection of constrant dimensions which act against a measure
    /// </summary>
    public class ConstraintCubeTest
    {
        private Rate optimumRate;

        public Rate OptimumRate
        {
            get { return optimumRate; }
            set { optimumRate = value; }
        }

        private List<IConstraintDimension> dimensions = new List<IConstraintDimension>();
        /// <summary>
        /// The dimensions of the cube
        /// </summary>
        public List<IConstraintDimension> Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public double MaximumUtilisation(IConstraintDimension dimension)
        {
            return dimensions
                .Where(d => d != dimension)
                .Aggregate((double)1F, (a1, d1) => a1 * Efficiency(d1));
        }

        public double Efficiency(IConstraintDimension dimension)
        {
            //var v1 = dimension.UnitRate(optimumRate).GetQuantityForTicks(100000);
            //var v2 = optimumRate.GetQuantityForTicks(100000);


            //var v3 = v1 / v2;
            return 1 - (dimension.UnitRate(optimumRate).GetQuantityForTicks(100000) / optimumRate.GetQuantityForTicks(100000));
        }

        public double Efficiency()
        {
            return dimensions
                .Aggregate((double)1F, (a1, d1) => a1 * Efficiency(d1));
        }
    }
}
