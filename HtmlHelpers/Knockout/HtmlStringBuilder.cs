using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HtmlHelpers
{
    public class HtmlStringBuilder : IHtmlString
    {
        internal ElementBuilder ElementBuilder { get; set; }

        public HtmlStringBuilder(ElementBuilder elementBuilder)
        {
            ElementBuilder = elementBuilder;
        }

        public string ToHtmlString()
        {
            return ElementBuilder.ToHtmlString();
        }
    }
}
