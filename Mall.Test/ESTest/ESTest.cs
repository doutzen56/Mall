
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Test.ESTest
{
    [TestClass]
    public class ESTest
    {
        [TestInitialize]
        public void Init()
        {
            var table = Hashtable.Synchronized(new());
            table.Add("1","222");
        }

        [TestCleanup]
        public void Finished()
        {
            
        }
    }
}
