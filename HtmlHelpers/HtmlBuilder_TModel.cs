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
    public class HtmlBuilder<TModel> : HtmlBuilder
    {
        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public HtmlBuilder(HtmlHelper helper)
            : base(helper)
        {
            this.helper = helper;
        }

        #endregion
    }
}