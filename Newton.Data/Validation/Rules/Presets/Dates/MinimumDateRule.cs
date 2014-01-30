using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets the minimum valid date.
    /// </summary>
    public class MinimumDateRule : FieldRule<DateTime?>
    {
        #region Parameters

        private DateTime minDate = DateTime.Now;
        /// <summary>
        /// Minimum date for the rule.
        /// </summary>
        public DateTime MinDate
        {
            set { minDate = value; }
            get { return minDate; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets the minimum valid date.
        /// </summary>
        public MinimumDateRule()
        { }

        /// <summary>
        /// A rule which sets the minimum valid date.
        /// </summary>
        public MinimumDateRule(DateTime minDate)
        {
            this.MinDate = minDate;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(DateTime? value)
        {
            if (value == null)
                return string.Empty;
            if (value.Value >= minDate)
                return string.Empty;
            return "{0} cannot be earlier than " + minDate.ToString("dd MMM yy");
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumDateRule)obj).MinDate >= minDate)
                return 1;
            else
                return -1;
        }

        #endregion
    }
}
