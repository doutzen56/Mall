using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.Model.Models;
using Mall.Service.Base;
using System;

namespace Mall.Service
{
    public class GoodsService : ServiceBase, IGoodsService
    {
        private readonly IRepository<Goods> goodsRes;
        public GoodsService(IRepository<Goods> goodsRes)
        {
            this.goodsRes = goodsRes;
        }
        public bool Add(Goods goods)
        {
            return goodsRes.Add(goods) > 0;
        }

        public void DecreaseStock(CartDto cart)
        {
            var stock = goodsRes.GetEx(a => a.Id == cart.GoodsId, rs => rs.Stock);
            //判断库存是否充足
            if (cart.Count > stock)
            {
                throw new Exception("库存不足,扣减失败");
            }
            else
            {
                goodsRes.Update(
                    a => a.Id == cart.GoodsId, 
                    u => new Goods { Stock = u.Stock - cart.Count });
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool Update(Goods goods)
        {
            var result = goodsRes.Update(goods) > 0;
            if (result)
            {
                //推送数据
            }
            return result;
        }
    }
}
