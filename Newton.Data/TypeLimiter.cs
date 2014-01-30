using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Limits by a set list of types
    /// </summary>
    public class TypeLimiter
    {
        #region Properties

        private List<Type> types = new List<Type>();
        /// <summary>
        /// List of types
        /// </summary>
        public List<Type> Types
        {
            get { return types; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the type is in the set listy
        /// </summary>
        public bool CheckType(object entity)
        {
            return CheckType(entity.GetType());
        }

        /// <summary>
        /// Checks if the type is in the set listy
        /// </summary>
        public bool CheckType<T>()
        {
            return CheckType(typeof(T));
        }

        /// <summary>
        /// Checks if the type is in the set listy
        /// </summary>
        public bool CheckType(Type type)
        {
            foreach (Type t in types)
            {
                if (t == type)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
