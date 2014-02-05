using System;
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
    public class KOViewModelBuilder<TModel> : HtmlBuilderBase
    {
        #region Properties

        private TModel model;

        private string saveUrl;

        #endregion

        #region Constructors

        public KOViewModelBuilder(HtmlHelper helper, TModel model)
            : base(helper)
        {
            this.model = model;
        }

        #endregion

        public KOViewModelBuilder<TModel> Save(string url)
        {
            this.saveUrl = url;
            return this;
        }

        public override string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"var data = {");

            foreach (var property in model.GetType().GetProperties())
            {
                sb.AppendLine(string.Format("{0}: ko.observable('{1}'),", property.Name, property.GetValue(model)));
            }

            //if (!string.IsNullOrEmpty(saveUrl))
            //{
            //    //var jsonData = ko.toJSON(data);
            //    var str = @"Save: function () { alert('Save Method'); alert(data);  $.post('" + saveUrl + "', data); },";
            //    sb.AppendLine(str);
            //}

            sb.AppendLine(@"}");
            sb.AppendLine(@"ko.applyBindings(data);");
            sb.AppendLine(@"</script>");

            return helper.Raw(sb).ToHtmlString();
        }
    }
}