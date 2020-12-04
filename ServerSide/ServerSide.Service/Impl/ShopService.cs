using ServerSide.Framework.Service;
using ServerSide.Models.Entity;
using ServerSide.Repository;

namespace ServerSide.Service.Impl
{
    public class ShopService : BaseService<ComShop, ITestRepository>, IShopService
    {
        private readonly IAuthRepository _authRepository;

        public ShopService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        public object Test()
        {
            var res = Repository.test();
            return res;
        }
    }
}
