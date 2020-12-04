using ServerSide.Framework.Repository;
using ServerSide.Models.Entity;

namespace ServerSide.Repository.Impl
{
    public class TestRepository : SimpleRepository<ComShop>, ITestRepository
    {
        public object test()
        {
            var res = Select.Limit(10).ToList(s => new { s.ShopId, s.ShopName });
            return res;
        }
    }
}
