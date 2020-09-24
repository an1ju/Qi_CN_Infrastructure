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
        private readonly ProjectData project;

        public DataController(ProjectData _project)
        {
            this.project = _project;
        }


        /// <summary>
        /// API运行测试，成功了就有Hello。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string SayHello()
        {
            return "Hello";
        }


        #region 开始啦


        [HttpGet("CreatePerson/{parentID}/{name}")]
        public DataClass.Person CreatePerson(int parentID, string name)
        {
            return project.CreatePerson(parentID, name);
        }

        [HttpGet("SearchPerson/{personID}")]
        public DataClass.Person SearchPerson(int personID)
        {
            return project.SearchPerson(personID);
        }
        [HttpGet("GetAllPeople")]
        public List<DataClass.Person> GetAllPeople()
        {
            return project.GetAllPeople();
        }

        [HttpGet("EditPersonName/{personID}/{name}")]
        public DataClass.Person EditPersonName(int personID, string name)
        {
            return project.EditPersonName(personID, name);
        }
        /// <summary>
        /// 投资
        /// </summary>
        /// <param name="person">用户数据</param>
        /// <param name="moneyOut">投资金额</param>
        /// <returns></returns>
        [HttpPost("SaveMoney/{moneyOut}")]
        public DataClass.Person SaveMoney([FromBody] DataClass.Person person, decimal moneyOut)
        {
            return project.PersonSaveMoney(person, moneyOut);
        }
        /// <summary>
        /// 自动测试01
        /// </summary>
        /// <param name="stepCount">多少个层次</param>
        /// <param name="everyMemberHaveChildCount">每个成员下线数量</param>
        [HttpPost("ChuanXiaoCreate/Test01/{stepCount}/{root_id}/{everyMemberHaveChildCount}")]
        public void ChuanXiaoCreate_Test01(int stepCount, int everyMemberHaveChildCount,int root_id)
        {

            //int root_id = -1;

            if (root_id == -1)
            {
                DataClass.Person temp = project.CreatePerson(root_id, "Main");
                root_id = temp.Id;
                if (stepCount - 1 > 0)
                {
                    ChuanXiaoCreate_Test01(stepCount - 1, everyMemberHaveChildCount, root_id);
                }                
            }
            else
            {
                List<int> rootList = new List<int>();
                for (int i = 0; i < everyMemberHaveChildCount; i++)
                {
                    DataClass.Person temp = project.CreatePerson(root_id, $"Root{root_id}_User{i + 1}_Step{stepCount}");//这里stepCount 数据越大，说明里顶端越近。
                    rootList.Add(temp.Id);
                }
                for (int i = 0; i < rootList.Count; i++)
                {
                    if (stepCount - 1 > 0)
                    {
                        ChuanXiaoCreate_Test01(stepCount - 1, everyMemberHaveChildCount, rootList[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 每名用户进行投资
        /// </summary>
        /// <param name="money"></param>
        [HttpPost("ChuanXiaoSaveMoney/Test02/{money}")]
        public void ChuanXiaoSaveMoney_Test02(decimal money)
        {
            List<DataClass.Person> temp = project.GetAllPeople();
            for (int i = 0; i < temp.Count; i++)
            {
                project.PersonSaveMoney(temp[i], money);
            }
        }

        #endregion
    }
}
