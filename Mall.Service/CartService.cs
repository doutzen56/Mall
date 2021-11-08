using Mall.Common.Extend;
using Mall.Core.Redis;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.Service.Base;
using System;
using System.Collections.Generic;

namespace Mall.Service
{
    public class CartService : ServiceBase, ICartService
    {
        private RedisProvider redis;
        public CartService(RedisProvider redis)
        {
            this.redis = redis;
        }
        /// <summary>
        /// 缓存统一前缀标识
        /// </summary>
        private static readonly string KEY_PREFIX = "mall:cart:uid:";
        public void AddCart(CartDto cart, UserInfo user)
        {
            // 获取用户信息
            string key = KEY_PREFIX + user.Id;
            //获取商品ID
            string hashKey = cart.GoodsId.ToString();
            //获取数量
            int num = cart.Count;
            //获取hash操作的对象
            var hashOps = redis.GetHashKeys(key);
            if (hashOps.Contains(hashKey))
            {
                cart = redis.GetValueFromHash<CartDto>(key, hashKey);
                cart.Count = num + cart.Count;
            }
            redis.SetEntryInHash(key, hashKey, cart);
        }

        public void DeleteCarts(List<int> ids, string userId)
        {
            string key = KEY_PREFIX + userId;
            var hashOps = redis.GetHashKeys(key);
            foreach (var item in ids)
            {
                if (hashOps.Contains(item.ToString()))
                {
                    //删除商品
                    redis.RemoveEntryFromHash(key, item.ToString());
                }
            }
        }

        public List<CartDto> ListCart(UserInfo user)
        {
            //获取该用户Redis中的key
            string key = KEY_PREFIX + user.Id;
            if (!redis.ContainsKey(key))
            { //Redis中没有给用户信息
                return null;
            }
            var hashOps = redis.GetHashValues(key);
            if (hashOps == null || hashOps.Count <= 0)
            {
                return null;
            }
            List<CartDto> carts = new List<CartDto>();
            foreach (var item in hashOps)
            {
                carts.Add(item.FromJson<CartDto>());
            }
            return carts;
        }

        public void UpdateNum(int id, int num, UserInfo user)
        {
            //获取该用户Redis中的key
            string key = KEY_PREFIX + user.Id;
            var hashOps = redis.GetHashKeys(key);
            if (!hashOps.Contains(id.ToString()))
            {
                //该商品不存在
                throw new Exception("购物车商品不存在, 用户：" + user.Id + ", 商品：" + id);
            }
            //查询购物车商品
            var cart = redis.GetValueFromHash<CartDto>(key, id.ToString());
            //修改数量
            cart.Count = num;
            // 回写Redis
            redis.SetEntryInHash(key, id.ToString(), cart);
        }
    }
}
