using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.UI.Wpf;

namespace Newton.UI.Wpf
{
    /// <summary>
    /// A factory to create a generic typed View Model
    /// </summary>
    public abstract class ViewModelFactory
    {
        /// <summary>
        /// Creates a generic typed View Model
        /// </summary>
        public abstract IViewModelBase Create<T>()
            where T : class;

        /// <summary>
        /// Creates a generic typed View Model
        /// </summary>
        public virtual IViewModelBase CreateFromType(Type type)
        {
            return (IViewModelBase)Newton.Reflection.Reflection.Invoke(this, "Create", type, null);


            //System.Reflection.MethodInfo method = this.GetType().GetMethods().Where(m => m.IsGenericMethod).FirstOrDefault();
            //System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { type });
            //return (IViewModelBase)genericMethod.Invoke(this, null);
        }
    }
}
