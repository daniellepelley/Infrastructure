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
    /// Base class for an Html Builder
    /// </summary>
    public abstract class HtmlBuilderBase : IHtmlString
    {
        #region Properties

        public List<string> Adds = new List<string>();

        public List<HtmlAttribute> attributes = new List<HtmlAttribute>();
       
        #endregion

        #region Methods

        public abstract string ToHtmlString();

        #endregion
    }
}
