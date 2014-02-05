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
    public class KOHtmlBuilder : HtmlBuilder
    {
        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public KOHtmlBuilder(HtmlHelper helper)
            : base(helper)
        { }

        #endregion
    }

    /// <summary>
    /// A builder responsible for creating HTML mark up
    /// </summary>
    public class KOHtmlBuilder<TModel> : HtmlBuilder<TModel>
    {
        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public KOHtmlBuilder(HtmlHelper<TModel> helper)
            : base(helper)
        { }

        #endregion
    }
}