using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.OAuth.Web.ProfileService
{
    public class UserProfileService : IProfileService
    {
        /// <summary>
        /// GetProfileDataAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                await Task.Run(() =>
                {
                    var claims = context.Subject.Claims.ToList();
                    context.IssuedClaims = claims.ToList();
                });
            }
            catch { }
        }

        /// <summary>
        /// IsActiveAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.Run(() =>
            {
                context.IsActive = true;
            });
        }
    }
}
