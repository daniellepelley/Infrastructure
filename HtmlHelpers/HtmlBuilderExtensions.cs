using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
using HtmlHelpers;
using System.Web.UI;

namespace HtmlHelpers
{
    public static class HtmlBuilderExtensions
    {
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

        public  static ElementBuilder Styx(this HtmlHelper helper, HtmlTextWriterTag tagName)
        {
            return new ElementBuilder(helper, tagName);
        }

        public static ElementBuilder Styx(this HtmlHelper helper, string elementName)
        {
            return new ElementBuilder(helper, elementName);
        }

        public static HtmlBuilder<TModel> Styx<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlBuilder<TModel>(helper);
        }

        //public static MvcHtmlString CustomHelperFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    var fieldName = ExpressionHelper.GetExpressionText(expression);
        //    var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
        //    var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

        //    var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
        //    var value = metadata.Model;

        //    TagBuilder tag = new TagBuilder("input");
        //    tag.Attributes.Add("name", fullBindingName);
        //    tag.Attributes.Add("id", fieldId);
        //    tag.Attributes.Add("type", "text");
        //    tag.Attributes.Add("value", value == null ? "" : value.ToString());

        //    var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
        //    foreach (var key in validationAttributes.Keys)
        //    {
        //        tag.Attributes.Add(key, validationAttributes[key].ToString());
        //    }

        //    return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        //}
    }

    public static class KnockoutExtensions
    {
        #region Elements

        public static ElementBuilder TextBox<TModel, TValue>(this KOHtmlBuilder<TModel> htmlBuilder, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = htmlBuilder.Helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlBuilder.Helper.ViewData);
            var value = metadata.Model;

            return new ElementBuilder(htmlBuilder.Helper, "input").Bind(a =>
                a.Property(fieldId)
                .BindingType("value"));
        }

        public static ElementBuilder TextBox(this KOHtmlBuilder htmlBuilder, string property)
        {
            return new ElementBuilder(htmlBuilder.Helper, "input").Bind(a =>
                a.Property(property)
                .BindingType("value"));
        }

        public static ElementBuilder TextBox<TModel>(this KOHtmlBuilder<TModel> htmlBuilder, string property)
        {
            return new ElementBuilder(htmlBuilder.Helper, "input").Bind(a =>
                a.Property(property)
                .BindingType("value"));
        }


        public static ElementBuilder TextBox<TModel, TValue>(this DataContext<TModel> dataContext, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = dataContext.Helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            //var metadata = ModelMetadata.FromLambdaExpression(expression, dataContext.Helper.ViewData);
            //var value = metadata.Model;

            return new ElementBuilder(dataContext.Helper, "input").Bind(a =>
                a.Property(fieldId)
                .BindingType("value"));
        }

        #endregion

        public static KOHtmlBuilder Bind(this KOHtmlBuilder builder, Action<KOBindAttribute> action)
        {
            KOBindAttribute setUp = new KOBindAttribute();
            action(setUp);

            builder.Adds.Add(string.Format("{0}='{1}'", setUp.Key(), setUp.Value()));

            return builder;
        }

        public static ElementBuilder Bind(this ElementBuilder builder, Action<KOBindAttribute> action)
        {
            KOBindAttribute setUp = new KOBindAttribute();
            action(setUp);
            builder.Adds.Add(setUp);
            return builder;
        }

        public static KOHtmlBuilder Value(this KOHtmlBuilder builder, string property)
        {
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("value: {0}", property) });
            return builder;
        }

        public static KOHtmlBuilder<TModel> Value<TModel, TValue>(this KOHtmlBuilder<TModel> builder, Expression<Func<TModel, TValue>> expression)
        {
            var property = ExpressionHelper.GetExpressionText(expression);
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("value: {0}", property) });
            return builder;
        }

        public static KOHtmlBuilder Text(this KOHtmlBuilder builder, string property)
        {
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("text: {0}", property) });
            return builder;
        }

        public static KOHtmlBuilder<TModel> Text<TModel, TValue>(this KOHtmlBuilder<TModel> builder, Expression<Func<TModel, TValue>> expression)
        {
            var property = ExpressionHelper.GetExpressionText(expression);
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("text: {0}", property) });
            return builder;
        }

        public static KOViewModelBuilder<TModel> ViewModel<TModel>(this KOHtmlBuilder<TModel> htmlBuilder)
        {
            var model = htmlBuilder.Helper.ViewContext.ViewData.Model;
            return new KOViewModelBuilder<TModel>(htmlBuilder.Helper, (TModel)model);
        }

        public static DataContext<TModel> DataContext<TModel>(this KOHtmlBuilder<TModel> builder)
        {
            return new DataContext<TModel>(builder.Helper, new string[0]);
        }

        public static DataContext<TValue> DataContext<TModel, TValue>(this KOHtmlBuilder<TModel> builder, Expression<Func<TModel, TValue>> expression)
        {
            var properties = ExpressionHelper.GetExpressionText(expression).Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            return new DataContext<TValue>(builder.Helper, properties);
        }

    }
}