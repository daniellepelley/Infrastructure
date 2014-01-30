using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Newton.Dev.Dev
{
    public static class SetExpressions
    {
        public static Action<T, TProperty> SetAction<T, TProperty>(string propertyName)
        {
            var fooParameter = Expression.Parameter(typeof(T));
            var valueParameter = Expression.Parameter(typeof(TProperty));
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var assignment = Expression.Assign(Expression.MakeMemberAccess(fooParameter, propertyInfo), valueParameter);
            var assign = Expression.Lambda<Action<T, TProperty>>(assignment, fooParameter, valueParameter);
            return assign.Compile();
        }

        public static Func<T, TProperty> GetFunc<T, TProperty>(string propertyName)
        {
            var fooParameter = Expression.Parameter(typeof(T));
            var valueParameter = Expression.Parameter(typeof(TProperty));
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var assignment = Expression.Assign(Expression.MakeMemberAccess(fooParameter, propertyInfo), valueParameter);
            var assign = Expression.Lambda<Func<T, TProperty>>(assignment, fooParameter, valueParameter);
            return assign.Compile();
        }
    }
}
