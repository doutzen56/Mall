using Mall.Model.DTO;
using System.Collections.Generic;

namespace Mall.Interface.Service
{
    /// <summary>
    /// 购物车操作
    /// </summary>
    public interface ICartService : IMallService
    {
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="user"></param>
        void AddCart(CartDto cart, UserInfo user);
        /// <summary>
        /// 查询购物车
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<CartDto> ListCart(UserInfo user);
        /// <summary>
        /// 根据id更新商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        /// <param name="user"></param>
        void UpdateNum(int id, int num, UserInfo user);
        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        void DeleteCarts(List<int> ids, string userId);
    }
}
