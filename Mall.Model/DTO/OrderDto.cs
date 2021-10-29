using System.Collections.Generic;

namespace Mall.Model.DTO
{
    public class OrderDto
    {
        public long AddressId { get; set; } // 收获人地址id
        public byte PaymentType { get; set; }// 付款类型
        public List<CartDto> Carts { get; set; }// 订单详情
    }
}
