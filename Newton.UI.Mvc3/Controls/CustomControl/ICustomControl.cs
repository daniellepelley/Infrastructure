using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Newton.UI.Mvc3.Controls
{
    /// <summary>
    /// An interface representing a custom control
    /// </summary>
    public interface ICustomControl
    {
        /// <summary>
        /// Renders the control and returns an HtmlString
        /// </summary>
        /// <returns></returns>
        IHtmlString Render();
    }
}
