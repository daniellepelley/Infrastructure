using System.Web;
using System.Web.Mvc;

namespace Newton.UI.Mvc3.Controls.HtmlHelpers
{
    /// <summary>
    /// Collection of Html Helpers
    /// </summary>
    public static partial class HtmlExtensions
    {
        /// <summary>
        /// Displays the passed string as multiple lines
        /// </summary>
        public static IHtmlString DisplayMultilineText(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text) ||
                htmlHelper == null)
            {
                return new HtmlString(string.Empty);
            }

            string myString = text;
            myString = myString.Replace("\n", "<br/>");

            while (myString.Contains("<br/><br/>"))
                myString = myString.Replace("<br/><br/>", "<br/>");

            return new HtmlString(myString);
        }
    }
}

