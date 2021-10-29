using Mall.Model.DTO;

namespace Mall.Interface.Service
{
    public interface IOrderService : IMallService
    {
        //创建订单
        long CreateOrder(OrderDto orderDto, UserInfo userInfo);
    }
}
