using System;
using System.Web.Mvc;

namespace HtmlHelpers
{
    public interface IHtmlBuilder
    {
        HtmlHelper Helper { get; set; }
        string ToHtmlString();
    }
}
