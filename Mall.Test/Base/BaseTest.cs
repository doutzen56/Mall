using Mall.Common.Ioc;
using Mall.Common.Ioc.IocOptions;
using Mall.Core.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Test
{
    [TestClass]
    public class BaseTest
    {
        protected IServiceCollection services;
        protected RedisProvider redis;
        [TestInitialize]
        public void Init()
        {
            services = new ServiceCollection();
            var allTypes = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                          .Select(Assembly.LoadFrom)
                          .Where(a => a.GetName().FullName.StartsWith("Mall."))
                          .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Any(p => p == typeof(ITransient))))
                          .ToList();

            //获取所有接口和抽象类
            var baseTypes = allTypes.Where(a => a.IsInterface || a.IsAbstract).ToList();
            //获取所有实现类
            var impTypes = allTypes.Where(a => a.IsClass && !a.IsAbstract).ToList();
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
                //}
            }
            redis = new RedisProvider(new RedisClient("localhost", 6379));
        }
        [TestCleanup]
        public void Finished()
        {
            services.Clear();
            redis.Dispose();
        }
    }
}
