using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using ServerSide.Framework.Repository;
using ServerSide.Framework.Utils;
using System;

namespace ServerSide.Framework.Service
{
    public class BaseService<T, TRepository>
        where T : class, new()
        where TRepository : ISimpleRepository<T>
    {
        public BaseService()
        {
            Repository = AutofacHelper.Resolve<TRepository>();
        }

        public TRepository Repository { get; }
    }

    public class BaseService<T> where T : class, new()
    {
        public BaseService()
        {
            Repository = AutofacHelper.Resolve<ISimpleRepository<T>>();
        }

        public ISimpleRepository<T> Repository { get; }
    }
}
