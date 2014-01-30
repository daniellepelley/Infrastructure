//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Linq.Expressions;
//using Newton.Data;

//namespace Newton.UI.Mvc3
//{
//    /// <summary>
//    /// Class representing a user visible filter
//    /// </summary>
//    public interface IFilter<T>
//    {
//        /// <summary>
//        /// The filter converted into a query
//        /// </summary>
//        IQuery<T> AsQuery();
//    }

//    /// <summary>
//    /// An interpretor which translates a filter into a query
//    /// </summary>
//    public interface IFilterInterpretor<T, TFilter> where TFilter : IFilter<T>
//    {
//        /// <summary>
//        /// Converts the passed filter into an IQuery
//        /// </summary>
//        IQuery<T> Convert(TFilter filter);
//    }

//    public class FilterViewModelBase<T> : IFilter<T>
//    {
//        private IFilterInterpretor<T, FilterViewModelBase<T>> filterInterpretor;

//        #region Constructors

//        public FilterViewModelBase(IFilterInterpretor<T, FilterViewModelBase<T>> filterInterpretor)
//        {
//            this.filterInterpretor = filterInterpretor;
//        }

//        #endregion

//        #region Methods

//        public IQuery<T> AsQuery()
//        {
//            return filterInterpretor.Convert(this);
//        }

//        #endregion
//    }

//    public class FilterManager<T, TFilter> where TFilter : IFilter<T>
//    {
//        public IEnumerable<T> Run<T>(IRepository<T> repository, TFilter filter) where T : class
//        {
//            return repository.Query(filter.AsQuery()).Take(50).ToArray();
//        }

//        public IEnumerable<T> Run<T>(IRepositoryFactory repositoryFactory, IFilter<T> filter) where T : class
//        {
//            return repositoryFactory.Create<T>().Query(filter.AsQuery()).Take(50).ToArray();
//        }

//        //public static Expression<Func<TEntity, bool>> GetPredicate<TEntity, TProperty>(FilterPart<TProperty> filter)
//        //{
//        //    ParameterExpression pe = Expression.Parameter(typeof(TEntity), "i");
//        //    Expression e1 = GetFilterPart<TEntity, TProperty>(filter, pe);
//        //    var lambda = LambdaExpression.Lambda<Func<TEntity, bool>>(e1, new ParameterExpression[] { pe });
//        //    return lambda;
//        //}

//        public static BinaryExpression GetFilterPart<TEntity, TProperty>(IFilterPart filter, ParameterExpression pe, string propertyName)
//        {
//            Type type = filter.GetType().GetGenericArguments()[0];

//            Expression left = Expression.Property(pe, typeof(TEntity).GetProperty(propertyName, type));
//            Expression right = Expression.Constant(filter.GetValue(), type);
//            return Expression.GreaterThanOrEqual(left, right);
//        }
//    }


//    public interface IFilterPart
//    {
//        List<IFilterPart> Children { get; }
//        BinaryExpression AsExpression<TEntity>(string propertyName, ParameterExpression parameterExpression);
//        //object GetValue();
//    }

    

//}
