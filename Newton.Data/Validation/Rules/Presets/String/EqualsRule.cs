using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets what the string should equal.
    /// </summary>
    public class EqualsRule : FieldRule<string>
    {
        #region Parameters

        protected string equalString;
        /// <summary>
        /// The string the value must contain.
        /// </summary>
        public string EqualString
        {
            set
            {
                equalString = value;
                //OnPropertyChanged("EqualString");
            }
            get { return equalString; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets what the string should equal.
        /// </summary>
        public EqualsRule()
        { }

        /// <summary>
        /// A rule which sets what the string should equal.
        /// </summary>
        public EqualsRule(string containsString)
        {
            this.equalString = containsString;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrEmpty(equalString))
            {
                return string.Empty;
            }

            if (value == equalString)
                return "Must equal " + equalString;

            return string.Empty;
        }

        #endregion
    }
}

