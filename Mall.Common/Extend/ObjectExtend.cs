using Newtonsoft.Json;

namespace Mall.Common.Extend
{
    public static class ObjectExtend
    {
        /// <summary>
        /// 将对象序列化成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 将目标json字符串反序列化成<typeparamref name="T"/>类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
