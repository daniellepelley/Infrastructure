using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Objects;
using System.Data.EntityClient;
using System.Data.EntityModel;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Entity Framework change logger
    /// </summary>
    public class EntityFrameworkChangeLogger : IEntityChangeLogger
    {
        #region Properties

        private EntityFrameworkDataContext dataContext;
        /// <summary>
        /// The contained data context
        /// </summary>
        public IDataContext DataContext
        {
            get { return dataContext; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Entity Framework change logger
        /// </summary>
        public EntityFrameworkChangeLogger(ObjectContext dataContext)
        {
            this.dataContext = new EntityFrameworkDataContext(dataContext);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="T">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<T>(T modifiedEntity) where T : class
        {
            return CreateChangeLog(modifiedEntity as object);
        }

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog(object modifiedEntity)
        {
            List<EntityChangeLogItem> logs = new List<EntityChangeLogItem>();

            if (dataContext == null || modifiedEntity == null)
                return logs;

            ObjectStateEntry stateEntryEntity = GetObjectStateEntry(modifiedEntity);

            if (stateEntryEntity != null &&
                !stateEntryEntity.IsRelationship &&
                stateEntryEntity.Entity != null)
            {
                foreach (string modifiedProperty in stateEntryEntity.GetModifiedProperties())
                {
                    EntityChangeLogItem newLog = new EntityChangeLogItem();

                    newLog.PropertyName = modifiedProperty;
                    newLog.CurrentValue = stateEntryEntity.CurrentValues[modifiedProperty].ToString();
                    newLog.OriginalValue = stateEntryEntity.OriginalValues[modifiedProperty].ToString();
                    logs.Add(newLog);
                }
            }

            return logs;
        }

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="T">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<T>(T modifiedEntity, IEnumerable<string> properties)
            where T : class
        {
            return CreateChangeLog(modifiedEntity, properties, true);
        }

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="T">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<T>(T modifiedEntity, IEnumerable<string> properties, bool changesOnly)
            where T : class
        {
            List<EntityChangeLogItem> logs = new List<EntityChangeLogItem>();

            if (dataContext == null || modifiedEntity == null)
                return logs;

            ObjectStateEntry stateEntryEntity = GetObjectStateEntry(modifiedEntity);

            foreach (string property in properties)
            {
                EntityChangeLogItem newLog = new EntityChangeLogItem();
                newLog.PropertyName = property;
                newLog.CurrentValue = stateEntryEntity.CurrentValues[property].ToString();
                newLog.OriginalValue = stateEntryEntity.OriginalValues[property].ToString();

                if (newLog.CurrentValue == newLog.OriginalValue &&
                    changesOnly)
                {
                    continue;
                }

                logs.Add(newLog);
            }

            return logs;
        }

        private ObjectStateEntry GetObjectStateEntry(object modifiedEntity)
        {
            ObjectStateEntry entry = null;
            dataContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(modifiedEntity, out entry);
            return entry;
        }

        #endregion
    }
}
