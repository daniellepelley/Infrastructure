using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
    public class ElementBuilder : HtmlBuilderBase
    {
        public List<IAttributeBuilder> Adds = new List<IAttributeBuilder>();

        private string elementName;

        public ElementBuilder(HtmlHelper helper, string elementName)
            :base(helper)
        {
            this.elementName = elementName;
        }

        public ElementBuilder(HtmlHelper helper, System.Web.UI.HtmlTextWriterTag tagName)
            :base(helper)
        {
            this.elementName = tagName.ToString().ToLower();
        }

        public override string ToHtmlString()
        {
            TagBuilder tag = new TagBuilder(elementName);

            foreach (var attr in Adds)
                tag.Attributes.Add(attr.Key(), attr.Value());

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing)).ToHtmlString();
        }
    }
}