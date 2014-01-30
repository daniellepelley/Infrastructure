using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class Waste : UnitDimension, IWaste
    {
        private string title;
        /// <summary>
        /// The title of the downtime
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
