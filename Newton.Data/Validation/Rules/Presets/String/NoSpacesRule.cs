using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which doesn't allow spaces in a string.
    /// </summary>
    public class NoSpacesRule
        : FieldRule<string>,
        IEnforcable<string>
    {
        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.Contains(" "))
                return "Must not contain spaces";
            return string.Empty;
        }

        /// <summary>
        /// Removes all spaces from the string
        /// </summary>
        public string Enforce(string value)
        {
            return value.Replace(" ", string.Empty);
        }

        #endregion
    }
}