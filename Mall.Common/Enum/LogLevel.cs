namespace Mall.Common.Enum
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        Trace = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
        Debug = 16
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 系统级别日志，如框架层面的，不需要人为去干预的
        /// </summary>
        System = 1,
        /// <summary>
        /// 自定义日志，包括写在方法里面的任何自定义形式
        /// </summary>
        Custom = 2,
        /// <summary>
        /// 系统异常日志，用于捕获应用程序级别异常日志
        /// </summary>
        Exception = 4
    }
}
