using System;

namespace ChuanXIaoAPI.DataClass
{
    /// <summary>
    /// 账目：这个是整个项目组（一个传销机构还敢叫项目组？！），整体的骗得的账款，是实打实的账款。
    /// 赃款！！！单条账目。
    /// </summary>
    public class Account
    {
        private int _id;
        private decimal _money;
        private DateTime _dt;
        private int _personID;

        /// <summary>
        /// 流水编号
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 投入资金
        /// </summary>
        public decimal Money { get => _money; set => _money = value; }
        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime Dt { get => _dt; set => _dt = value; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public int PersonID { get => _personID; set => _personID = value; }
    }
}
