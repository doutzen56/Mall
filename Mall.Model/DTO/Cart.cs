using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Model.DTO
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 购买时数量
        /// </summary>
        public double Price { get; set; }
    }
}
