using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    public class RuleCollection<T> : IRuleCollection
    {
        private List<IRule<T>> rules = new List<IRule<T>>();
        /// <summary>
        /// Collection of rules
        /// </summary>
        public List<IRule<T>> Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        public void Add(IRule rule)
        {
            if (rule is IRule<T>)
                rules.Add((IRule<T>)rule);
        }

        /// <summary>
        /// Checks the value of the field against the rule and returns a result.
        /// </summary>
        public string[] Check(T value)
        {
            List<string> brokenRules = new List<string>();
            foreach (Rule<T> rule in rules)
            {
                string brokenRule = rule.Logic.Invoke(value);
                if (!string.IsNullOrEmpty(brokenRule))
                {
                    brokenRules.Add(brokenRule);
                }
            }
            return brokenRules.ToArray();
        }

        /// <summary>
        /// Checks the value of the field against the rule and returns a result.
        /// </summary>
        public bool CheckValid(T value)
        {
            return Check(value).Length == 0;
        }

        /// <summary>
        /// Returns a collection of rules as IRule
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRule> GetIRules()
        {
            return rules;
        }
    }
}
