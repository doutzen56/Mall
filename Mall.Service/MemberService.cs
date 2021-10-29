using Mall.Core.Repositories.Interface;
using Mall.Interface.Service;
using Mall.Model.Models;
using Mall.Service.Base;

namespace Mall.Service
{
    public class MemberService : ServiceBase, IMemberService
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
