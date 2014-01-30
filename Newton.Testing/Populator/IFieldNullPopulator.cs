using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Testing
{
    /// <summary>
    /// Populates field as null (default(T)) 
    /// </summary>
    public class RandomNullPopulator : IFieldPopulator
    {
        #region Properties

        private IFieldPopulator fieldPopulator;

        private double chanceOfNull;
        /// <summary>
        /// The chance of a null
        /// </summary>
        public double ChanceOfNull
        {
            get { return chanceOfNull; }
            set { chanceOfNull = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Populates field as null (default(T)) 
        /// </summary>
        public RandomNullPopulator(IFieldPopulator fieldPopulator, double chanceOfNull)
        {
            this.fieldPopulator = fieldPopulator;
            this.chanceOfNull = chanceOfNull;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the field with random data
        /// </summary>
        public object Create(Random random)
        {
            if (chanceOfNull >= random.NextDouble())
                return null;
            return fieldPopulator.Create(random);
        }

        /// <summary>
        /// Check if the field populator works on this type
        /// </summary>
        public bool CheckType(Type type)
        {
            return fieldPopulator.CheckType(type);
        }

        #endregion

    }
}
