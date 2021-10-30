

using Mall.Hubs.Models;
using System;
using System.Threading.Tasks;

namespace Mall.Hubs.Interface
{
    public interface IPushMsg
    {
        /// <summary>
        /// 客户端接收数据
        /// </summary>
        /// <param name="clientMessageModel">消息实体类</param>
        /// <returns></returns>
        Task ReceiveMessage(ClientMessageModel clientMessageModel);
        /// <summary>
        /// 数据推送
        /// </summary>
        /// <param name="data">JSON格式的可以被Echarts识别的data数据</param>
        /// <returns></returns>
        Task PushMessage(Array data);
        /// <summary>
        /// 客户端获取自己登录后的UID
        /// </summary>
        /// <param name="clientMessageModel">消息实体类</param>
        /// <returns></returns>
        Task GetMyId(ClientMessageModel clientMessageModel);
    }
}
