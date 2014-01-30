using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Class representing a change to a property on an entity
    /// </summary>
    public class EntityChangeLogItem : IEntityChangeLogItem
    {
        #region Properties

        private string propertyName;
        /// <summary>
        /// The name of the property
        /// </summary>
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string currentValue;
        /// <summary>
        /// The current value of the property
        /// </summary>
        public string CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        private string originalValue;
        /// <summary>
        /// The original value of the property
        /// </summary>
        public string OriginalValue
        {
            get { return originalValue; }
            set { originalValue = value; }
        }

        #endregion
    }
}
