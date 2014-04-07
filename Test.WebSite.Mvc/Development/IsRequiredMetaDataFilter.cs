using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newton.Validation;

namespace Test.WebSite.Mvc.Development
{
    public class IsRequiredMetaDataFilter : IMetaDataFilter
    {
        private IEntityRuleProviderFactory _entityRuleProviderFactory;

        public IsRequiredMetaDataFilter(IEntityRuleProviderFactory entityRuleProviderFactory)
        {
            _entityRuleProviderFactory = entityRuleProviderFactory;
        }

        public void Execute(ModelMetadata modelMetaData, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(modelMetaData.PropertyName) &&
                _entityRuleProviderFactory.Create<TestUser>().FieldRules.ContainsKey(modelMetaData.PropertyName))
            {
                modelMetaData.IsRequired = _entityRuleProviderFactory.Create<TestUser>().FieldRules[modelMetaData.PropertyName].GetIRules().Any(r => r is IsRequiredRule<string>);
            }

            

        }
    }
}