using Mall.Common.Enum;
using System;

namespace Mall.Common.Models
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 日志类型,取值参考 <see cref="Mall.Common.Enum.LogType"/>
        /// </summary>
        public LogType LogType { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
