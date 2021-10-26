using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Common.Ioc
{
    /// <summary>
    /// 所有派生自此接口的类，在应用程序启动时，都将被Ioc容器自动装载，生命周期为 Scoped
    /// </summary>
    public interface IScope
    {
    }
}
