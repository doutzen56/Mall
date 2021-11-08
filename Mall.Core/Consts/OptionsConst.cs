using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Core.Consts
{
    public class OptionsConst
    {
        /// <summary>
        /// JWT 配置节点 key的名称
        /// </summary>
        public static string JWT_TOKEN_OPTIONS => "JwtTokenOptions";
        /// <summary>
        /// Redis 配置节点 key的名称
        /// </summary>
        public static string REDIS_CONN_OPTIONS => "RedisConnOptions";
    }
}
