using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule matching the field value with a single regex.
    /// </summary>
    public class RegexRule : FieldRule<string>
    {
        #region Parameters

        private string regex;
        /// <summary>
        /// The regex for this item.
        /// </summary>
        public string Regex
        {
            get { return regex; }
            set
            {
                if (regex != value)
                {
                    regex = value;
                    //OnPropertyChanged("Regex");
                }
            }
        }

        private string text = "Must be in a valid format";

        public override string Text
        {
            get { return text; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Field rule matching the field value with a collection of regexes.
        /// </summary>
        public RegexRule()
        { }

        /// <summary>
        /// Field rule matching the field value with a collection of regexes.
        /// </summary>
        public RegexRule(string regex)
        {
            this.regex = regex;
        }

        /// <summary>
        /// Field rule matching the field value with a collection of regexes.
        /// </summary>
        public RegexRule(string regex, string text)
        {
            this.regex = regex;
            this.text = text;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (string.IsNullOrEmpty(regex))
                return string.Empty;

            if (System.Text.RegularExpressions.Regex.Match(value, regex).Value == value)
                return string.Empty;

            return Text;
        }

        #endregion
    }
}
