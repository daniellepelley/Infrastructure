using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    public class BusinessObjectsContext : IDisposable
    {
        private List<IBusinessEntity> businessEntities = new List<IBusinessEntity>();

        #region Methods

        /// <summary>
        /// Attaches a business object to the context
        /// </summary>
        public void Attach(IBusinessEntity businessEntity)
        {
            businessEntity.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(businessEntity_PropertyChanged);
        }

        /// <summary>
        /// Detaches a business object from the context
        /// </summary>
        public void Detach(IBusinessEntity businessEntity)
        {
            businessEntity.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(businessEntity_PropertyChanged);
            businessEntities.Remove(businessEntity);
        }

        #endregion

        void businessEntity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
               
        public void Dispose()
        {
            foreach (var businessEntity in businessEntities)
                businessEntity.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(businessEntity_PropertyChanged);
        }
    }
}
