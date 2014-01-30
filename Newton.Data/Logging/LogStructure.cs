using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Class representing a custom item on the log
    /// </summary>
    public class LogStructureItem<T>
    {
        #region Properties

        private string name;
        /// <summary>
        /// The name of the field
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Func<T, string> valueSelecter;
        /// <summary>
        /// A value selector to get the value of the field
        /// </summary>
        public Func<T, string> ValueSelecter
        {
            get { return valueSelecter; }
            set { valueSelecter = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a custom item on the log
        /// </summary>
        public LogStructureItem(string name, Func<T, string> valueSelecter)
        {
            this.name = name;
            this.valueSelecter = valueSelecter;
        }

        #endregion
    }

    /// <summary>
    /// Class representing a custom item on the log
    /// </summary>
    public class LogStructure<T>
    {
        #region Properties

        private List<LogStructureItem<T>> items = new List<LogStructureItem<T>>();
        /// <summary>
        /// Collection of items that make up the free text part of a log
        /// </summary>
        public List<LogStructureItem<T>> Items
        {
            get { return items; }
            set { items = value; }
        }

        private string format = "{0}={1}|";
        /// <summary>
        /// The format of the field name {0} and the value {1}
        /// </summary>
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a custom item on the log
        /// </summary>
        public LogStructure()
        { }

        #endregion

        #region Method

        /// <summary>
        /// Builds the message from the source object
        /// </summary>
        public string BuildMessage(T source)
        {
            StringBuilder message = new StringBuilder();

            foreach (var logItem in items)
            {
                message.Append(string.Format(format, logItem.Name, logItem.ValueSelecter(source)));
            }

            return message.ToString();
        }

        #endregion
    }
}
