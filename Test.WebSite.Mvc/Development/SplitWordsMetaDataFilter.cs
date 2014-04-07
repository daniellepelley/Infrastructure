using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Newton.Extensions;

namespace Test.WebSite.Mvc.Development
{
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
}