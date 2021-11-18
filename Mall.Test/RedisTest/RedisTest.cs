using Mall.Common.Extend;
using Mall.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Test.RedisTest
{
    [TestClass]
    public class RedisTest : BaseTest
    {
        private string stringKey = "stringKey";
        private string hashKey = "hashKey";
        [TestMethod]
        public void AddString()
        {
            var value = "123456";
            redis.Add(stringKey, value);
            var secRs = redis.Add(stringKey, value);
            var rs = redis.Get<string>(stringKey);
            redis.Remove(stringKey);
            Assert.IsFalse(secRs);
            Assert.AreEqual(rs, value);
        }
        [TestMethod]
        public void SetString()
        {
            var value = "123456";
            redis.Add(stringKey, value);
            var secRs = redis.Set(stringKey, value);
            var rs = redis.Get<string>(stringKey);
            redis.Remove(stringKey);
            Assert.IsTrue(secRs);
            Assert.AreEqual(rs, value);
        }

        [TestMethod]
        public void HashAdd()
        {
            var user = new User { ID = 1, UserName = "test", Address = "beijing", Age = 10 };
            var user2 = new User { ID = 2, UserName = "test2", Address = "beijing2", Age = 10 };
            redis.StoreAsHash<User>(user);
            redis.StoreAsHash<User>(user2);
            var person = new Person { Id = 10, MyProperty = 1, MyProperty2 = 2, MyProperty3 = 3 };
            redis.StoreAsHash<Person>(person);
            var u = redis.GetFromHash<User>(user.ID);
            var u2 = redis.GetFromHash<User>(user2.ID);
            var p= redis.GetFromHash<Person>(person.Id);
            Assert.AreEqual(user.ID, u.ID);
            Assert.AreEqual(user2.ID, u2.ID);
            Assert.AreEqual(person.Id, p.Id);
            redis.DeleteAll<User>();
            redis.DeleteAll<Person>();
        }

        [TestMethod]
        public void ListAdd()
        {
            var name = "userList";

            var user = new User { ID = 1, UserName = "test", Address = "beijing", Age = 10 };
            var user2 = new User { ID = 2, UserName = "test2", Address = "beijing2", Age = 10 };
            var list = new List<string>() { user.ToJson(), user2.ToJson() };
            redis.AddRangeToList(name, list);
            var rs = redis.GetListCount(name);
            redis.RemoveAllFromList(name);
            Assert.AreEqual(2, rs);
        }
    }
}
