using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    //To be replaced with forms version.
    public enum CasingType { Normal, Lower, Upper };

    /// <summary>
    /// A rule which sets the casing type for a string.
    /// </summary>
    public class CasingRule
        : FieldRule<string>,
        IEnforcable<string>
    {
        #region Parameters

        private CasingType casingType = CasingType.Normal;
        /// <summary>
        /// Minimum length for the string.
        /// </summary>
        public CasingType CasingType
        {
            set { casingType = value; }
            get { return casingType; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets a minimum length for a string.
        /// </summary>
        public CasingRule()
        { }

        /// <summary>
        /// A rule which sets a minimum length for a string.
        /// </summary>
        public CasingRule(CasingType casingType)
        {
            this.casingType = casingType;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(string value)
        {
            if (value == null) { return string.Empty; }
            if (casingType == CasingType.Lower)
            {
                if (value.ToLower() == value)
                    return string.Empty;
                else
                    return "Must be all lowercase";
            }
            else if (casingType == CasingType.Upper)
            {
                if (value.ToUpper() == value)
                    return string.Empty;
                else
                    return "Must be all uppercase";
            }
            return string.Empty;
        }

        /// <summary>
        /// Changes the casing as required
        /// </summary>
        public string Enforce(string value)
        {
            if (casingType == Validation.CasingType.Lower)
                return value.ToLower();
            else if (casingType == Validation.CasingType.Upper)
                return value.ToUpper();
            return value;
        }

        #endregion
    }
}
