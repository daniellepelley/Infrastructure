using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which states the date must be in the past.
    /// </summary>
    public class PastRule : FieldRule<DateTime?>
    {
        #region Methods

        /// <summary>
        /// A rule which states the date must be in the past.
        /// </summary>
        protected override string LogicMethod(DateTime? value)
        {
            if (value == null)
                return string.Empty;

            value = new DateTime(
                value.Value.Year,
                value.Value.Month,
                value.Value.Day);

            if (value.Value < DateTime.Now)
                return string.Empty;
            return "{0} must be a past date";
        }

        #endregion
    }
}