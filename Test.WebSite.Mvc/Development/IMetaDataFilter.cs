using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Test.WebSite.Mvc.Development
{
    public interface IMetaDataFilter
    {
        void Execute(ModelMetadata modelMetaData, IEnumerable<Attribute> attributes);
    }
}