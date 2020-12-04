using ServerSide.Framework.Service;
using ServerSide.Models.Entity;

namespace ServerSide.Service.Impl
{
    public class TestService : BaseService<ComShop>, ITestService
    {
        public string Do()
        {
            var list = Repository.Select.Limit(10).ToList(s => new { s.ShopId, s.ShopName });
            return "ok!";
        }
    }
}
