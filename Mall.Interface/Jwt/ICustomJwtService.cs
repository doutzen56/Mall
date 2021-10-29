using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Interface.Jwt
{
    public interface ICustomJwtService
    {
        /// <summary>
        /// 用户登录成功以后，用来生成Token的方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GetToken(Member user);
    }
}
