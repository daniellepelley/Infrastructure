//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Validation
//{
//    /// <summary>
//    /// Interface representing an item that performs a validation check.
//    /// </summary>
//    public interface IRule
//    {
//        string Text { get; }
//    }

//    /// <summary>
//    /// Interface representing an item that performs a validation check.
//    /// </summary>
//    public interface IRule<T>
//        : IRule
//    {
//        string Check(T value);
//        bool CheckValid(T value);
//    }

//    /// <summary>
//    /// Base class representing a single rule.
//    /// </summary>
//    public class Rule<T>
//    //: NotifyPropertyChanged
//    {
//        #region Parameters

//        private Func<T, string> logic;
//        /// <summary>
//        /// The logical test which determines if the conditions are met.
//        /// </summary>
//        public Func<T, string> Logic
//        {
//            set
//            {
//                if (value == null)
//                    logic = LogicMethod;
//                logic = value;
//                //OnPropertyChanged("Logic");
//            }
//            get { return logic; }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Base class representing a single rule.
//        /// </summary>
//        public Rule()
//        {
//            logic = LogicMethod;
//        }

//        /// <summary>
//        /// Base class representing a single rule.
//        /// </summary>
//        public Rule(Func<T, string> logic)
//            : this()
//        {
//            this.logic = logic;
//        }

//        /// <summary>
//        /// Base class representing a single rule.
//        /// </summary>
//        public Rule(Func<T, string> logic, Func<T, T> enforcer)
//            : this()
//        {
//            this.logic = logic;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Checks if the rule has been broken.
//        /// </summary>
//        protected virtual string LogicMethod(T value)
//        {
//            //Make sure it doesn't call itself
//            if (logic == LogicMethod)
//                return string.Empty;
//            return logic(value);
//        }

//        #endregion
//    }

//    /// <summary>
//    /// Class representing a single validation rule associated with a field.
//    /// </summary>
//    public class FieldRule<T>
//        : Rule<T>,
//        IRule<T>,
//        IComparable
//    {
//        #region Properties

//        private string text = string.Empty;

//        public virtual string Text
//        {
//            get { return text; }
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Checks the value of the field against the rule and returns a result.
//        /// </summary>
//        public string Check(T value)
//        {
//            return Logic(value);
//        }

//        ///// <summary>
//        ///// Checks the value of the field against the rule and returns a result.
//        ///// </summary>
//        //public string[] CheckObject(object value)
//        //{
//        //    if (value is T)
//        //    {
//        //        string result = Check((T)value);
//        //        if (!string.IsNullOrEmpty(result))
//        //            return new string[] { result };
//        //    }
//        //    return new string[0];
//        //}

//        /// <summary>
//        /// Checks the value of the field against the rule and returns a result.
//        /// </summary>
//        public bool CheckValid(T value)
//        {
//            return Logic(value) == string.Empty;
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Class representing a single validation rule associated with a field.
//        /// </summary>
//        public FieldRule()
//            : base() { }

//        /// <summary>
//        /// Class representing a single validation rule associated with a field.
//        /// </summary>
//        public FieldRule(Func<T, string> logic)
//            : base(logic) { }

//        /// <summary>
//        /// Class representing a single validation rule associated with a field.
//        /// </summary>
//        public FieldRule(Func<T, string> logic, string text)
//            : base(logic)
//        {
//            this.text = text;
//        }

//        #endregion

//        #region IComparable Members

//        /// <summary>
//        /// Compares the current instance with another object of the same type and returns
//        /// an integer that indicates whether the current instance precedes, follows,
//        /// or occurs in the same position in the sort order as the other object.
//        /// </summary>
//        public virtual int CompareTo(object obj)
//        {
//            return 0;
//        }

//        #endregion

//        #region Operators

//        public static bool operator <(FieldRule<T> field1, FieldRule<T> field2)
//        {
//            return (field1.CompareTo(field2) == -1);
//        }

//        public static bool operator >(FieldRule<T> field1, FieldRule<T> field2)
//        {
//            return (field1.CompareTo(field2) == 1);
//        }

//        public static bool operator <=(FieldRule<T> field1, FieldRule<T> field2)
//        {
//            return (field1.CompareTo(field2) != 1);
//        }

//        public static bool operator >=(FieldRule<T> field1, FieldRule<T> field2)
//        {
//            return (field1.CompareTo(field2) != -1);
//        }

//        #endregion
//    }

//    public interface IRuleCollection
//    {
//        IEnumerable<IRule> GetIRules();
//        void Add(IRule rule);
//    }


//    public class RuleCollection<T> : IRuleCollection
//    {
//        private List<IRule<T>> rules = new List<IRule<T>>();
//        /// <summary>
//        /// Collection of rules
//        /// </summary>
//        public List<IRule<T>> Rules
//        {
//            get { return rules; }
//            set { rules = value; }
//        }

//        public void Add(IRule rule)
//        {
//            if (rule is IRule<T>)
//                rules.Add((IRule<T>)rule);
//        }

//        /// <summary>
//        /// Checks the value of the field against the rule and returns a result.
//        /// </summary>
//        public string[] Check(T value)
//        {
//            List<string> brokenRules = new List<string>();
//            foreach (Rule<T> rule in rules)
//            {
//                string brokenRule = rule.Logic.Invoke(value);
//                if (!string.IsNullOrEmpty(brokenRule))
//                {
//                    brokenRules.Add(brokenRule);
//                }
//            }
//            return brokenRules.ToArray();
//        }

//        /// <summary>
//        /// Checks the value of the field against the rule and returns a result.
//        /// </summary>
//        public bool CheckValid(T value)
//        {
//            return Check(value).Length == 0;
//        }

//        public IEnumerable<IRule> GetIRules()
//        {
//            return rules;
//        }
//    }



//}
