using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanXIaoAPI
{
    interface IprojectData
    {
        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <param name="parentID">-1 创建ID为0的祖宗用户</param>
        /// <param name="name">用户名称</param>
        /// <param name="secret">秘钥。微信登录凭证</param>
        /// <returns></returns>
        DataClass.Person CreatePerson(int parentID,string name, string secret);
        /// <summary>
        /// 查看用户
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        DataClass.Person SearchPerson(int personID);
        /// <summary>
        /// 查看全部用户
        /// </summary>
        /// <returns></returns>
        List<DataClass.Person> GetAllPeople();
        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DataClass.Person EditPersonName(int personID, string name);
        /// <summary>
        /// 修改资金分配比例
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="pocketPrecent"></param>
        /// <returns></returns>
        DataClass.Person EditPersonPocketPrecent(int personID, decimal pocketPrecent);
        /// <summary>
        /// 创建分享链接
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        string CreateShareLink(int personID);
        /// <summary>
        /// 用户存钱
        /// </summary>
        /// <param name="person">用户的全部信息</param>
        /// <param name="moneyIn">存入金额</param>
        /// <returns>返回用户信息</returns>
        DataClass.Person PersonSaveMoney(DataClass.Person person, decimal moneyIn);


        /// <summary>
        /// 款项分配
        /// </summary>
        /// <param name="parentID">上层ID</param>
        /// <param name="moneyLess">资金数额、资金余额：因为这个要递归形式向下传递使用</param>
        /// <param name="accountID">资金来源</param>
        /// <param name="message">传递信息</param>
        /// <returns>字符串反馈结果，我想写：分配给多少个人</returns>
        string MakeMoney(int parentID, decimal moneyLess, int accountID, ref string message);
    }
}
