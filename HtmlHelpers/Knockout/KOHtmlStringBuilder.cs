using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HtmlHelpers
{
    public class KOHtmlStringBuilder : IHtmlString
    {
        internal TagTemplate TagTemplate { get; set; }

        public KOHtmlStringBuilder(TagTemplate tagTemplate)
        {
            TagTemplate = tagTemplate;
        }

        public string ToHtmlString()
        {
            return ElementBuilder.Render(TagTemplate);
        }
    }
}
