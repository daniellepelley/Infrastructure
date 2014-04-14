using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
    public class ElementBuilder : HtmlBuilder, IDisposable
    {
        public List<IAttributeBuilder> Adds = new List<IAttributeBuilder>();

        private string elementName;

        public ElementBuilder(HtmlHelper helper, string elementName)
            : base(helper)
        {
            this.elementName = elementName;
        }

        public ElementBuilder(HtmlHelper helper, System.Web.UI.HtmlTextWriterTag tagName)
            : base(helper)
        {
            this.elementName = tagName.ToString().ToLower();
        }

        public static string Render(TagTemplate tagTemplate)
        {
            var stringBuilder = new StringBuilder();

            var tag = new TagBuilder(tagTemplate.TagName);

            foreach (var attr in tagTemplate.Attributes)
                tag.Attributes.Add(attr.Key, attr.Value);

            stringBuilder.Append(tag.ToString(TagRenderMode.SelfClosing));

            return new MvcHtmlString(stringBuilder.ToString()).ToHtmlString();
        }

        public override string ToHtmlString()
        {
            var tag = new TagBuilder(elementName);

            tag.ToString(TagRenderMode.EndTag);

            foreach (var attr in Adds)
                tag.Attributes.Add(attr.Key(), attr.Value());

            return new MvcHtmlString(tag.ToString(TagRenderMode.StartTag)).ToHtmlString();
        }

        public void Dispose()
        {
            TagBuilder tag = new TagBuilder(elementName);
            Helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.EndTag));
        }
    }

    public class WrapperElementBuilder : HtmlBuilder, IDisposable
    {
        public List<IAttributeBuilder> Adds = new List<IAttributeBuilder>();

        private string elementName;

        public WrapperElementBuilder(HtmlHelper helper, string elementName)
            : base(helper)
        {
            this.elementName = elementName;
            Render();
        }

        public WrapperElementBuilder(HtmlHelper helper, string elementName, IDictionary<string, string> dictionary)
            : base(helper)
        {
            this.elementName = elementName;

            foreach (var pair in dictionary)
                attributes.Add(new HtmlAttribute() { Name = pair.Key, Value = pair.Value });

            Render();
        }

        public WrapperElementBuilder(HtmlHelper helper, System.Web.UI.HtmlTextWriterTag tagName)
            : base(helper)
        {
            this.elementName = tagName.ToString().ToLower();
            Render();
        }

        private void Render()
        {
            TagBuilder tag = new TagBuilder(elementName);

            tag.ToString(TagRenderMode.EndTag);

            foreach (var attr in Adds)
                tag.Attributes.Add(attr.Key(), attr.Value());

            foreach (var attr in attributes)
                tag.Attributes.Add(attr.Name, attr.Value);

            Helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            TagBuilder tag = new TagBuilder(elementName);
            Helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.EndTag));
        }
    }
}