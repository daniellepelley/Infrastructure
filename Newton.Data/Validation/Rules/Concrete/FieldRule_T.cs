using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// Class representing a single validation rule associated with a field.
    /// </summary>
    public class FieldRule<T>
        : Rule<T>,
        IRule<T>,
        IComparable
    {
        #region Properties

        private string text = string.Empty;

        public virtual string Text
        {
            get { return text; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the value of the field against the rule and returns a result.
        /// </summary>
        public string Check(T value)
        {
            return Logic(value);
        }

        ///// <summary>
        ///// Checks the value of the field against the rule and returns a result.
        ///// </summary>
        //public string[] CheckObject(object value)
        //{
        //    if (value is T)
        //    {
        //        string result = Check((T)value);
        //        if (!string.IsNullOrEmpty(result))
        //            return new string[] { result };
        //    }
        //    return new string[0];
        //}

        /// <summary>
        /// Checks the value of the field against the rule and returns a result.
        /// </summary>
        public bool CheckValid(T value)
        {
            return Logic(value) == string.Empty;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a single validation rule associated with a field.
        /// </summary>
        public FieldRule()
            : base() { }

        /// <summary>
        /// Class representing a single validation rule associated with a field.
        /// </summary>
        public FieldRule(Func<T, string> logic)
            : base(logic) { }

        /// <summary>
        /// Class representing a single validation rule associated with a field.
        /// </summary>
        public FieldRule(Func<T, string> logic, string text)
            : base(logic)
        {
            this.text = text;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns
        /// an integer that indicates whether the current instance precedes, follows,
        /// or occurs in the same position in the sort order as the other object.
        /// </summary>
        public virtual int CompareTo(object obj)
        {
            return 0;
        }

        #endregion

        #region Operators

        public static bool operator <(FieldRule<T> field1, FieldRule<T> field2)
        {
            return (field1.CompareTo(field2) == -1);
        }

        public static bool operator >(FieldRule<T> field1, FieldRule<T> field2)
        {
            return (field1.CompareTo(field2) == 1);
        }

        public static bool operator <=(FieldRule<T> field1, FieldRule<T> field2)
        {
            return (field1.CompareTo(field2) != 1);
        }

        public static bool operator >=(FieldRule<T> field1, FieldRule<T> field2)
        {
            return (field1.CompareTo(field2) != -1);
        }

        #endregion
    }
}