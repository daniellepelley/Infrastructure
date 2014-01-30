using System;
namespace Newton.Data
{
    /// <summary>
    /// Interface representing a change to a property on an entity
    /// </summary>
    public interface IEntityChangeLogItem
    {
        #region Properties

        /// <summary>
        /// The name of the property
        /// </summary>
        string PropertyName { get; set; }
        
        /// <summary>
        /// The original value of the property
        /// </summary>
        string OriginalValue { get; set; }
        
        /// <summary>
        /// The current value of the property
        /// </summary>
        string CurrentValue { get; set; }

        #endregion
    }
}
