using Newtonsoft.Json;

namespace Mall.Common.Extend
{
    public static class ObjectExtend
    {
        /// <summary>
        /// 序列化json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
