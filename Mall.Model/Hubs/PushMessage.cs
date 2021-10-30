using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Model.Hubs
{
    public class PushMessage
    {
        /// <summary>
        /// 推送消息类型，方便扩展，1=>商品
        /// </summary>
        public int MsgType { get; set; } = 1;
    }
}
