using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanXIaoAPI.Tools
{
    public class ToolsForProject
    {
        #region For Person
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="name"></param>
        /// <param name="nowCount">当前List的Count，为了编写ID</param>
        /// <returns></returns>
        public static DataClass.Person MakePerson(int parentID, string name,int nowCount)
        {
            DataClass.Person temp = new DataClass.Person();
            temp.Id = nowCount;
            temp.Name = name;
            temp.MoneyIn = 0;
            temp.MoneyOut = 0;
            temp.PocketPrecent = ProjectData.FEN_PEI_PRECENT;//暂时留个默认值
            temp.ParentID = parentID;

            return temp;
        }

        public static bool CheckParentId_Exist_In_Person(List<DataClass.Person> PeopleList,int parentID)
        {
            bool r = false;
            var test = PeopleList.Where(o => o.Id == parentID).ToList();
            if (test.Count > 0)
            {
                //证明父节存在
                r = true;
            }
            else
            {
                //父节点不存在。
                r = false;                
            }
            return r;
        }

        public static DataClass.Person GetPersonData_In_Person(List<DataClass.Person> PeopleList, int personID)
        {
            var test = PeopleList.Where(o => o.Id == personID).SingleOrDefault();
            if (test != null)
            {
                //证明父节存在
                return test;
            }
            else
            {
                //父节点不存在。
                return null;
            }
        }

        #endregion


        #region For Money
        /// <summary>
        /// 用户存钱时使用，入资金池
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="money"></param>
        /// <param name="nowCount">当前List的Count，为了编写ID</param>
        /// <returns></returns>
        public static DataClass.Account MakeMoneyAccount(int personID, decimal money, int nowCount)
        {
            DataClass.Account temp = new DataClass.Account();
            temp.Id = nowCount;
            temp.PersonID = personID;
            temp.Money = money;
            temp.Dt = DateTime.Now;

            return temp;
        }
        /// <summary>
        /// 资金分配时使用，赃款分配
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="money"></param>
        /// <param name="accountID">资金来源，从哪里来的</param>
        /// <param name="nowCount"></param>
        /// <returns></returns>
        public static DataClass.MakeMoneyToParent MakeMoneyIn(int personID, decimal money,int accountID, int nowCount)
        {
            DataClass.MakeMoneyToParent temp = new DataClass.MakeMoneyToParent();
            temp.Id = nowCount;
            temp.PersonID = personID;
            temp.Money = money;
            temp.Dt = DateTime.Now;
            temp.AccountID = accountID;

            return temp;
        }

        #endregion
    }
}
