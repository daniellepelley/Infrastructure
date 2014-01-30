using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which dictates that a value cannot be equal to a collection of other values.
    /// </summary>
    public class IsNotEqualToRule : IsNotEqualToRule<string>
    {
        #region Properties

        private bool isCaseSensative;
        /// <summary>
        /// Whether the rule is case sensative when comparing the value with the other values
        /// </summary>
        public bool IsCaseSensative
        {
            get { return isCaseSensative; }
            set { isCaseSensative = value; }
        }

        #endregion

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
        protected override string LogicMethod(string value)
        {
            Func<string, bool> func = o => false;

            if (!isCaseSensative)
            {
                func = o => !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(o) && o.ToUpper() == value.ToUpper();
            }

            if (OtherValues.Where(func).Count() > 0)
            {
                return Text;
            }
            return string.Empty;
        }

        #endregion
    }
}
