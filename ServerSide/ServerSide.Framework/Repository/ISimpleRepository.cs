using FreeSql;

namespace ServerSide.Framework.Repository
{
    public interface ISimpleRepository<T> : IBaseRepository<T> where T : class, new()
    {
    }
}
