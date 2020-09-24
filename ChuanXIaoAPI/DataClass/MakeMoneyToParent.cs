using System;

namespace ChuanXIaoAPI.DataClass
{
    /// <summary>
    /// 向上层机构分配赃款。
    /// 分赃流水
    /// 这个类的结构与Account一样，但用法不同。
    /// 
    /// </summary>
    public class MakeMoneyToParent
    {
        private int _id;
        private decimal _money;
        private DateTime _dt;
        private int _personID;
        private int _accountID;

        /// <summary>
        /// 流水编号
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 赚得赃款
        /// </summary>
        public decimal Money { get => _money; set => _money = value; }
        /// <summary>
        /// 入账日期
        /// </summary>
        public DateTime Dt { get => _dt; set => _dt = value; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public int PersonID { get => _personID; set => _personID = value; }
        /// <summary>
        /// 入账编号：资金来源
        /// </summary>
        public int AccountID { get => _accountID; set => _accountID = value; }
    }
}
