using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets the maximum valid date.
    /// </summary>
    public class MaximumDateRule : FieldRule<DateTime?>
    {
        #region Parameters

        private DateTime maxDate = DateTime.Now;
        /// <summary>
        /// Maximum date for the rule.
        /// </summary>
        public DateTime MaxDate
        {
            set { maxDate = value; }
            get { return maxDate; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets the maximum valid date.
        /// </summary>
        public MaximumDateRule()
        { }

        /// <summary>
        /// A rule which sets the maximum valid date.
        /// </summary>
        public MaximumDateRule(DateTime maxDate)
        {
            this.MaxDate = maxDate;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The logic behind the rule.
        /// </summary>
        protected override string LogicMethod(DateTime? value)
        {
            if (value == null)
                return string.Empty;

            value = new DateTime(
                value.Value.Year,
                value.Value.Month,
                value.Value.Day);

            if (value.Value < maxDate)
                return string.Empty;
            return "{0} cannot be later than " + MaxDate.ToString("dd MMM yy");
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MaximumDateRule)obj).MaxDate <= maxDate)
                return 1;
            else
                return -1;
        }

        #endregion
    }
}