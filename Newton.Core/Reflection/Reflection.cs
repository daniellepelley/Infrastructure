using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Reflection
{
    public static class Reflection
    {
                /// <summary>
        /// Invokes a generic typed method on the passed object
        /// </summary>
        public static object Invoke(object ob, string methodName, Type genericType, object[] parameters)
        {
            return Invoke(ob, methodName, new Type[] { genericType }, parameters);
        }

        /// <summary>
        /// Invokes a generic typed method on the passed object
        /// </summary>
        public static object Invoke(object ob, string methodName, IEnumerable<Type> genericTypes, object[] parameters)
        {
            lock (ob)
            {
                System.Reflection.MethodInfo method = ob.GetType().GetMethod(methodName);
                System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(genericTypes.ToArray());
                return genericMethod.Invoke(ob, parameters);
            }

        }
    }
}
