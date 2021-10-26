using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Test.Ioc
{
    [TestClass]
    public class IocTest
    {
        [TestMethod]
        public void InitTest()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => a.GetName().FullName.StartsWith("Mall.Test"))
                            .FirstOrDefault();
            var baseType = typeof(Person);
            var list = assembly.GetTypes()
                  .Where(a => a != baseType && baseType.IsAssignableFrom(a))
                  .ToList();
        }
    }
}
