using Mall.Common.Ioc;
using Mall.Core.Repositories.Enum;
using Mall.Core.Repositories.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Mall.Core.Repositories.Interface
{
    public interface IReadRepository<T> : IDisposable
    {
        #region 数据库连接

        IDbConnection Conn { get; }

        #endregion

        #region 01 Count 查询记录数

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="dbLock">数据库锁</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate, string dbLock = DbLock.Default, bool readOnly = false);

        #endregion

        #region 02 Exists 判断记录是否存在

        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="dbLock">数据库锁</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate, string dbLock = DbLock.Default, bool readOnly = false);

        #endregion

        #region 03 Get 获取单个实体

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="dbLock">数据库锁</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby = null, string dbLock = DbLock.Default, bool readOnly = false);

        #endregion

        #region 04 GetEx 获取单条记录

        ///<summary>
        ///查询单条记录
        ///</summary>
        ///<typeparam name="TResult">查询结果类型</typeparam>
        ///<param name="predicate">查询条件</param>
        ///<param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        ///<returns></returns>
        TResult GetEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector
            );

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        TResult GetEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby = null,
            string dbLock = null,
            bool readOnly = false
            );

        #endregion

        #region 05 ToList 查询实体列表

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        List<T> ToList(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="top">最多返回几条记录</param>
        /// <param name="dbLock">数据库锁</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        List<T> ToList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby = null,
            int top = 0,
            string dbLock = DbLock.Default,
            bool readOnly = false
            );

        #endregion

        #region 06 ToListEx 查询记录列表

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector
            );

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="top">最多返回几条记录（0表示不限制）</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby = null,
            int top = 0,
            string dbLock = DbLock.Default,
            bool readOnly = false
            );

        #endregion

        #region 07 PageList 查询分页数据

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize
            );

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：CreateTime DESC</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            string orderby,
            int pageIndex,
            int pageSize
            );

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            );

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：CreateTime DESC</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            string orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            );

        #endregion

        #region 08 PageListEx 查询分页记录

        /// <summary>
        /// 查询分页记录
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize);

        /// <summary>
        /// 查询分页记录
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：CreateTime DESC</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            string orderby,
            int pageIndex,
            int pageSize);

        /// <summary>
        /// 查询分页记录
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            );

        /// <summary>
        /// 查询分页记录
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 排序 例：CreateTime DESC</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbLock">数据库锁 例：DbLock.NoLock</param>
        /// <param name="readOnly">是否读从库</param>
        /// <returns></returns>
        PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            string orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            );

        #endregion

        #region 09 SQL 查询

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="TResult">返回对象类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，例： new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns>
        IEnumerable<TResult> Query<TResult>(string sql, object parms);

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="T1">返回对象类型1</typeparam>
        /// <typeparam name="T2">返回对象类型2</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，如 new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns> 
        List<IEnumerable> Query<T1, T2>(string sql, object parms);

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="T1">返回对象类型1</typeparam>
        /// <typeparam name="T2">返回对象类型2</typeparam>
        /// <typeparam name="T3">返回对象类型3</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，如 new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns>
        List<IEnumerable> Query<T1, T2, T3>(string sql, object parms);

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="T1">返回对象类型1</typeparam>
        /// <typeparam name="T2">返回对象类型2</typeparam>
        /// <typeparam name="T3">返回对象类型3</typeparam>
        /// <typeparam name="T4">返回对象类型4</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，如 new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns>
        List<IEnumerable> Query<T1, T2, T3, T4>(string sql, object parms);

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="T1">返回对象类型1</typeparam>
        /// <typeparam name="T2">返回对象类型2</typeparam>
        /// <typeparam name="T3">返回对象类型3</typeparam>
        /// <typeparam name="T4">返回对象类型4</typeparam>
        /// <typeparam name="T5">返回对象类型5</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，如 new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns>  
        List<IEnumerable> Query<T1, T2, T3, T4, T5>(string sql, object parms);

        /// <summary>
        /// 执行 SQL 语句查询单个对象
        /// </summary>
        /// <typeparam name="TResult">返回对象类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，例： new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象</returns>
        TResult ExecuteScalar<TResult>(string sql, object parms);

        #endregion

        #region 10 Execute 执行 SQL 语句

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <returns>影响行数</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// //执行SQL
        /// IUnitOfWork unitOfWork = new UnitOfWork();
        /// IRepository<SYS_User> repositoryUser = new Repository<SYS_User>(unitOfWork, new SqlConnection(connString));
        /// //执行SQL
        /// var result = repositoryUser.Execute("delete from sys_user where username=@name", new { name = "test1" });
        /// if(result<1)
        /// {
        ///     //执行SQL失败!
        /// }
        /// else
        /// {
        ///     //执行SQL成功!
        /// }
        /// ]]>
        /// </code>
        /// </example>
        int Execute(string sql, object parms);

        #endregion

    }
}
