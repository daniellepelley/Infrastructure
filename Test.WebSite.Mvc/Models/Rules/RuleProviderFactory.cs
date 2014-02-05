using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newton.Validation;

namespace Test.WebSite.Mvc
{
    public class RuleProviderFactory : IEntityRuleProviderFactory
    {
        public IEntityRuleProvider<T> Create<T>()
        {
            if (typeof(T) == typeof(TestUser))
            {
                var ruleProvider = new Newton.UI.Mvc.MvcRuleProvider<TestUser>();
                ruleProvider.AddRule<string>("FirstName", new Newton.Validation.MaximumLengthRule(4));
                ruleProvider.AddRule<string>("FirstName", new Newton.Validation.NoSpacesRule());
                ruleProvider.AddRule<string>("FirstName", new Newton.Validation.IsRequiredRule<string>());

                ruleProvider.AddRule<string>("LastName", new Newton.Validation.MaximumLengthRule(6));
                ruleProvider.AddRule<string>("LastName", new Newton.Validation.IsRequiredRule<string>());
                return (IEntityRuleProvider<T>)ruleProvider;
            }
            return null;

        }
    }
}