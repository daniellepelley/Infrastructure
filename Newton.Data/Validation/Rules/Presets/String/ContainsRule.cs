using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which sets what the string should contain.
    /// </summary>
    public class ContainsRule : FieldRule<string>
    {
        #region Parameters

        protected string containsString;
        /// <summary>
        /// The string the value must contain.
        /// </summary>
        public string ContainsString
        {
            set
            {
                containsString = value;
                //OnPropertyChanged("ContainsString");
            }
            get { return containsString; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets what the string should contain.
        /// </summary>
        public ContainsRule()
        { }

        /// <summary>
        /// A rule which sets what the string should contain.
        /// </summary>
        public ContainsRule(string containsString)
        {
            this.containsString = containsString;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrEmpty(containsString))
            {
                return string.Empty;
            }

            if (value.Contains(containsString))
                return "Must contain " + containsString;

            return string.Empty;
        }

        #endregion
    }
}

