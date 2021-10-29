using Mall.Common.Ioc;
using System;

namespace Mall.Interface.Service
{
    /// <summary>
    /// IService基类
    /// </summary>
    public interface IMallService : ITransient
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
