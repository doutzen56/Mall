namespace Mall.Common.Models
{
    /// <summary>
    /// 通用返回类型封装
    /// </summary>
    public class RespResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        public object Data { get; set; }

        public static RespResult Ok()
        {
            return Ok(null);
        }
        public static RespResult Ok(object data)
        {
            return new RespResult() { Status = 0, Msg = "ok", Data = data };
        }
        public static RespResult Fail()
        {
            return Fail(null);
        }
        public static RespResult Fail(object data)
        {
            return new RespResult() { Status = 1, Msg = "操作失败", Data = data };
        }
    }

    public class RespResult<T> : RespResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public new T Data { get; set; }
    }
}
