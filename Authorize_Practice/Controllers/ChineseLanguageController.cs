using Authorize_Practice.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authorize_Practice.Controllers
{
    /// <summary>
    /// 接口调用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChineseLanguageController : ControllerBase, ITestLanguage
    {
        /// <summary>
        /// 查询我使用的语言
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return MyLanguage();
        }
        /// <summary>
        /// 接口实现
        /// </summary>
        /// <returns></returns>
        public string MyLanguage()
        {
            return "中文";
        }
    }
}
