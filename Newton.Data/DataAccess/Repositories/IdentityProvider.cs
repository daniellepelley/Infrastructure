using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A class that provide the next Id from a sequence of existing entities
    /// </summary>
    public class IdentityProvider<T>
    {
        #region Properties

        private Func<T, int> getIdFunc;
        /// <summary>
        /// A function that gets an int Id from an entity of type T
        /// </summary>
        public Func<T, int> GetIdFunc
        {
            get { return getIdFunc; }
            set { getIdFunc = value; }
        }

        private Action<T, int> setIdFunc;
        /// <summary>
        /// A function that sets an int Id on an entity of type T
        /// </summary>
        public Action<T, int> SetIdFunc
        {
            get { return setIdFunc; }
            set { setIdFunc = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A class that provide the next Id from a sequence of existing entities
        /// </summary>
        public IdentityProvider()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available identity 
        /// </summary>
        public int GetNextIdentity(IEnumerable<T> entities)
        {
            if (getIdFunc == null)
                return -1;

            return entities.Select(e => getIdFunc(e)).Max() + 1;
        }

        public void SetIdentity(T entity)
        {

        }

        #endregion
    }

    /// <summary>
    /// Sets the identity on an entity
    /// </summary>
    public interface IIdentitySetter<T>
    {
        /// <summary>
        /// Sets the Id on an entity
        /// </summary>
        void SetId(T entity);
    }

    /// <summary>
    /// Sets the identity on an entity
    /// </summary>
    public class IdentitySetter<T> : IIdentitySetter<T>
    {
        #region Properties

        private int seed;
        /// <summary>
        /// The current highest Id
        /// </summary>
        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        private Action<T, int> setIdFunc;
        /// <summary>
        /// A function that sets an int Id on an entity of type T
        /// </summary>
        public Action<T, int> SetIdFunc
        {
            get { return setIdFunc; }
            set { setIdFunc = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Sets the identity on an entity
        /// </summary>
        public IdentitySetter(int seed, Action<T, int> setIdFunc)
        {
            this.seed = seed;
            this.setIdFunc = setIdFunc;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the Id on the passed entity
        /// </summary>
        public void SetId(T entity)
        {
            seed++;
            setIdFunc(entity, seed);
        }

        #endregion
    }

}
