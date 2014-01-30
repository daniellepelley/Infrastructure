using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets a minimum length for a string.
    /// </summary>
    public class MinimumLengthRule : FieldRule<string>
    {
        #region Parameters

        private int? minLength;
        /// <summary>
        /// Minimum length for the string.
        /// </summary>
        public int? Length
        {
            set { minLength = (value < 0 ? 0 : value); }
            get { return minLength; }
        }

        public override string Text
        {
            get
            {
                return "Must be " + minLength.ToString() + " or more characters in length";
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets a minimum length for a string.
        /// </summary>
        public MinimumLengthRule()
        { }

        /// <summary>
        /// A rule which sets a minimum length for a string.
        /// </summary>
        public MinimumLengthRule(int minLength)
        {
            this.Length = minLength;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value)
                || minLength == null
                || minLength.Value <= 0
                || value.Length >= minLength.Value)
            {
                return string.Empty;
            }
            if (minLength > 0)
                return Text;
            return string.Empty;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumLengthRule)obj).Length <= minLength)
                return 1;
            else
                return -1;
        }

        #endregion
    }
}
