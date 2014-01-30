using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets a maximum length for a string.
    /// </summary>
    public class MaximumLengthRule
        : FieldRule<string>,
        IEnforcable<string>
    {
        #region Parameters

        protected int? length;
        /// <summary>
        /// Maximum length for the string.
        /// </summary>
        public int? Length
        {
            set
            {
                length = (value < 0 ? 0 : value);
                //OnPropertyChanged("Length");
            }
            get { return length; }
        }

        public override string Text
        {
            get
            {
                return "Must be " + length.ToString() + " or less characters in length";
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// A rule which sets a maximum length for a string.
        /// </summary>
        public MaximumLengthRule()
        { }

        /// <summary>
        /// A rule which sets a maximum length for a string.
        /// </summary>
        public MaximumLengthRule(int maxLength)
        {
            this.Length = maxLength;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value)
                || length == null
                || length.Value <= 0
                || value.Length <= length.Value)
            {
                return string.Empty;
            }
            if (length > 0)
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
            if (((MaximumLengthRule)obj).Length <= length)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Enforces the rule
        /// </summary>
        public string Enforce(string value)
        {
            if (length.HasValue &&
                value.Length > length)
            {
                return value.Substring(0, length.Value);
            }

            return value;
        }

        #endregion
    }
}