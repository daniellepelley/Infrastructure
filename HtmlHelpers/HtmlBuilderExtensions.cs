using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
using HtmlHelpers;
using System.Web.UI;

namespace HtmlHelpers
{
    public static class HtmlBuilderExtensions
    {
        public static ModelMetadata ModelMetadataFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
        }

        public static KOHtmlBuilder KO(this WebViewPage page)
        {
            return new KOHtmlBuilder(page.Html);
        }

        public static KOHtmlBuilder<TModel> KO<TModel>(this WebViewPage<TModel> page)
        {
            return new KOHtmlBuilder<TModel>(page.Html);
        }

        public static KOHtmlBuilder KO(this HtmlHelper helper)
        {
            return new KOHtmlBuilder(helper);
        }

        public static KOHtmlBuilder<TModel> KO<TModel>(this HtmlHelper<TModel> helper)
        {
            return new KOHtmlBuilder<TModel>(helper);
        }

        public static ElementBuilder KO(this HtmlHelper helper, HtmlTextWriterTag tagName)
        {
            return new ElementBuilder(helper, tagName);
        }

        //public  static ElementBuilder Styx(this HtmlHelper helper, HtmlTextWriterTag tagName)
        //{
        //    return new ElementBuilder(helper, tagName);
        //}

        //public static ElementBuilder Styx(this HtmlHelper helper, string elementName)
        //{
        //    return new ElementBuilder(helper, elementName);
        //}

        //public static HtmlBuilder<TModel> Styx<TModel>(this HtmlHelper<TModel> helper)
        //{
        //    return new HtmlBuilder<TModel>(helper);
        //}
    }
}