using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanXIaoAPI.DataClass
{
    /// <summary>
    /// 就是一个人。一个商户。一个受害者。
    /// 如果是最顶端的，或比较高级的，那还是有钱图的。
    /// </summary>
    public class Person
    {
        private int _id;
        private int _name;
        private int _parentID;
        private decimal _moneyOut;
        private decimal _moneyIn;
        private decimal _pocketPrecent;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 名字
        /// </summary>
        public int Name { get => _name; set => _name = value; }
        /// <summary>
        /// 推荐人编号（上级编号）
        /// </summary>
        public int ParentID { get => _parentID; set => _parentID = value; }
        /// <summary>
        /// 投入资金
        /// </summary>
        public decimal MoneyOut { get => _moneyOut; set => _moneyOut = value; }
        /// <summary>
        /// 回收资金
        /// </summary>
        public decimal MoneyIn { get => _moneyIn; set => _moneyIn = value; }
        /// <summary>
        /// 分成比例，自己与上级的比例
        /// </summary>
        public decimal PocketPrecent { get => _pocketPrecent; set => _pocketPrecent = value; }
    }
}
