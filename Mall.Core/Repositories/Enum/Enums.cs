namespace Mall.Core.Repositories.Enum
{
    public static class DbLock
    {
        /// <summary>
        /// 默认设置
        /// </summary>
        public const string Default = "";

        /// <summary>
        /// 不添加共享锁和排它锁，可能读到未提交读的数据或“脏数据”
        /// </summary>
        public const string NoLock = "(NOLOCK)";
    }
}
