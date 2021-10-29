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
            return Ok(0, "操作成功！", data);
        }
        public static RespResult Ok(string msg)
        {
            return Ok(0, msg, null);
        }
        public static RespResult Ok(string msg, object data)
        {
            return Ok(0, msg, data);
        }
        public static RespResult Ok(int status, string msg, object data)
        {
            return new RespResult() { Status = status, Msg = msg, Data = data };
        }
        public static RespResult Fail()
        {
            return Fail(null);
        }
        public static RespResult Fail(object data)
        {
            return Ok(1, "操作失败", data);
        }
        public static RespResult Fail(string msg)
        {
            return Ok(1, msg, null);
        }
        public static RespResult Fail(int status, string msg, object data)
        {
            return new RespResult() { Status = status, Msg = msg, Data = data };
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
