using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authorize_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChineseController : ControllerBase,InterFace.ILanguages
    {
        // GET: api/<ChineseController>
        [HttpGet]
        public string Get()
        {
            //return "Chinese";
            return GetMyLanguage();
        }

        [HttpGet("Language")]
        public string GetMyLanguage()
        {
            return "中文";
        }
    }
}
