using System.Web;
using System.Web.Mvc;
using Newton.UI.Mvc.Controls;

namespace Newton.UI.Mvc
{
    /// <summary>
    /// Collection of Html Helpers
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Gets the Newton Control Factory
        /// </summary>
        public static NewtonControlFactory<TModel> Newton<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new NewtonControlFactory<TModel>(htmlHelper);
        }

        /// <summary>
        /// Gets the Newton Control Factory
        /// </summary>
        public static NewtonControlFactory Newton(this HtmlHelper htmlHelper)
        {
            return new NewtonControlFactory(htmlHelper);   
        }
    }

    public class NewtonControlFactory
    {
        private HtmlHelper htmlHelper;

        public NewtonControlFactory(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public TextBox TextBox()
        {
            return new TextBox(htmlHelper);
        }
    }

    public class NewtonControlFactory<TModel>
    {
        private HtmlHelper<TModel> htmlHelper;

        public NewtonControlFactory(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public TextBoxFor<TModel, TProperty> TextBoxFor<TProperty>(System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
        {
            return new TextBoxFor<TModel, TProperty>(htmlHelper, expression);
        }

        public RadioButtonFor<TModel, TProperty> RadioButtonFor<TProperty>(System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
        {
            return new RadioButtonFor<TModel, TProperty>(htmlHelper, expression);
        }
    }
}

