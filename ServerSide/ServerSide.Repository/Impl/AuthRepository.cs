using ServerSide.Framework.Attributes;
using ServerSide.Framework.Repository;
using ServerSide.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Repository.Impl
{
    public class AuthRepository : SimpleRepository<LazadaAuthorize>, IAuthRepository
    {
        public AuthRepository() : base("Token")
        { }
    }
}
