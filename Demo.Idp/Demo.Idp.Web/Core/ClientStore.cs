using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Idp.Web.Core
{
    public class ClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return await Task.Run(() =>
            {
                #region 用户名密码
                var clients = OAuthConfig.GetClients();
                if (clients.Any(o => o.ClientId == clientId))
                {
                    return clients.FirstOrDefault(o => o.ClientId == clientId);
                }
                #endregion

                #region 通过数据库查询Client 信息
                return GetClient(clientId);
                #endregion
            });
        }

        private Client GetClient(string client)
        {
            //TODO 根据数据库查询
            return null;
        }
    }
}
