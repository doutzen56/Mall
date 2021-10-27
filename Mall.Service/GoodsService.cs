using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.Model.Models;

namespace Mall.Service
{
    public class GoodsService: IGoodsService
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

        public bool DecreaseStock(Cart cart)
        {
            var result = true;

            return result;
        }

        /// <summary>
        /// 按需更新
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool Update(Goods goods)
        {
            return goodsRes.Update(a => a.Id == goods.Id, u => new Goods { Name = goods.Name }) > 0;
        }
    }
}
