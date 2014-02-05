using System.Linq;
using System.Data.Linq;
using System.Data.Objects;


namespace Newton.Data
{
    /// <summary>
    /// A wrapper around a Linq To SQL data context
    /// </summary>
    public class EntityFrameworkDataContext : IDataContext
    {
        #region Properties

        private ObjectContext objectContext;

        /// <summary>
        /// The contained ObjectContext used to comunicate with the database
        /// </summary>
        public ObjectContext ObjectContext
        {
            get { return objectContext; }
            set { objectContext = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A wrapper around a Linq To SQL data context
        /// </summary>
        public EntityFrameworkDataContext(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        #endregion

        #region Methods

        /// Whether there are changes ready to be persisted to the data source
        /// </summary>
        public bool AreChanges()
        {
            return
                objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Count() > 0 ||
                objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Deleted).Count() > 0 ||
                objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified).Count() > 0;
        }

        /// <summary>
        /// Saves the changes to the data source
        /// </summary>
        public void SaveChanges()
        {
            objectContext.SaveChanges();
        }

        #endregion
    }
}
