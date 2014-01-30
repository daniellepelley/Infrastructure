using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Newton.UI.Mvc3.Controls
{
    public abstract class CustomControl : ICustomControl
    {
        /// <summary>
        /// Renders the controls to Html
        /// </summary>
        public abstract IHtmlString Render();
    }

    //public class Bold : CustomControl
    //{
    //    private HtmlHelper htmlHelper;

    //    private List<string> text = new List<string>();

    //    public Bold(HtmlHelper htmlHelper)
    //    {
    //        this.htmlHelper = htmlHelper;
    //    }

    //    public Bold AddText(string text)
    //    {
    //        this.text.Add(text);
    //        return this;
    //    }

    //    public override HtmlString Render()
    //    {
    //        HtmlTextWriter writer = new HtmlTextWriter(new System.IO.StringWriter());

    //        foreach (string t in text)
    //        {
    //            writer.RenderBeginTag(HtmlTextWriterTag.B);
    //            writer.Write(t);
    //            writer.RenderEndTag();
    //            writer.RenderBeginTag(HtmlTextWriterTag.Br);
    //            writer.RenderEndTag();
    //        }
    //        return new HtmlString(writer.InnerWriter.ToString());
    //    }

    //    //public override string ToString()
    //    //{
    //    //    return Render().ToHtmlString();
    //    //}
    //}
}
