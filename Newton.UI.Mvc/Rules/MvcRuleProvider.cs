using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using Newton.Extensions;
using Newton.Validation;

namespace Newton.UI.Mvc
{
    /// <summary>
    /// A class representing a rule provider that works with the Mvc framework
    /// </summary>
    public class MvcRuleProvider<T>
        : IMvcRuleProvider<T>,
        IEntityRuleProvider<T>
    {
        #region Properties

        private IEntityRuleProvider<T> entityRuleProvider;

        /// <summary>
        /// Rules associated with each field in the entity
        /// </summary>
        public Dictionary<string, IRuleCollection> FieldRules
        {
            get { return entityRuleProvider.FieldRules; }
        }

        /// <summary>
        /// Rules associated with the whole entity
        /// </summary>
        public RuleCollection<T> EntityRules
        {
            get { return entityRuleProvider.EntityRules; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A class representing a rule provider that works with the Mvc framework
        /// </summary>
        public MvcRuleProvider()
            : this(new EntityRuleProvider<T>())
        { }

        /// <summary>
        /// A class representing a rule provider that works with the Mvc framework
        /// </summary>
        public MvcRuleProvider(IEntityRuleProvider<T> entityRuleProvider)
        {
            this.entityRuleProvider = entityRuleProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        public void AddRule<TFieldType>(string fieldName, IRule<TFieldType> rule)
        {
            entityRuleProvider.AddRule<TFieldType>(fieldName, rule);
        }

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        public void AddRule<TFieldType>(Expression<Func<T, TFieldType>> expression, IRule<TFieldType> rule)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            entityRuleProvider.AddRule<TFieldType>(fieldName, rule);
        }

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        public void AddRules<TFieldType>(Expression<Func<T, TFieldType>> expression, params IRule<TFieldType>[] rules)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);

            foreach (var rule in rules)
            {
                entityRuleProvider.AddRule(fieldName, rule);
            }
        }

        /// <summary>
        /// Validates the model and returns an IDictionary of fieldName to broken rules pairs
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Validate(T model)
        {
            return entityRuleProvider.Validate(model);
        }

        public T Clean(T model)
        {
            return model;
        }

        /// <summary>
        /// Validates the model and returns a ModelStateDictionary
        /// </summary>
        public ModelStateDictionary ValidateForMvc(T model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            IDictionary<string, IEnumerable<string>> result = Validate(model);

            foreach (var keyValuePair in result)
            {
                foreach (string brokenRule in keyValuePair.Value)
                {
                    modelState.AddModelError(keyValuePair.Key, brokenRule);
                }
            }
            return modelState;
        }

        /// <summary>
        /// Creates a collection of UnobstrutiveJava html attributes
        /// </summary>
        public IDictionary<string, IDictionary<string, object>> GetUnobstrutiveJava()
        {
            Dictionary<string, IDictionary<string, object>> output = new Dictionary<string, IDictionary<string, object>>();

            foreach (var keyValuePair in FieldRules)
            {
                IDictionary<string, object> htmlAttributes = keyValuePair.Value.GetUnobstrutiveJava();
                output.Add(keyValuePair.Key, htmlAttributes);
            }
            return output;
        }

        #endregion
    }
}