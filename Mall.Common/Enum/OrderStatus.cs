using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Common.Enum
{
    public enum OrderStatus
    {
        待支付 = 1,
        已支付 = 2,
        已发货 = 4,
        已取消 = 8
    }
}
