namespace Mall.Common.Models
{
    /// <summary>
    /// 通用返回类型封装
    /// </summary>
    public class ApiResult
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

        public static ApiResult Ok()
        {
            return Ok(null);
        }
        public static ApiResult Ok(object data)
        {
            return new ApiResult() { Status = 0, Msg = "ok", Data = data };
        }
        public static ApiResult Fail()
        {
            return Fail(null);
        }
        public static ApiResult Fail(object data)
        {
            return new ApiResult() { Status = 1, Msg = "操作失败", Data = data };
        }
    }

    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public new T Data { get; set; }
    }
}
