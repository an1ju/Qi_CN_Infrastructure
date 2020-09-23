using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChuanXIaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// API运行测试，成功了就有Hello。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string SayHello()
        {
            return "Hello";
        }
    }
}
