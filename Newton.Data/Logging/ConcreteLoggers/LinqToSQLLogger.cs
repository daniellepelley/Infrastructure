using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A logger designed to work with LinqToSQL
    /// </summary>
    public class LinqToSQLChangeLogger : IEntityChangeLogger
    {
        #region Properties

        private LinqToSqlDataContext dataContext;
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
        /// A logger designed to work with LinqToSQL
        /// </summary>
        public LinqToSQLChangeLogger(DataContext dataContext)
        {
            this.dataContext = new LinqToSqlDataContext(dataContext);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="TEntity">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<TEntity>(TEntity modifiedEntity) where TEntity : class
        {
            if (dataContext == null || modifiedEntity == null)
                return new IEntityChangeLogItem[0];

            List<EntityChangeLogItem> logs = new List<EntityChangeLogItem>();

            foreach (ModifiedMemberInfo modifiedProperty in dataContext.DataContext.GetTable<TEntity>().GetModifiedMembers(modifiedEntity))
            {
                EntityChangeLogItem newLog = new EntityChangeLogItem();
                newLog.PropertyName = modifiedProperty.Member.Name;
                newLog.CurrentValue = modifiedProperty.CurrentValue.ToString();
                newLog.OriginalValue = modifiedProperty.OriginalValue.ToString();
                logs.Add(newLog);
            }

            return logs;
        }

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog(object modifiedEntity)
        {
            if (dataContext == null || modifiedEntity == null)
                return new IEntityChangeLogItem[0];

            List<EntityChangeLogItem> logs = new List<EntityChangeLogItem>();

            Type entityType = modifiedEntity.GetType();

            foreach (ModifiedMemberInfo modifiedProperty in dataContext.DataContext.GetTable(entityType).GetModifiedMembers(modifiedEntity))
            {
                EntityChangeLogItem newLog = new EntityChangeLogItem();
                newLog.PropertyName = modifiedProperty.Member.Name;
                newLog.CurrentValue = modifiedProperty.CurrentValue.ToString();
                newLog.OriginalValue = modifiedProperty.OriginalValue.ToString();
                logs.Add(newLog);
            }

            return logs;
        }

                /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="TEntity">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<TEntity>(TEntity modifiedEntity, Dictionary<string, Func<TEntity, string>> keySelectors)
            where TEntity : class
        {
            return CreateChangeLog(modifiedEntity, keySelectors, true);
        }

        /// <summary>
        /// Audits Changes to a Specific Entity
        /// </summary>
        /// <typeparam name="TEntity">The Data Entity Type</typeparam>
        /// <param name="modifiedEntity">The Entity To Audit</param>
        public IEnumerable<IEntityChangeLogItem> CreateChangeLog<TEntity>(TEntity modifiedEntity, Dictionary<string, Func<TEntity, string>> keySelectors, bool changesOnly)
            where TEntity : class
        {
            if (dataContext == null || modifiedEntity == null)
                return new IEntityChangeLogItem[0];

            List<EntityChangeLogItem> logs = new List<EntityChangeLogItem>();

            TEntity originalEntity = dataContext.DataContext.GetTable<TEntity>().GetOriginalEntityState(modifiedEntity);

            foreach (KeyValuePair<string, Func<TEntity, string>> keySelector in keySelectors)
            {
                EntityChangeLogItem newLog = new EntityChangeLogItem();
                newLog.PropertyName = keySelector.Key;
                newLog.CurrentValue = keySelector.Value(modifiedEntity);
                newLog.OriginalValue = keySelector.Value(originalEntity);

                if (newLog.CurrentValue == newLog.OriginalValue)
                    continue;

                logs.Add(newLog);
            }

            return logs;
        }

        #endregion
    }
}
