using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which dictates that a value cannot be equal to a collection of other values.
    /// </summary>
    public class IsNotEqualToRule<T> : FieldRule<T>
    {
        private List<T> otherValues = new List<T>();
        /// <summary>
        /// The list of other values
        /// </summary>
        public List<T> OtherValues
        {
            get { return otherValues; }
        }

        public override string Text
        {
            get
            {
                return "Already exists";
            }
        }

        #region Constructors

        /// <summary>
        /// A rule which dictates that a value cannot be null.
        /// </summary>
        public IsNotEqualToRule()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the rule has been broken.
        /// </summary>
        protected override string LogicMethod(T value)
        {
            return otherValues.Contains(value)
                ? Text
                : string.Empty;
        }

        #endregion
    }
}
