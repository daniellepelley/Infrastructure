using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace HtmlHelpers
{
    public static partial class KnockoutExtensions
    {
        #region Elements

        public static IHtmlString ValidationMessage<TModel, TValue>(this KOHtmlBuilder<TModel> htmlBuilder, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            return new HtmlString(
                string.Format("<span data-bind='visible: {0}.hasError, text: {0}.validationMessage'> </span>",
                    fieldName));
        }

        //public static HtmlStringBuilder TextBoxFor<TModel, TValue>(this KOHtmlBuilder<TModel> htmlBuilder, Expression<Func<TModel, TValue>> expression)
        //{
        //    var fieldName = ExpressionHelper.GetExpressionText(expression);
        //    var fullBindingName = htmlBuilder.Helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
        //    var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

        //    var elementBuilder = new ElementBuilder(htmlBuilder.Helper, "input")
        //        .Bind(a => a.Property(fieldId)
        //         .BindingType("value"));

        //    return new HtmlStringBuilder(elementBuilder);
        //}

        //public static HtmlStringBuilder TextBoxFor(this KOHtmlBuilder htmlBuilder, string property)
        //{
        //    var elementBuilder = new ElementBuilder(htmlBuilder.Helper, "input").Bind(a =>
        //        a.Property(property)
        //            .BindingType("value"));
        //    return new HtmlStringBuilder(elementBuilder);
        //}

        //public static HtmlStringBuilder TextBoxFor<TModel>(this KOHtmlBuilder<TModel> htmlBuilder, string property)
        //{
        //    var elementBuilder = new ElementBuilder(htmlBuilder.Helper, "input").Bind(a =>
        //        a.Property(property)
        //            .BindingType("value"));

        //    return new HtmlStringBuilder(elementBuilder);
        //}

        //public static ElementBuilder TextBoxFor<TModel, TValue>(this DataContext<TModel> dataContext, Expression<Func<TModel, TValue>> expression)
        //{
        //    var fieldName = ExpressionHelper.GetExpressionText(expression);
        //    var fullBindingName = dataContext.Helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
        //    var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

        //    return new ElementBuilder(dataContext.Helper, "input").Bind(a =>
        //        a.Property(fieldId)
        //            .BindingType("value"));
        //}

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
            var setUp = new KOBindAttribute();
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