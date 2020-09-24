namespace ChuanXIaoAPI.DataClass
{
    /// <summary>
    /// 就是一个人。一个商户。一个受害者。
    /// 如果是最顶端的，或比较高级的，那还是有钱图的。
    /// </summary>
    public class Person
    {
        private int _id;
        private string _name;
        private int _parentID;
        private decimal _moneyOut;
        private decimal _moneyIn;
        private decimal _pocketPrecent;
        private string _bindSecret;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get => _name; set => _name = value; }
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
        /// 数据范围：0-1   eg. 0.7
        /// 分成比例，自己与上级的比例
        /// 使用时，把自己下线的资金，乘以此值，留个自己。剩下的向自己的上线传递
        /// </summary>
        public decimal PocketPrecent { get => _pocketPrecent; set => _pocketPrecent = value; }
        /// <summary>
        /// 绑定秘钥，依靠此字段，从微信小程序登录。
        /// </summary>
        public string BindSecret { get => _bindSecret; set => _bindSecret = value; }
    }
}
