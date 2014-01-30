using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// Base class representing a single rule.
    /// </summary>
    public class Rule<T>
    //: NotifyPropertyChanged
    {
        #region Parameters

        private Func<T, string> logic;
        /// <summary>
        /// The logical test which determines if the conditions are met.
        /// </summary>
        public Func<T, string> Logic
        {
            set
            {
                if (value == null)
                    logic = LogicMethod;
                logic = value;
                //OnPropertyChanged("Logic");
            }
            get { return logic; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Base class representing a single rule.
        /// </summary>
        public Rule()
        {
            logic = LogicMethod;
        }

        /// <summary>
        /// Base class representing a single rule.
        /// </summary>
        public Rule(Func<T, string> logic)
            : this()
        {
            this.logic = logic;
        }

        /// <summary>
        /// Base class representing a single rule.
        /// </summary>
        public Rule(Func<T, string> logic, Func<T, T> enforcer)
            : this()
        {
            this.logic = logic;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the rule has been broken.
        /// </summary>
        protected virtual string LogicMethod(T value)
        {
            //Make sure it doesn't call itself
            if (logic == LogicMethod)
                return string.Empty;
            return logic(value);
        }

        #endregion
    }
}