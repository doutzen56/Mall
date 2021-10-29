namespace Mall.Common.Ioc.IocOptions
{
    public class JwtTokenOptions
    {
        /// <summary>
        /// 受众
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }
    }
}
