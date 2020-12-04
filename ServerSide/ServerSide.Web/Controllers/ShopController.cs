using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Framework.Models;
using ServerSide.Service;

namespace ServerSide.Web.Controllers
{
    /// <summary>
    /// shop
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IAuthService _authService;

        /// <summary>
        /// shop
        /// </summary>
        /// <param name="shopService"></param>
        /// <param name="authService"></param>
        public ShopController(IShopService shopService, IAuthService authService)
        {
            _shopService = shopService;
            _authService = authService;
        }

        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse<object> Test()
        {
            var res = _shopService.Test();
            return ApiResponse.Success(res);
        }

        /// <summary>
        /// auth
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse<object> Auth()
        {
            var res = _authService.Auth();
            return ApiResponse.Success(res);
        }
    }
}
