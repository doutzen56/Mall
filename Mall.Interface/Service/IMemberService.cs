using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Interface.Service
{
    public interface IMemberService : ServiceBase
    {
        bool Login(string name, string pwd);
    }
}
