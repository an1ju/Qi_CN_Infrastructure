using ChuanXIaoAPI.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanXIaoAPI
{
    /// <summary>
    /// 项目数据
    /// </summary>
    public class ProjectData : IprojectData
    {
        private List<DataClass.Person> PeopleList = new List<DataClass.Person>();
        private List<DataClass.Account> AccountList = new List<DataClass.Account>();
        private List<DataClass.MakeMoneyToParent> MakeMoneyList = new List<DataClass.MakeMoneyToParent>();

        /// <summary>
        /// 赃款分配比较默认值   自己与上线比值，或者说自身分红占比。
        /// 数据范围：0-1  即0-100%
        /// </summary>
        public static decimal FEN_PEI_PRECENT = (decimal)0.5;
        /// <summary>
        /// 用户MoneyOut乘以此系数。就是最大收益数据。
        /// </summary>
        public static decimal FAN_BEI_MULTIPLY = (decimal)100.00;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectData()
        {
            
        }

        public Person CreatePerson(int parentID,string name, string secret)
        {

            Person r = Tools.ToolsForProject.MakePerson(parentID, name, secret, PeopleList.Count);
            if (parentID == -1)
            {
                //创建祖宗，基本不会有错误。
                PeopleList.Add(r);
            }
            else
            {
                if (Tools.ToolsForProject.CheckParentId_Exist_In_Person(PeopleList, parentID))
                {
                    //证明父节点可用
                    PeopleList.Add(r);
                }
                else
                {
                    //父节点不可用。
                    throw new Exception("参数：【parentID】输入错误，查无此人，请核对。");
                }
            }

            return r;
        }

        public DataClass.Person SearchPerson(int personID)
        {
            return Tools.ToolsForProject.GetPersonData_In_Person(PeopleList, personID);
        }

        public List<DataClass.Person> GetAllPeople()
        {
            return PeopleList;
        }

        public DataClass.Person EditPersonName(int personID, string name)
        {
            DataClass.Person temp = Tools.ToolsForProject.GetPersonData_In_Person(PeopleList, personID);
            if (temp != null)
            {
                temp.Name = name;
            }
            else
            {
                //父节点不可用。
                throw new Exception("参数：【personID】输入错误，查无此人，请核对。");
            }
            return temp;
        }
        /// <summary>
        /// 修改资金分配比例
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="pocketPrecent"></param>
        /// <returns></returns>
        public DataClass.Person EditPersonPocketPrecent(int personID, decimal pocketPrecent)
        {
            DataClass.Person temp = Tools.ToolsForProject.GetPersonData_In_Person(PeopleList, personID);
            if (temp != null)
            {
                temp.PocketPrecent = pocketPrecent;
            }
            else
            {
                //父节点不可用。
                throw new Exception("参数：【personID】输入错误，查无此人，请核对。");
            }
            return temp;
        }

        public string CreateShareLink(int personID)
        {
            string r = "";
            DataClass.Person temp = Tools.ToolsForProject.GetPersonData_In_Person(PeopleList, personID);
            if (temp != null)
            {
                r = $"{temp.Id}";
            }
            else
            {
                //父节点不可用。
                throw new Exception("参数：【personID】输入错误，查无此人，请核对。");
            }
            return r;
        }


        /// <summary>
        /// 资金分配：这部分可以算是赚钱
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="moneyLess"></param>
        /// <param name="accountID">资金来源</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string MakeMoney(int parentID, decimal moneyLess, int accountID, ref string message)
        {
            string r = "";
            Person thisPerson = PeopleList.Where(o => o.Id == parentID).SingleOrDefault();
            if (thisPerson != null)
            {
                //有父节点

                //分配规则：
                //1、查看自身与上线的分配比例，进行计算
                //2、查看自身投资金额数量，确定自身最大可获取赃款总数，加上本次的是否超出，如果超出，超出的部分也要一同上交到上线处理、
                //3、查询上线ID，如果不是-1，进入递归过程。如果是-1，应该提交上线的资金，划归自己所有。
                #region 资金分配
                decimal canHave = thisPerson.PocketPrecent * moneyLess;//本次预计收入
                decimal lessGoToNext = moneyLess - canHave;//这些转到上线去

                if ((thisPerson.MoneyIn + canHave) <= (thisPerson.MoneyOut * FAN_BEI_MULTIPLY))
                {
                    //正常处理
                }
                else
                {
                    //重新计算本次资金分配
                    canHave = thisPerson.MoneyOut * FAN_BEI_MULTIPLY - thisPerson.MoneyIn;
                    lessGoToNext = moneyLess - canHave;
                }

                MakeMoneyToParent moneyIn = Tools.ToolsForProject.MakeMoneyIn(thisPerson.Id, canHave, accountID, MakeMoneyList.Count);
                MakeMoneyList.Add(moneyIn);

                thisPerson.MoneyIn = thisPerson.MoneyIn + canHave;


                if (thisPerson.ParentID != -1)
                {
                    //向上线上供
                    MakeMoney(thisPerson.ParentID, lessGoToNext, accountID, ref message);
                }
                else
                {
                    //自身消化
                    MakeMoneyToParent moneyInSelf = Tools.ToolsForProject.MakeMoneyIn(thisPerson.Id, lessGoToNext, accountID, MakeMoneyList.Count);
                    MakeMoneyList.Add(moneyInSelf);

                    thisPerson.MoneyIn = thisPerson.MoneyIn + lessGoToNext;
                }


                #endregion
            }
            else
            {
                throw new Exception("参数：【parentID】错误，查无此人，请核对。");
            }

            return r;
        }
        /// <summary>
        /// 被骗用户投资，相当于花钱。
        /// </summary>
        /// <param name="person"></param>
        /// <param name="moneyOut"></param>
        /// <returns></returns>
        public Person PersonSaveMoney(Person person, decimal moneyOut)
        {
            //用户存钱是一个过程：
            //1、把钱上交到平台，平台做入账记录
            //2、用户自身属于花钱，记录到moneyOut字段中
            //3、上线进入递归过程，分配资金，并记录到每个上线的moneyIn字段和MakeMoneyToParent数据中。


            person = PeopleList.Where(o => o.Id == person.Id).SingleOrDefault();//防范伪造数据，在入口直接检测封杀。
            if (person != null)
            {
                //OK
                Account account = Tools.ToolsForProject.MakeMoneyAccount(person.Id, moneyOut, AccountList.Count);
                AccountList.Add(account);//1

                person.MoneyOut = person.MoneyOut + moneyOut;//2

                string msg = "";
                if (person.ParentID!=-1)
                {
                    //提交上级
                    MakeMoney(person.ParentID, moneyOut, account.Id, ref msg);//3
                }
                else
                {
                    //自身消化
                    MakeMoneyToParent moneyInSelf = Tools.ToolsForProject.MakeMoneyIn(person.Id, moneyOut, account.Id, MakeMoneyList.Count);
                    MakeMoneyList.Add(moneyInSelf);

                    person.MoneyIn = person.MoneyIn + moneyOut;
                }
                
            }
            else
            {
                throw new Exception("参数：【person】错误，有伪造数据嫌疑，请按正规流程操作。");
            }

            return person;
        }
    }
}
