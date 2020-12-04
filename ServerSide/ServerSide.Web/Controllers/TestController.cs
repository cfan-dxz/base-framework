using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Framework.Models;
using ServerSide.Models.Entity;
using ServerSide.Service;

namespace ServerSide.Web.Controllers
{
    /// <summary>
    /// test
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        /// <summary>
        /// test
        /// </summary>
        /// <param name="testService"></param>
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse<string> Do()
        {
            var res = _testService.Do();
            return ApiResponse.Success(res);
        }
    }
}
