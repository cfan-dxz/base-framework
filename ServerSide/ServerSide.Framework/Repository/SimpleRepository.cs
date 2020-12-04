using FreeSql;
using ServerSide.Framework.Attributes;
using ServerSide.Framework.Utils;
using System;
using System.Linq;

namespace ServerSide.Framework.Repository
{
    public class SimpleRepository<T> : BaseRepository<T>, ISimpleRepository<T> where T : class, new()
    {
        /// <summary>
        /// 分库模式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static IFreeSql GetFreeSql(string key = null)
        {
            var ib = AutofacHelper.Resolve<IdleBus<IFreeSql>>();
            return ib.Get($"{key}ConnectionString");
        }

        public SimpleRepository(string dbName) : base(GetFreeSql(dbName), null, null)
        {

        }

        public SimpleRepository() : this(null)
        {

        }
    }
}
