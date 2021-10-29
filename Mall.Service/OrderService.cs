using Mall.Common.Enum;
using Mall.Common.Extend;
using Mall.Common.Models;
using Mall.Common.Utils;
using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model;
using Mall.Model.DTO;
using Mall.Model.Models;
using Mall.Service.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Service
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly IRepository<Order> orderRes;
        private readonly IRepository<Goods> goodsRes;
        private readonly ICartService cartService;
        private readonly ILogger<OrderService> logger;


        public OrderService(IRepository<Order> orderRes,
            IRepository<Goods> goodsRes,
            ICartService cartService,
            ILogger<OrderService> logger)
        {
            this.orderRes = orderRes;
            this.goodsRes = goodsRes;
            this.cartService = cartService;
            this.logger = logger;
        }
        public long CreateOrder(OrderDto orderDto, UserInfo userInfo)
        {
            orderDto.Carts.Add(new CartDto { Count = 1, GoodsId = 2, GoodsName = "iPhoneX Max", Price = 8800 });
            var orderId = SnowflakeHelper.Next();
            AddressDTO addressDTO = AddressClient.FindById(orderDto.AddressId);
            //组装订单
            var order = new Order()
            {
                CreateTime = DateTime.UtcNow,
                Status = (int)OrderStatus.待支付,
                PaymentType = orderDto.PaymentType,
                TotalPay = orderDto.Carts.Select(a => a.Count * a.Price).Sum(a => a),
                UserId = int.Parse(userInfo.Id),
                //Address= addressDTO.address
            };
            //下单+扣减库存，强事务关联

            try
            {
                #region 开启事务(未做实现)
                //模拟扣除一个商品的库存
                var firstGoods = orderDto.Carts.First();
                //BeginTransaction()
                //1.扣库存
                var stock = goodsRes.GetEx(a => a.Id == firstGoods.GoodsId, s => s.Stock);
                if (stock >= firstGoods.Count)
                {
                    stock -= firstGoods.Count;
                    var success = goodsRes.Update(
                        a => a.Id == firstGoods.GoodsId,
                        u => new Goods { Stock = stock });
                }
                else
                {
                    throw new Exception("库存不足，扣减失败");
                }

                logger.LogInformation($"生成订单，订单编号：{orderId}，用户id：{userInfo.Id}");
                orderRes.Add(order);
                //Commit()
                #endregion 提交事务

                //清空购物车
                //实际中可异步丢到消息队列去处理
                cartService.DeleteCarts(orderDto.Carts.Select(a => a.GoodsId).ToList(), userInfo.Id);
            }
            catch (Exception ex)
            {
                //Rollback()
                var log = new LogModel
                {
                    LogType = Common.Enum.LogType.Exception,
                    LogLevel = Common.Enum.LogLevel.Error,
                    CreateTime = DateTime.UtcNow,
                    Message = $"创建订单失败，订单编号：{orderId}，用户id：{userInfo.Id},异常信息：{ex.Message}"
                };
                logger.LogError(log.ToJson());
                throw;
            }
            return orderId;
        }
    }
}
