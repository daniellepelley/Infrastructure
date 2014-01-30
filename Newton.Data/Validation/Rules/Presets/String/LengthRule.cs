using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets a length for a string.
    /// </summary>
    /// <remarks>
    /// Will still pass if the string is empty.
    /// </remarks>
    public class LengthRule
        : MaximumLengthRule
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a length for a string.
        /// </summary>
        public LengthRule()
        { }

        /// <summary>
        /// A rule which sets a length for a string.
        /// </summary>
        public LengthRule(int length)
        {
            this.length = length;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (length == null)
                return string.Empty;

            if (string.IsNullOrEmpty(value)
                || value.Length == length)
            {
                return string.Empty;
            }
            return "Must be " + length.ToString() + " characters in length";
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MaximumLengthRule)obj).Length <= length)
                return 1;
            else
                return -1;
        }

        #endregion
    }
}