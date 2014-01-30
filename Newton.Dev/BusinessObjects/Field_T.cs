using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    /// <summary>
    /// Class representing a field in a business object.
    /// </summary>
    public abstract class Field
    {
        #region Parameters

        private string name;
        /// <summary>
        /// The name of the field.
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        private string displayName;
        /// <summary>
        /// The name of the field.
        /// </summary>
        public string DisplayName
        {
            set { displayName = value; }
            get
            {
                if (string.IsNullOrEmpty(displayName))
                    return name;
                return displayName;
            }
        }

        /// <summary>
        /// Whether the value of the field conforms to all it's rules.
        /// </summary>
        public bool IsValid
        {
            get { return BrokenRules.Length == 0; }
        }

        public virtual string[] BrokenRules
        {
            get
            {
                return new string[0];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a field in a business object.
        /// </summary>
        protected Field() { }

        #endregion

        #region Method

        /// <summary>
        /// Converts the field into Field<T> if possible, otherwise returns null.
        /// </summary>
        public Field<T> AsField<T>()
        {
            if (this is Field<T>)
                return (Field<T>)this;
            else
                return null;
        }

        /// <summary>
        /// Returns the type of field held by the field.
        /// </summary>
        public virtual Type GetFieldType()
        {
            return null;
        }

        /// <summary>
        /// Enforces the rules on a value (if possible).
        /// </summary>
        public virtual void EnforceRules()
        { }

        public virtual bool Check()
        {
            return true;
        }

        #endregion
    }

    public class Field<T> : Field
    {
        #region Parameters

        private List<string> brokenRules = new List<string>();
        /// <summary>
        /// List of rules that have been broken for this field value.
        /// </summary>
        public override string[] BrokenRules
        {
            get { return brokenRules.ToArray(); }
        }

        private bool? wasValid = null;

        private IFieldValue<T> fieldValue = new FieldValue<T>();
        /// <summary>
        /// An object that holds the value of the field
        /// </summary>
        internal IFieldValue<T> FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        private FieldType<T> fieldType;
        /// <summary>
        /// The FieldType of this field.
        /// </summary>
        /// <remarks>
        /// This holds the rules which check if a field is
        /// valid or not.
        /// </remarks>
        public FieldType<T> FieldType
        {
            set { fieldType = value; }
            get
            {
                if (fieldType == null)
                    return new FieldType<T>();
                return fieldType;
            }
        }

        /// <summary>
        /// Whether the field is valid against the contained rules.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return BrokenRules.Length == 0;
            }
        }

        /// <summary>
        /// Value held by the field.
        /// </summary>
        public T Value
        {
            set { fieldValue.Value = value; }
            get { return fieldValue.Value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name, string displayName)
            : this(name)
        {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name, T value)
            : this(name)
        {
            fieldValue.Value = value;
        }

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name, string displayName, T value)
            : this(name, displayName)
        {
            fieldValue.Value = value;
        }

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name, FieldType<T> fieldType)
            : this(name)
        {
            this.fieldType = fieldType;
        }

        /// <summary>
        /// Class representing a field of type T in a business object.
        /// </summary>
        public Field(string name, string displayName, FieldType<T> fieldType)
            : this(name, displayName)
        {
            this.fieldType = fieldType;
        }

        #endregion

        #region Methods

        ///// <summary>
        ///// Checks the value against the fields rules.
        ///// </summary>
        //public override bool Check()
        //{
        //    return CheckRules(fieldValue.Value);
        //}

        //private void CheckIsValid()
        //{
        //    bool isValid = IsValid;
        //    if (wasValid == null)
        //    {
        //        OnPropertyChanged("IsValid");
        //    }
        //    else if (wasValid.Value && !isValid)
        //    {
        //        OnPropertyChanged("IsValid");
        //    }
        //    else if (!wasValid.Value && isValid)
        //    {
        //        OnPropertyChanged("IsValid");
        //    }
        //    wasValid = isValid;
        //}

        //private bool CheckRules(T value)
        //{
        //    wasValid = brokenRules.Count == 0;

        //    string[] oldBrokenRules = brokenRules.ToArray();
        //    brokenRules.Clear();
        //    brokenRules.AddRange(fieldType.CheckBrokenRules(value, displayName));

        //    if (!oldBrokenRules.AreMatch(brokenRules.ToArray()))
        //    {
        //        OnPropertyChanged("BrokenRules");
        //        CheckIsValid();
        //    }
        //    return brokenRules.Count == 0;
        //}

        ///// <summary>
        ///// Enforces the rules on a value (if possible).
        ///// </summary>
        ///// <remarks>
        ///// Needs improving so that only the 'rule winner' over the various rule sets
        ///// is used to enforce the rule. Multiple rule sets have not been used very often
        ///// so is not yet a problem.
        ///// </remarks>
        //public override void EnforceRules()
        //{
        //    fieldValue.Value = fieldType.EnforceRules(fieldValue.Value);
        //}

        ///// <summary>
        ///// Adds a new rule to the field.
        ///// </summary>
        //public override bool AddRule(IRule rule)
        //{
        //    if (rule != null)
        //    {
        //        if (rule is Rule<T>)
        //            return AddRule((Rule<T>)rule);
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Adds a new rule to the field.
        ///// </summary>
        //public bool AddRule(Rule<T> rule)
        //{
        //    if (FieldType != null && rule != null)
        //        return FieldType.AddRule(rule);
        //    return false;
        //}

        ///// <summary>
        ///// Adds a new rule to the field.
        ///// </summary>
        //public bool AddRule(Func<T, string> logic)
        //{
        //    if (logic != null)
        //        return AddRule(new Rule<T>(logic));
        //    return false;
        //}

        ///// <summary>
        ///// Adds new rules to the field.
        ///// </summary>
        //public bool AddRules(Rule<T>[] rules)
        //{
        //    bool result = true;
        //    foreach (Rule<T> rule in rules)
        //        result = (result && AddRule(rule));
        //    return result;
        //}

        private bool TEquals(T newValue)
        {
            if (fieldValue.Value == null && newValue == null)
                return true;

            if (fieldValue.Value != null && fieldValue.Value.Equals(newValue))
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current System.Object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Field<T>)
            {
                Field<T> field = (Field<T>)obj;
                if (field == null) { return false; }

                return
                    field.Name == this.Name &&
                    TEquals(field.Value);
            }
            return base.Equals(obj);
        }

        public override Type GetFieldType()
        {
            return typeof(T);
        }

        ///// <summary>
        ///// Searches the field and returns the dominant rule.
        ///// </summary>
        //public U RuleWinner<U>() where U : FieldRule<T>
        //{
        //    return this.RuleWinner<T, U>();
        //}

        ///// <summary>
        ///// Searches the field and returns the rules matching the passed type.
        ///// </summary>
        //public U[] FindRules<U>() where U : FieldRule<T>
        //{
        //    return this.FindRules<T, U>();
        //}

        public override string ToString()
        {
            if (this.Value == null)
                return string.Empty;
            return this.Value.ToString();
        }

        #endregion
    }
}
