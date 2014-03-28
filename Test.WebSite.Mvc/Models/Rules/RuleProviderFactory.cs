using Newton.UI.Mvc;
using Newton.Validation;

namespace Test.WebSite.Mvc
{
    public class RuleProviderFactory : IEntityRuleProviderFactory
    {
        public IEntityRuleProvider<T> Create<T>()
        {
            if (typeof(T) == typeof(TestUser))
            {
                var ruleProvider = new MvcRuleProvider<TestUser>();
                ruleProvider.AddRules(
                    t => t.FirstName,
                    new IsRequiredRule<string>(),
                    new MaximumLengthRule(6),
                    new MinimumLengthRule(2),
                    new NoSpacesRule());

                ruleProvider.AddRules(
                    t => t.LastName,
                    new IsRequiredRule<string>(),
                    new MaximumLengthRule(6),
                    new MinimumLengthRule(2));
                
                return (IEntityRuleProvider<T>)ruleProvider;
            }
            return null;

        }
    }
}