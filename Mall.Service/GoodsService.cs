using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.Model.Models;
using System;

namespace Mall.Service
{
    public class GoodsService : IGoodsService
    {
        private IRepository<Goods> goodsRes;
        public GoodsService(IRepository<Goods> goodsRes)
        {
            this.goodsRes = goodsRes;
        }
        public bool Add(Goods goods)
        {
            return goodsRes.Add(goods) > 0;
        }

        public void DecreaseStock(Cart cart)
        {
            var stock = goodsRes.GetEx(a => a.Id == cart.GoodsId, rs => rs.Stock);
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
            return goodsRes.Update(goods) > 0;
        }
    }
}
