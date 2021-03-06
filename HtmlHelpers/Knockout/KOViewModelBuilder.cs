﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
  
    /// <summary>
    /// Builds a Knockout viewmodel
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class KOViewModelBuilder2<TModel> : HtmlBuilderBase
    {
        #region Properties

        private TModel model;

        private string saveUrl;

        private HtmlHelper helper;

        #endregion

        #region Constructors

        public KOViewModelBuilder2(HtmlHelper helper, TModel model)
        {
            this.helper = helper;
            this.model = model;
        }

        #endregion

        public override string ToHtmlString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"var data = {");

            foreach (var property in model.GetType().GetProperties())
            {
                sb.AppendLine(string.Format("{0}: ko.observable('{1}'),", property.Name, property.GetValue(model)));
            }

            sb.AppendLine(@"}");
            sb.AppendLine(@"ko.applyBindings(data);");
            sb.AppendLine(@"</script>");

            return helper.Raw(sb).ToHtmlString();
        }
    }

        /// <summary>
    /// Builds a Knockout viewmodel
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class KOViewModelBuilder<TModel> : HtmlBuilderBase
    {
        #region Properties

        private TModel model;

        //private string saveUrl;

        private HtmlHelper helper;

        #endregion

        #region Constructors

        public KOViewModelBuilder(HtmlHelper helper, TModel model)
        {
            this.helper = helper;
            this.model = model;
        }

        #endregion

        //public KOViewModelBuilder<TModel> Save(string url)
        //{
        //    this.saveUrl = url;
        //    return this;
        //}


        private string FromType(Type type)
        {
            List<string> propertyDefinitions = new List<string>();
            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType.IsClass &&
                    property.PropertyType != typeof(string))
                {
                    propertyDefinitions.Add(property.Name + ": { " + FromType(property.PropertyType) + " }");
                }
                else
                {
                    propertyDefinitions.Add(property.Name + ": ko.observable('')");
                }
            }
            return string.Join(",", propertyDefinitions);
        }

        public override string ToHtmlString()
        {
            string text = FromType(model.GetType());
            return helper.Raw(text).ToHtmlString();
        }
    }





}