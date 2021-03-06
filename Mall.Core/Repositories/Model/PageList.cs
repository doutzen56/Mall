using System.Collections.Generic;

namespace Mall.Core.Repositories.Model
{
    /// <summary>
    /// 分页数据封装
    /// </summary>
    public class PageList
    {
        /// <summary>
        /// 分页数据封装
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="items">当前页数据集</param>
        public PageList(int total, int pageSize, int pageIndex, object items)
        {
            if (pageSize <= 0)
                pageSize = 5;
            if (pageIndex <= 0)
                pageIndex = 1;
            if (total < 0)
                total = 0;
            this.Total = total;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Items = items;
            this.TotalPage = total % pageSize == 0 ?
                             total / pageSize :
                             total / pageSize + 1;
        }
        public PageList(int total, int pageSize, int pageIndex, object items, dynamic extend)
            : this(total, pageSize, pageIndex, items)
        {
            this.Extend = extend;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 有无上页
        /// </summary>
        public bool HasPrev
        {
            get { return this.PageIndex > 1; }
        }
        /// <summary>
        /// 有无下页
        /// </summary>
        public bool HasNext
        {
            get
            {
                return this.PageIndex < this.TotalPage;
            }
        }
        /// <summary>
        /// 当前页数据
        /// </summary>
        public object Items { get; set; }

        public dynamic Extend { get; set; }
    }
    /// <summary>
    /// 分页数据封装
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class PageList<T> : PageList
    {
        /// <summary>
        /// 分页数据封装
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="items">当前页数据集</param>
        public PageList(int total, int pageSize, int pageIndex, List<T> items)
            : base(total, pageSize, pageIndex, items)
        {
        }
        public PageList(int total, int pageSize, int pageIndex, List<T> items, dynamic extend)
            : base(total, pageSize, pageIndex, items)
        {
            this.Extend = extend;
        }
        /// <summary>
        /// 当前页数据
        /// </summary>
        public new IList<T> Items { get; set; }
    }
}
