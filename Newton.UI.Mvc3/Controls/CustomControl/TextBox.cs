using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Mvc.Html;

using Newton.Extensions;
using Newton.Validation;

namespace Newton.UI.Mvc3.Controls
{
    /// <summary>
    /// A class representing a textbox control
    /// </summary>
    public abstract class InputControl : CustomControl
    {
        #region Properties

        protected string id;

        protected List<string> classes;

        protected string name;

        protected object value;

        protected Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

        protected HtmlHelper htmlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// A class representing a textbox control
        /// </summary>
        public InputControl(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        #endregion

        #region Methods

        #region HtmlAttributes

        /// <summary>
        /// Adds html attributes to the textbox
        /// </summary>
        public InputControl AddHtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            this.htmlAttributes.Merge(htmlAttributes, true);
            return this;
        }

        #endregion

        public InputControl Rules(IRuleCollection ruleCollection)
        {
            this.htmlAttributes.Merge(ruleCollection.GetUnobstrutiveJava(), true);
            return this;
        }

        /// <summary>
        /// Sets the name of the textbox
        /// </summary>
        public InputControl Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Sets the name of the textbox
        /// </summary>
        public InputControl Value(object value)
        {
            this.value = value;
            return this;
        }

        #endregion
    }

    /// <summary>
    /// A class representing a textbox control
    /// </summary>
    public class TextBox : InputControl
    {
        #region Properties

        //private string name;

        //private object value;

        //private Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

        //private HtmlHelper htmlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// A class representing a textbox control
        /// </summary>
        public TextBox(HtmlHelper htmlHelper)
            : base(htmlHelper)
        { }

        #endregion

        #region Methods

        ///// <summary>
        ///// Adds htmnl attributes to the textbox
        ///// </summary>
        //public TextBox AddHtmlAttributes(IDictionary<string, object> htmlAttributes)
        //{
        //    base.AddHtmlAttributes(htmlAttributes);
        //    return this;
        //}

        //public TextBox Rules(IRuleCollection ruleCollection)
        //{
        //    base.Rules(ruleCollection);
        //    return this;
        //}

        ///// <summary>
        ///// Sets the name of the textbox
        ///// </summary>
        //public TextBox Name(string name)
        //{
        //    base.Name(name);
        //    return this;
        //}

        ///// <summary>
        ///// Sets the name of the textbox
        ///// </summary>
        //public TextBox Value(object value)
        //{
        //    base.Value(value);
        //    return this;
        //}

        /// <summary>
        /// Renders the textbox
        /// </summary>
        public override System.Web.IHtmlString Render()
        {
            return System.Web.Mvc.Html.InputExtensions.TextBox(htmlHelper, name, value, htmlAttributes);
        }

        #endregion
    }

    /// <summary>
    /// A class representing a textbox control
    /// </summary>
    public abstract class InputControlFor<TModel, TProperty> : CustomControl
    {
        #region Properties

        protected string name;

        protected string id;

        protected List<string> classes = new List<string>();

        protected object value;

        protected Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

        protected System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression;

        protected HtmlHelper<TModel> htmlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// A class representing a textbox control
        /// </summary>
        public InputControlFor(HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
        {
            this.htmlHelper = htmlHelper;
            this.expression = expression;
        }

        #endregion

        #region Methods

        protected IDictionary<string, object> CompileHtmlAttributes()
        {
            Dictionary<string, object> classDictionary = new Dictionary<string, object>();
            classDictionary.Add(
                "class",
                classes.Aggregate((s1, s2) => string.Format("{0} {1}", s1, s2)));

            return htmlAttributes.Merge(classDictionary);
        }

        /// <summary>
        /// Adds htmnl attributes to the textbox
        /// </summary>
        public InputControlFor<TModel, TProperty> AddHtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            this.htmlAttributes.Merge(htmlAttributes);
            return this;
        }

        #region Classes

        public InputControlFor<TModel, TProperty> AddClass(string className)
        {
            classes.Add(className);
            return this;
        }

        public InputControlFor<TModel, TProperty> AddClass(IEnumerable<string> classNames)
        {
            classes.AddRange(classNames);
            return this;
        }

        #endregion

        public InputControlFor<TModel, TProperty> Id(string id)
        {
            htmlAttributes.Merge("id", id);
            return this;
        }

        public InputControlFor<TModel, TProperty> Expression(System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
        {
            this.expression = expression;
            return this;
        }

        public InputControlFor<TModel, TProperty> Rules(IRuleCollection ruleCollection)
        {
            this.htmlAttributes.Merge(ruleCollection.GetUnobstrutiveJava(), true);
            return this;
        }

        public InputControlFor<TModel, TProperty> Rules(Newton.UI.Mvc3.MvcRuleProvider<TModel> rulesProvider)
        {
            if (string.IsNullOrEmpty(name))
                return this;

            if (rulesProvider.FieldRules.ContainsKey(name))
            {
                return Rules(rulesProvider.FieldRules[name]);
            }
            return this;
        }

        public InputControlFor<TModel, TProperty> Rules(object rules)
        {
            if (rules is IRuleCollection)
                return Rules((IRuleCollection)rules);
            else if (rules is Newton.UI.Mvc3.MvcRuleProvider<TModel>)
                return Rules((Newton.UI.Mvc3.MvcRuleProvider<TModel>)rules);

            return this;
        }

        /// <summary>
        /// Sets the name of the textbox
        /// </summary>
        public InputControlFor<TModel, TProperty> Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Sets the name of the textbox
        /// </summary>
        public InputControlFor<TModel, TProperty> Value(object value)
        {
            this.value = value;
            return this;
        }

        #endregion
    }

    /// <summary>
    /// A class representing a textbox control
    /// </summary>
    public class TextBoxFor<TModel, TProperty> : InputControlFor<TModel, TProperty>
    {
        #region Constructors

        /// <summary>
        /// A class representing a textbox control
        /// </summary>
        public TextBoxFor(HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
            : base(htmlHelper, expression)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the textbox
        /// </summary>
        public override System.Web.IHtmlString Render()
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(htmlHelper, expression, this.CompileHtmlAttributes());
        }

        #endregion
    }

    /// <summary>
    /// A class representing a textbox control
    /// </summary>
    public class RadioButtonFor<TModel, TProperty> : InputControlFor<TModel, TProperty>
    {
        #region Constructors

        /// <summary>
        /// A class representing a textbox control
        /// </summary>
        public RadioButtonFor(HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
            : base(htmlHelper, expression)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the textbox
        /// </summary>
        public override System.Web.IHtmlString Render()
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButtonFor<TModel, TProperty>(htmlHelper, expression, this.CompileHtmlAttributes());
        }

        #endregion
    }


    ///// <summary>
    ///// A class representing a textbox control
    ///// </summary>
    //public class TextBoxFor<TModel, TProperty> : CustomControl
    //{
    //    #region Properties

    //    private string name;

    //    private object value;

    //    private Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

    //    private System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression;

    //    private HtmlHelper<TModel> htmlHelper;

    //    #endregion

    //    #region Constructors

    //    /// <summary>
    //    /// A class representing a textbox control
    //    /// </summary>
    //    public TextBoxFor(HtmlHelper<TModel> htmlHelper)
    //    {
    //        this.htmlHelper = htmlHelper;
    //    }

    //    #endregion

    //    #region Methods

    //    /// <summary>
    //    /// Adds htmnl attributes to the textbox
    //    /// </summary>
    //    public TextBoxFor<TModel, TProperty> AddHtmlAttributes(IDictionary<string, object> htmlAttributes)
    //    {
    //        this.htmlAttributes.Merge(htmlAttributes, true);
    //        return this;
    //    }

    //    public TextBoxFor<TModel, TProperty> Expression(System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression)
    //    {
    //        this.expression = expression;
    //        return this;
    //    }

    //    public TextBoxFor<TModel, TProperty> Rules(IRuleCollection ruleCollection)
    //    {
    //        this.htmlAttributes.Merge(ruleCollection.GetUnobstrutiveJava(), true);
    //        return this;
    //    }

    //    public TextBoxFor<TModel, TProperty> Rules(Newton.UI.Mvc3.MvcRuleProvider<TModel> rulesProvider)
    //    {
    //        if (rulesProvider.FieldRules.ContainsKey(name))
    //        {
    //            return Rules(rulesProvider.FieldRules[name]);
    //        }
    //        return this;
    //    }

    //    public TextBoxFor<TModel, TProperty> Rules(object rules)
    //    {
    //        if (rules is IRuleCollection)
    //            return Rules((IRuleCollection)rules);
    //        else if (rules is Newton.UI.Mvc3.MvcRuleProvider<TModel>)
    //            return Rules((Newton.UI.Mvc3.MvcRuleProvider<TModel>)rules);

    //        return this;
    //    }

    //    /// <summary>
    //    /// Sets the name of the textbox
    //    /// </summary>
    //    public TextBoxFor<TModel, TProperty> Name(string name)
    //    {
    //        this.name = name;
    //        return this;
    //    }

    //    /// <summary>
    //    /// Sets the name of the textbox
    //    /// </summary>
    //    public TextBoxFor<TModel, TProperty> Value(object value)
    //    {
    //        this.value = value;
    //        return this;
    //    }

    //    /// <summary>
    //    /// Renders the textbox
    //    /// </summary>
    //    public override System.Web.HtmlString Render()
    //    {
    //        return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(htmlHelper, expression, htmlAttributes);
    //    }

    //    #endregion
    //}
}
