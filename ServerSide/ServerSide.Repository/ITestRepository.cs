using FreeSql;
using ServerSide.Framework.Repository;
using ServerSide.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Repository
{
    public interface ITestRepository : ISimpleRepository<ComShop>
    {
        object test();
    }
}
