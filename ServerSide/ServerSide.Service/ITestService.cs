using ServerSide.Framework.Service;
using ServerSide.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Service
{
    public interface ITestService : IBaseService<ComShop>
    {
        string Do();
    }
}
