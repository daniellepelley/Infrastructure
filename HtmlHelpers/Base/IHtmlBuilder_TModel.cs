using System;
using System.Web.Mvc;

namespace HtmlHelpers
{
    public interface IHtmlBuilder<TModel>
    {
        HtmlHelper<TModel> Helper { get; set; }
        string ToHtmlString();
    }
}
