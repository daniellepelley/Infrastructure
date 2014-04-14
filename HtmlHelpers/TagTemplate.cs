using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlHelpers
{
    public class TagTemplate
    {
        public string TagName { get; set; }

        public Dictionary<string, string> Attributes = new Dictionary<string, string>();

        public TagTemplate InnerTagTemplate { get; set; }

        public TagTemplate(string tagName)
        {
            TagName = tagName;
        }
    }
}
