using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// An interface representing a rule provider that works with the Mvc framework
    /// </summary>
    public class EntityRuleProvider<T> : IEntityRuleProvider<T>
    {
        #region Properties

        private Dictionary<string, IRuleCollection> fieldRules = new Dictionary<string, IRuleCollection>();
        /// <summary>
        /// Rules associated with each field in the entity
        /// </summary>
        public Dictionary<string, IRuleCollection> FieldRules
        {
            get { return fieldRules; }
            set { fieldRules = value; }
        }

        private RuleCollection<T> entityRules = new RuleCollection<T>();
        /// <summary>
        /// Rules associated with the whole entity
        /// </summary>
        public RuleCollection<T> EntityRules
        {
            get { return entityRules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        public void AddRule<TFieldType>(string fieldName, IRule<TFieldType> rule)
        {
            if (!fieldRules.ContainsKey(fieldName))
            {
                IRuleCollection ruleCollection = (IRuleCollection)Activator.CreateInstance(typeof(RuleCollection<>).MakeGenericType(typeof(TFieldType)));
                fieldRules.Add(fieldName, ruleCollection);
            }

            fieldRules[fieldName].Add(rule);
        }

        /// <summary>
        /// Validates the model and returns an IDictionary of fieldName to broken rules pairs
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Validate(T model)
        {
            Dictionary<string, IEnumerable<string>> modelState = new Dictionary<string, IEnumerable<string>>();

            //Check each prpoerty in the model
            foreach (var property in model.GetType().GetProperties())
            {
                //If mapped in the rules dictionary
                if (FieldRules.ContainsKey(property.Name))
                {
                    //Gets a rulecollection type on generic type based on the property type
                    Type type = typeof(RuleCollection<>).MakeGenericType(new Type[] { property.PropertyType });

                    if (FieldRules[property.Name].GetType() == type)
                    {
                        //Invokes 'Check' method and checks the rules
                        string[] brokenRules = (string[])type.InvokeMember(
                            "Check",
                            System.Reflection.BindingFlags.InvokeMethod,
                            null,
                            FieldRules[property.Name],
                            new object[] { property.GetValue(model, null) });

                        //Adds the broken rules to the model state
                        modelState.Add(property.Name, brokenRules);
                    }
                }
            }

            //Adds the broken rules for the entity to the model state
            modelState.Add(string.Empty, EntityRules.Check(model));

            return modelState;
        }

        /// <summary>
        /// Cleans the model using any enforcable rules
        /// </summary>
        /// <remarks>
        /// Required testing
        /// </remarks>
        public T Clean(T model)
        {
            foreach (var property in model.GetType().GetProperties())
            {
                //TODO: Requires testing
                if (FieldRules.ContainsKey(property.Name))
                {
                    var enforcableRules = FieldRules[property.Name].GetIRules().Where(r => r is IEnforcable);

                    foreach (var enforcableRule in enforcableRules)
                    {
                        //Gets a rulecollection type on generic type based on the property type
                        Type type = typeof(IEnforcable<>).MakeGenericType(new Type[] { property.PropertyType });

                        if (FieldRules[property.Name].GetType() == type)
                        {
                            //Invokes 'Check' method and checks the rules
                            property.SetValue(
                                model,
                                (string[])type.InvokeMember(
                                "Enforce",
                                System.Reflection.BindingFlags.InvokeMethod,
                                null,
                                enforcableRule,
                                new object[] { property.GetValue(model, null) }), null);
                        }
                    }
                }
            }

            return model;
        }

        #endregion
    }
}