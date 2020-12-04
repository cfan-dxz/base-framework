using ServerSide.Framework.Service;
using ServerSide.Models.Entity;
using ServerSide.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Service.Impl
{
    public class AuthService : BaseService<LazadaAuthorize, IAuthRepository>, IAuthService
    {
        public object Auth()
        {
            var res = Repository.Select.Limit(10).ToList();
            return res;
        }
    }
}
