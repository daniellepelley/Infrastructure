using Newton.Validation;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newton.Validation;

namespace Newton.Extensions
{
    public static class RuleExtensions
    {
        /// <summary>
        /// Creates a set of unobstrusive java html attributes
        /// </summary>
        public static IDictionary<string, object> GetUnobstrutiveJava(this IRuleCollection ruleCollection)
        {
            IDictionary<string, object> htmlAttributes = new Dictionary<string, object>();

            IEnumerable<IRule> rules = ruleCollection.GetIRules();

            foreach (IRule iRule in ruleCollection.GetIRules())
            {
                Type type = iRule.GetType();

                if (type.IsGenericType)
                    type = type.GetGenericTypeDefinition();

                if (type == typeof(IsRequiredRule<>))
                {
                    htmlAttributes.Add("data-val-required", iRule.Text);
                }
                else if (type == typeof(MaximumLengthRule))
                {
                    MaximumLengthRule rule = (MaximumLengthRule)iRule;

                    if (rule.Length.HasValue)
                    {
                        if (!htmlAttributes.ContainsKey("data-val-length"))
                            htmlAttributes.Add("data-val-length", rule.Text);

                        htmlAttributes.Add("data-val-length-max", rule.Length.Value);
                    }
                }
                else if (type == typeof(MinimumLengthRule))
                {
                    MinimumLengthRule rule = (MinimumLengthRule)iRule;

                    if (rule.Length.HasValue)
                    {
                        if (!htmlAttributes.ContainsKey("data-val-length"))
                            htmlAttributes.Add("data-val-length", rule.Text);
                        htmlAttributes.Add("data-val-length-min", rule.Length.Value);
                    }
                }
                else if (type == typeof(RegexRule))
                {
                    RegexRule rule = (RegexRule)iRule;

                    if (!string.IsNullOrEmpty(rule.Regex))
                    {
                        htmlAttributes.Add("data-val-regex", rule.Text);
                        htmlAttributes.Add("data-val-regex-pattern", rule.Regex);
                    }
                }
                else if (type == typeof(MinimumByteRule))
                {
                    MinimumByteRule rule = (MinimumByteRule)iRule;

                    if (!htmlAttributes.ContainsKey("data-val-range"))
                        htmlAttributes.Add("data-val-range", rule.Text);

                    htmlAttributes.Add("data-val-range-min", rule.ComparisonValue);
                }
                else if (type == typeof(MaximumByteRule))
                {
                    MaximumByteRule rule = (MaximumByteRule)iRule;

                    if (!htmlAttributes.ContainsKey("data-val-range"))
                        htmlAttributes.Add("data-val-range", rule.Text);

                    htmlAttributes.Add("data-val-range-max", rule.ComparisonValue);
                }
            }

            if (htmlAttributes.Count > 0)
            {
                htmlAttributes.Add("data-val", "true");
            }

            return htmlAttributes;
        }

        /// <summary>
        /// Validates the model and returns a ModelStateDictionary
        /// </summary>
        public static ModelStateDictionary ValidateForMvc<T>(this IEntityRuleProvider<T> ruleProvider, T model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            IDictionary<string, IEnumerable<string>> result = ruleProvider.Validate(model);

            foreach (var keyValuePair in result)
            {
                foreach (string brokenRule in keyValuePair.Value)
                {
                    modelState.AddModelError(keyValuePair.Key, brokenRule);
                }
            }
            return modelState;
        }
    
    }
}
