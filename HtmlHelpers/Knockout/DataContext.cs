using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
    public class DataContext<TModel> : HtmlBuilder<TModel>, IDisposable
    {
        private List<string> properties = new List<string>();

        public DataContext(HtmlHelper<TModel> helper, IEnumerable<string> properties)
            : base(helper)
        {
            this.properties.AddRange(properties);
            Render();
        }

        private void Render()
        {
            TagBuilder tag = new TagBuilder("div");

            string value = "with: $root.data";

            if (properties.Count > 0)
                value = string.Format("with: $root.data.{0}", string.Join(".", properties));

            tag.Attributes.Add("data-bind", value);

            Helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            TagBuilder tag = new TagBuilder("div");
            Helper.ViewContext.Writer.Write(tag.ToString(TagRenderMode.EndTag));
        }
    }
}
