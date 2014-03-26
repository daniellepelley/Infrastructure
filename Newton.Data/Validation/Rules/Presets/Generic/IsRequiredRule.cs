using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which dictates that a value cannot be null.
    /// </summary>
    public class IsRequiredRule<T> : FieldRule<T>
    {
        private bool isRequired = true;

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }


        public override string Text
        {
            get
            {
                return "Is required";
            }
        }
        #region Constructors

        /// <summary>
        /// A rule which dictates that a value cannot be null.
        /// </summary>
        public IsRequiredRule()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the rule has been broken.
        /// </summary>
        protected override string LogicMethod(T value)
        {
            if (!isRequired)
            {
                return string.Empty;
            }
            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    return Text;
                return string.Empty;
            }
            if (value != null)
            {
                return string.Empty;
            }
            return Text;
        }

        #endregion
    }
}
