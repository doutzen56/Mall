using Mall.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Interface.Service
{
    public interface IMemberService : IMallService
    {
        Member QueryUser(string name, string pwd);
    }
}
