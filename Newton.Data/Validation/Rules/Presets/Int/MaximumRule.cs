using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    ///// <summary>
    ///// A rule which sets a maximum length for a string.
    ///// </summary>
    //public class MaximumRule : FieldRule<int?>
    //{
    //    #region Parameters

    //    private int? maxValue;
    //    /// <summary>
    //    /// Maximum length for the string.
    //    /// </summary>
    //    public int? MaxValue
    //    {
    //        set { maxValue = value; }
    //        get { return maxValue; }
    //    }

    //    #endregion

    //    #region Constructors
    //    /// <summary>
    //    /// A rule which sets a maximum length for a string.
    //    /// </summary>
    //    public MaximumRule()
    //    { }

    //    /// <summary>
    //    /// A rule which sets a maximum length for a string.
    //    /// </summary>
    //    public MaximumRule(int maxValue)
    //    {
    //        this.maxValue = maxValue;
    //    }

    //    #endregion

    //    #region Methods

    //    protected override string LogicMethod(int? value)
    //    {
    //        if (!value.HasValue)
    //            return string.Empty;
    //        if (value.Value <= maxValue)
    //            return string.Empty;
    //        return "{0} cannot be more than " + maxValue.ToString();
    //    }

    //    /// <summary>
    //    /// Compares the current instance with another object of the same type and returns
    //    //  an integer that indicates whether the current instance precedes, follows,
    //    //  or occurs in the same position in the sort order as the other object.
    //    /// </summary>
    //    public override int CompareTo(object obj)
    //    {
    //        if (((MaximumRule)obj).MaxValue <= maxValue)
    //            return 1;
    //        else
    //            return -1;
    //    }

    //    #endregion
    //}
}