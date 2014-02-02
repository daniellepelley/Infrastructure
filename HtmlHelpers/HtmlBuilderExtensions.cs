using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
    public static class HtmlBuilderExtensions
    {
        public  static ElementBuilder Ringo(this HtmlHelper helper, System.Web.UI.HtmlTextWriterTag tagName)
        {
            return new ElementBuilder(helper, tagName);
        }

        public static ElementBuilder Ringo(this HtmlHelper helper, string elementName)
        {
            return new ElementBuilder(helper, elementName);
        }

        public static HtmlBuilder<TModel> Ringo<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlBuilder<TModel>(helper);
        }

        public static ElementBuilder Bind(this HtmlHelper helper, string property)
        {
            return new ElementBuilder(helper, "input").KOBind(a =>
                a.Property(property)
                .BindingType("value"));
        }


        public static HtmlBuilder KOValue(this HtmlBuilder builder, string property)
        {
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("value: {0}", property) });
            return builder;
        }

        public static HtmlBuilder<TModel> KOValue<TModel, TValue>(this HtmlBuilder<TModel> builder, Expression<Func<TModel, TValue>> expression)
        {
            var property = ExpressionHelper.GetExpressionText(expression);
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("value: {0}", property) });
            return builder;
        }

        public static HtmlBuilder KOText(this HtmlBuilder builder, string property)
        {
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("text: {0}", property) });
            return builder;
        }

        public static HtmlBuilder<TModel> KOText<TModel, TValue>(this HtmlBuilder<TModel> builder, Expression<Func<TModel, TValue>> expression)
        {
            var property = ExpressionHelper.GetExpressionText(expression);
            builder.attributes.Add(new HtmlAttribute { Name = "data-bind", Value = string.Format("text: {0}", property) });
            return builder;
        }

        public static HtmlBuilder KOBind(this HtmlBuilder builder, Action<KOBindingSetUp> action)
        {
            KOBindingSetUp setUp = new KOBindingSetUp();
            action(setUp);

            builder.Adds.Add(string.Format("{0}='{1}'", setUp.Key(),  setUp.Value()));
                       
            return builder;
        }

        

        public static ElementBuilder KOBind(this ElementBuilder builder, Action<KOBindingSetUp> action)
        {
            KOBindingSetUp setUp = new KOBindingSetUp();
            action(setUp);
            builder.Adds.Add(setUp);
            return builder;
        }

        public static MvcHtmlString CustomHelperFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("type", "text");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            foreach (var key in validationAttributes.Keys)
            {
                tag.Attributes.Add(key, validationAttributes[key].ToString());
            }

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static ElementBuilder Bind<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var value = metadata.Model;

            return new ElementBuilder(helper, "input").KOBind(a =>
                a.Property(fieldId)
                .BindingType("value"));
        }

        public static KOViewModelBuilder<TModel> KOViewModel<TModel>(this HtmlHelper<TModel> helper)
        {
            var model = helper.ViewContext.ViewData.Model;
            return new KOViewModelBuilder<TModel>(helper, (TModel)model);
        }
    }
}