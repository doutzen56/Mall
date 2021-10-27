using Mall.Common.Ioc;
using System;

namespace Mall.Interface.Service
{
    /// <summary>
    /// Service基类
    /// </summary>
    public interface ServiceBase : ITransient
    {
        /// <summary>
        /// Service版本号，方便跟踪代码使用
        /// </summary>
        public virtual string Version
        {
            get { return Guid.NewGuid().ToString("N"); }
        }
    }
}
