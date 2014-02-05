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
    public class KOHtmlBuilder : HtmlBuilderBase
    {
        #region Properties

        public List<string> Adds = new List<string>();

        public List<HtmlAttribute> attributes = new List<HtmlAttribute>();

        #endregion

        #region Constructors

        /// <summary>
        /// A builder responsible for creating HTML mark up
        /// </summary>
        public KOHtmlBuilder(HtmlHelper helper)
            : base(helper)
        { }

        #endregion

        #region Methods

        public override string ToHtmlString()
        {
            var str = attributes.Select(a => string.Format(@"{0}='{1}'", a.Name, a.Value)).FirstOrDefault();
            str = str + Adds.FirstOrDefault();
            return helper.Raw(str).ToHtmlString();
        }

        #endregion
    }
}
