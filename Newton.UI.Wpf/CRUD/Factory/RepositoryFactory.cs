using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Objects;

using Newton.Data;

namespace Test.Newton.Controls.Wpf
{
    //public class LinqToSQLRepositoryFactory : IRepositoryFactory
    //{
    //    private DataContext dataContext;

    //    public LinqToSQLRepositoryFactory(DataContext dataContext)
    //    {
    //        this.dataContext = dataContext;
    //    }

    //    public IRepository<T> Create<T>() where T : class
    //    {
    //        return new LinqToSQLRepository<T>(dataContext);
    //    }
    //}

    //public class EFRepositoryFactory : IRepositoryFactory
    //{
    //    private ObjectContext objectContext;

    //    public EFRepositoryFactory(ObjectContext objectContext)
    //    {
    //        this.objectContext = objectContext;
    //    }

    //    public IRepository<T> Create<T>() where T : class
    //    {
    //        return new EntityFrameworkRepository<T>(objectContext);
    //    }
    //}
}