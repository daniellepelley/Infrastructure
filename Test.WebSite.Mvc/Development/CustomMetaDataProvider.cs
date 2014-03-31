using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newton.Extensions;
using Newton.Validation;

namespace Test.WebSite.Mvc.Development
{
    public class CustomMetaDataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly List<IMetaDataFilter> _filters = new List<IMetaDataFilter>();

        public CustomMetaDataProvider(IMetaDataFilter[] metaDataFilters)
        {
            if (metaDataFilters != null)
                _filters = metaDataFilters.ToList();
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType,
            string propertyName)
        {
            var modelMetaData = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            foreach (var filter in _filters)
            {
                filter.Execute(modelMetaData, attributes);
            }

            return modelMetaData;
        }
    }

    public interface IMetaDataFilter
    {
        void Execute(ModelMetadata modelMetaData, IEnumerable<Attribute> attributes);
    }

    public class AddColonMetaDataFilter : IMetaDataFilter
    {
        public void Execute(ModelMetadata modelMetaData, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(modelMetaData.DisplayName) &&
                !modelMetaData.DisplayName.EndsWith(":"))
            {
                modelMetaData.DisplayName = modelMetaData.DisplayName + ":";
            }
        }
    }

    public class SplitWordsMetaDataFilter : IMetaDataFilter
    {
        public void Execute(ModelMetadata modelMetaData, IEnumerable<Attribute> attributes)
        {
            if (string.IsNullOrEmpty(modelMetaData.DisplayName) &&
                !string.IsNullOrEmpty(modelMetaData.PropertyName))
            {
                var sb = new StringBuilder();
                foreach (var ch in modelMetaData.PropertyName)
                {
                    if (ch.IsUpper())
                    {
                        sb.Append(" ");
                    }
                    sb.Append(ch);
                }

                modelMetaData.DisplayName = sb.ToString();
            }
        }
    }

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