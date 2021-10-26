using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Common.Ioc
{
    /// <summary>
    /// ioc扩展
    /// </summary>
    public static class IocExtend
    {
        /// <summary>
        /// 自动注入默认类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection DefaultRegister(this IServiceCollection services)
        {
            #region 

            #endregion
            var allTypes = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray()
                            .Where(a => a.GetName().FullName.StartsWith("Mall."))
                            .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Any(p => p == typeof(ITransient))))
                            .ToList();
            //var allTypes = new List<Type>();
            //foreach (var item in assemblies)
            //{
            //var singletons = GetTypes<ISingleton>(item);
            //var transients = GetTypes<ITransient>(item);
            //var scopes = GetTypes<IScope>(item);

            #region ISingleton

            #endregion

            #region ITransient
            //获取所有接口和抽象类
            var baseTypes = allTypes.GetInterfaces();
            //获取所有实现类
            var impTypes = allTypes.GetImplement();
            foreach (var baseType in baseTypes)
            {
                //获取当前接口的所有实现类
                var imps = impTypes.Where(a => baseType.IsAssignableFrom(a)).ToList();
                if (imps != null && imps.Count > 0)
                {
                    //将该接口的所有实现类都注入
                    foreach (var imp in imps)
                    {
                        services.AddTransient(baseType, imp);
                    }
                }
            }
            #endregion

            #region IScope

            #endregion
            //}
            return services;
        }
        /// <summary>
        /// 获取所有<typeparamref name="T"/> 类型的继承类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assenbly">目标程序集</param>
        /// <returns></returns>
        private static List<Type> GetTypes<T>(Assembly assembly)
        {
            List<Type> list = null;
            var baseType = typeof(T);
            list = assembly.GetTypes()
                  .Where(a => a != baseType && baseType.IsAssignableFrom(a))
                  .ToList();
            return list;
        }

        /// <summary>
        /// 获取类型中所有的接口类型或抽象类型
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private static List<Type> GetInterfaces(this List<Type> types)
        {
            return types.Where(a => a.IsInterface || a.IsAbstract).ToList();
        }
        /// <summary>
        /// 获取类型中所有的实现类
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private static List<Type> GetImplement(this List<Type> types)
        {
            return types.Where(a => a.IsClass && !a.IsAbstract).ToList();
        }

    }
}
