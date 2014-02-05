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
    /// A builder responsible for creating HTML mark up
    /// </summary>
    public class HtmlBuilder : HtmlBuilderBase, IHtmlBuilder
    {
        #region Properties

        public HtmlHelper Helper { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public HtmlBuilder(HtmlHelper helper)
        {
            this.Helper = helper;
        }

        #endregion

        #region Methods

        public override string ToHtmlString()
        {
            return string.Empty;
        }

        #endregion
    }


    /// <summary>
    /// A builder responsible for creating HTML mark up
    /// </summary>
    public class HtmlBuilder<TModel> : HtmlBuilderBase, IHtmlBuilder<TModel>
    {
        #region Properties

        public HtmlHelper<TModel> Helper { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public HtmlBuilder(HtmlHelper<TModel> helper)
        {
            this.Helper = helper;
        }

        #endregion

        #region Methods

        public override string ToHtmlString()
        {
            return string.Empty;
        }

        #endregion
    }
}