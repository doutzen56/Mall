using System;
using System.Linq.Expressions;

namespace Mall.Core.Repositories.Model
{
    public class DbSort<T>
    {
        private string orderby;

        public DbSort(string str = null)
        {
            this.orderby = str;
        }

        /// <summary>
        /// 排序(正序)
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns></returns>
        public DbSort<T> Asc<TField>(Expression<Func<T, TField>> expression)
        {
            this.orderby += "," + (expression.Body as MemberExpression).Member.Name;
            return this;
        }

        /// <summary>
        /// 排序(反序)
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns></returns>
        public DbSort<T> Desc<TField>(Expression<Func<T, TField>> expression)
        {
            this.orderby += "," + (expression.Body as MemberExpression).Member.Name + " DESC";
            return this;
        }

        public static implicit operator string(DbSort<T> value)
        {
            return value == null ? null : value.orderby.TrimStart(',');
        }
    }
}
