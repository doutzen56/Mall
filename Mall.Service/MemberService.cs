using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Service
{
    public class MemberService : IMemberService
    {
        private IRepository<Member> userRes;
        public MemberService(IRepository<Member> userRes)
        {
            this.userRes = userRes;
        }
        public Member QueryUser(string name, string pwd)
        {
            return userRes.Get(a => a.Name == name && a.Password == pwd);
        }
    }
}
