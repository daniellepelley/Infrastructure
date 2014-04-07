using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Test.WebSite.Mvc.Development
{
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
}