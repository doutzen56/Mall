using Mall.Model.DTO;
using Mall.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Interface.Service
{
    public interface IGoodsService : ServiceBase
    {

        bool Add(Goods goods);

        bool Update(Goods goods);
        /// <summary>
        /// 缩减库存
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        void DecreaseStock(Cart cart);
    }
}
