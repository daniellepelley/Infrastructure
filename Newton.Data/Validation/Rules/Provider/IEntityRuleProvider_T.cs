using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// An interface representing a rule provider that works with the Mvc framework
    /// </summary>
    public interface IEntityRuleProvider<T>
    {
        #region Properties

        /// <summary>
        /// Rules associated with each field in the entity
        /// </summary>
        Dictionary<string, IRuleCollection> FieldRules { get; }

        /// <summary>
        /// Rules associated with the whole entity
        /// </summary>
        RuleCollection<T> EntityRules { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        void AddRule<TFieldType>(string fieldName, IRule<TFieldType> rule);

        /// <summary>
        /// Validates the entity and returns a dictionary of broken rules
        /// </summary>
        IDictionary<string, IEnumerable<string>> Validate(T model);

        /// <summary>
        /// Cleans the entity and the entity in a cleaned state
        /// </summary>
        T Clean(T model);

        #endregion
    }
}

