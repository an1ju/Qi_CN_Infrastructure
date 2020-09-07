using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authorize_Practice.Controllers
{
    /// <summary>
    /// 控制器地址，本例子直接使用Test即可。
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly SampleData sampleData;

        /// <summary>
        /// 构造函数，接收注入的数据。
        /// </summary>
        /// <param name="sampleData"></param>
        public TestController(SampleData sampleData)
        {
            this.sampleData = sampleData;
        }

        #region 内部方法

        /// <summary>
        /// Get 某一类或某一个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回值是-1就是没找到</returns>
        private int SearchIndex(int id)
        {
            int r = -1;
            for (int i = 0; i < sampleData.data.Count; i++)
            {
                if (sampleData.data[i].Id == id)
                {
                    r = i;
                    break;
                }
            }

            #region z暂时不用
            //if (r == -1)
            //{
            //    throw new System.Exception("未找到该编号数据");
            //}
            //else
            //{
            //    return r;
            //}
            #endregion


            return r;
        }

        


        #endregion


        #region 标准

        /// <summary>
        /// GetAll
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TestData> Get()
        {
            return sampleData.data;
        }

        /// <summary>
        /// GetByID
        /// 获取指定的数据，查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public TestData Get(int id)
        {
            int index = SearchIndex(id);

            return sampleData.data[index];
        }

        /// <summary>
        /// Add
        /// Post方式增加数据，新增
        /// </summary>
        /// <param name="value">数据结构体</param>
        [HttpPost]
        public void Post([FromBody] TestData value)
        {
            sampleData.data.Add(value);
        }

        /// <summary>
        /// Update
        /// Put方式提交数据，修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value">数据结构体</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TestData value)
        {
            int index = SearchIndex(id);
            sampleData.data[index].Id = value.Id;//此处意义就是，人家要改ID，不能拦着不是。
            sampleData.data[index].Data = value.Data;
        }
        /// <summary>
        /// Delete
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int index = SearchIndex(id);
            sampleData.data.RemoveAt(index);
        }

        
        #endregion



    }

    /// <summary>
    /// 示例数据
    /// </summary>
    public class SampleData
    {
        /// <summary>
        /// 调用结构
        /// </summary>
        public List<TestData> data = new List<TestData>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleData()
        {
            for (int i = 0; i < 10; i++)
            {
                TestData temp = new TestData();
                temp.Id = i + 1;
                temp.Data = string.Format("{0} See id", i + 1);
                data.Add(temp);
            }
        }
    }

    /// <summary>
    /// 测试数据
    /// </summary>
    public class TestData
    {
        private int _id;
        private string _data;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 文本数据(字符串)
        /// </summary>
        public string Data { get => _data; set => _data = value; }
    }
}
