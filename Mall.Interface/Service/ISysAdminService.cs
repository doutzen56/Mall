using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Interface.Service
{
    public interface ISysAdminService : IMallService
    {
        bool Login(string name, string pwd);
    }
}
