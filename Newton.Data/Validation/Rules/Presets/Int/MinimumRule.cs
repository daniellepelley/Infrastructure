using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    ///// <summary>
    ///// A rule which sets a minimum length for a string.
    ///// </summary>
    //public class MinimumRule : FieldRule<int?>
    //{
    //    #region Parameters

    //    private int? minValue;
    //    /// <summary>
    //    /// Minimum length for the string.
    //    /// </summary>
    //    public int? MinValue
    //    {
    //        set { minValue = value; }
    //        get { return minValue; }
    //    }

    //    #endregion

    //    #region Constructors

    //    /// <summary>
    //    /// A rule which sets a minimum length for a string.
    //    /// </summary>
    //    public MinimumRule()
    //    { }

    //    /// <summary>
    //    /// A rule which sets a minimum length for a string.
    //    /// </summary>
    //    public MinimumRule(int minValue)
    //    {
    //        this.minValue = minValue;
    //    }

    //    #endregion

    //    #region Methods

    //    protected override string LogicMethod(int? value)
    //    {
    //        if (!value.HasValue)
    //            return string.Empty;
    //        if (value.Value >= minValue)
    //            return string.Empty;
    //        return "{0} cannot be less than " + minValue.ToString();
    //    }

    //    /// <summary>
    //    /// Compares the current instance with another object of the same type and returns
    //    //  an integer that indicates whether the current instance precedes, follows,
    //    //  or occurs in the same position in the sort order as the other object.
    //    /// </summary>
    //    public override int CompareTo(object obj)
    //    {
    //        if (((MinimumRule)obj).MinValue <= minValue)
    //            return 1;
    //        else
    //            return -1;
    //    }

    //    #endregion
    //}

    /// <summary>
    /// A rule which sets a maximum for an int.
    /// </summary>
    public class MaximumRule : ComparisonRuleBase<int?>
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a maximum for an int.
        /// </summary>
        public MaximumRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1 <= v2, string.Empty)
        { }

        /// <summary>
        /// A rule which sets a maximum for an int.
        /// </summary>
        public MaximumRule(int? minValue)
            : this()
        {
            this.ComparisonValue = minValue;
            this.text = "Cannot be more than " + minValue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumRule)obj).ComparisonValue <= this.ComparisonValue)
                return 1;
            else
                return -1;
        }

        #endregion
    }

    /// <summary>
    /// A rule which sets a minimum for an int.
    /// </summary>
    public class MinimumRule : ComparisonRuleBase<int?>
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a minimum for an int.
        /// </summary>
        public MinimumRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1 >= v2, string.Empty)
        { }

        /// <summary>
        /// A rule which sets a minimum for an int.
        /// </summary>
        public MinimumRule(int? minValue)
            : this()
        {
            this.ComparisonValue = minValue;
            this.text = "Cannot be less than " + minValue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumRule)obj).ComparisonValue <= this.ComparisonValue)
                return 1;
            else
                return -1;
        }

        #endregion
    }

    /// <summary>
    /// A rule which sets a maximum for a byte.
    /// </summary>
    public class MaximumByteRule : ComparisonRuleBase<byte?>
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a maximum for a byte.
        /// </summary>
        public MaximumByteRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1 <= v2, string.Empty)
        { }

        /// <summary>
        /// A rule which sets a maximum for a byte.
        /// </summary>
        public MaximumByteRule(byte? minValue)
            : this()
        {
            this.ComparisonValue = minValue;
            this.text = "Cannot be more than " + minValue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumRule)obj).ComparisonValue <= this.ComparisonValue)
                return 1;
            else
                return -1;
        }

        #endregion
    }

    /// <summary>
    /// A rule which sets a minimum for a byte.
    /// </summary>
    public class MinimumNullableByteRule<T> : ComparisonRuleBase<byte?>
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a minimum for a byte.
        /// </summary>
        public MinimumNullableByteRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1 >= v2, string.Empty)
        { }

        /// <summary>
        /// A rule which sets a minimum for a byte.
        /// </summary>
        public MinimumNullableByteRule(byte minValue)
            : this()
        {
            this.ComparisonValue = minValue;
            this.text = "Cannot be less than " + minValue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumRule)obj).ComparisonValue <= this.ComparisonValue)
                return 1;
            else
                return -1;
        }

        #endregion
    }

    /// <summary>
    /// A rule which sets a minimum for a byte.
    /// </summary>
    public class MinimumByteRule : ComparisonRuleBase<byte>
    {
        #region Constructors

        /// <summary>
        /// A rule which sets a minimum for a byte.
        /// </summary>
        public MinimumByteRule()
            : base((v1, v2) => v1 >= v2, string.Empty)
        { }

        /// <summary>
        /// A rule which sets a minimum for a byte.
        /// </summary>
        public MinimumByteRule(byte minValue)
            : this()
        {
            this.ComparisonValue = minValue;
            this.text = "Cannot be less than " + minValue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MinimumRule)obj).ComparisonValue <= this.ComparisonValue)
                return 1;
            else
                return -1;
        }

        #endregion
    }

    public abstract class ComparisonRuleBase<T> : FieldRule<T>
    {
        #region Parameters

        protected string text;

        public override string Text
        {
            get { return text; }
        }

        private T comparisonValue;
        /// <summary>
        /// Minimum length for the string.
        /// </summary>
        public T ComparisonValue
        {
            set { comparisonValue = value; }
            get { return comparisonValue; }
        }

        private Func<T, T, bool> comparisonFunc;

        public Func<T, T, bool> ComparisonFunc
        {
            get { return comparisonFunc; }
            set { comparisonFunc = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which compares to objects of type T.
        /// </summary>
        public ComparisonRuleBase(Func<T, T, bool> comparisonFunc, string text)
        {
            this.comparisonFunc = comparisonFunc;
            this.text = text;
        }

        /// <summary>
        /// A rule which compares to objects of type T.
        /// </summary>
        public ComparisonRuleBase(T comparisonValue, Func<T, T, bool> comparisonFunc, string text)
            : this(comparisonFunc, text)
        {
            this.comparisonValue = comparisonValue;
        }

        #endregion

        #region Methods

        protected override string LogicMethod(T value)
        {
            if (comparisonFunc != null &&
                comparisonFunc(value, this.comparisonValue))
                return string.Empty;
            return Text;
        }

        ///// <summary>
        ///// Compares the current instance with another object of the same type and returns
        ////  an integer that indicates whether the current instance precedes, follows,
        ////  or occurs in the same position in the sort order as the other object.
        ///// </summary>
        //public override int CompareTo(object obj)
        //{
        //    if (((MinimumRule<T>)obj).MinValue <= minValue
                
        //        )
        //        return 1;
        //    else
        //        return -1;
        //}

        #endregion
    }
}