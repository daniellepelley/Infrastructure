using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Converts one unit to another
    /// </summary>
    public class UnitConversion
    {
        #region Properties

        /// <summary>
        /// The name of unit 1
        /// </summary>
        public string Unit1 { get; set; }

        /// <summary>
        /// The name of unit 2
        /// </summary>
        public string Unit2 { get; set; }

        /// <summary>
        /// The ratio between unit 1 and unit 2
        /// </summary>
        public double Ratio { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts unit 1 to unit 2
        /// </summary>
        public double ConvertToUnit2(double unit1)
        {
            return unit1* Ratio;
        }

        /// <summary>
        /// Converts unit 2 to unit 1
        /// </summary>
        public double ConvertToUnit1(double unit2)
        {
            return unit2 / Ratio;
        }

        #endregion
    }

    /// <summary>
    /// Converts one unit to another
    /// </summary>
    public class UnitConversionChain
    {
        #region Properties

        /// <summary>
        /// The name of unit 1
        /// </summary>
        public UnitConversion UnitConversion1 { get; set; }

        /// <summary>
        /// The name of unit 2
        /// </summary>
        public UnitConversion UnitConversion2 { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts unit 1 to unit 2
        /// </summary>
        public double ConvertToUnit2(double unit1)
        {
            return unit1 * (UnitConversion1.Ratio * UnitConversion2.Ratio);
        }

        /// <summary>
        /// Converts unit 2 to unit 1
        /// </summary>
        public double ConvertToUnit1(double unit2)
        {
            return unit2 / (UnitConversion1.Ratio * UnitConversion2.Ratio);
        }

        #endregion
    }

    /// <summary>
    /// Interface representing a unit converter
    /// </summary>
    public interface IUnitConverter
    {
        #region Methods

        /// <summary>
        /// Converts unit 1 to unit 2
        /// </summary>
        double ConvertToUnit2(double unit1);

        /// <summary>
        /// Converts unit 2 to unit 1
        /// </summary>
        double ConvertToUnit1(double unit2);
        
        #endregion
    }

}
