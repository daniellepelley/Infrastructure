using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
}