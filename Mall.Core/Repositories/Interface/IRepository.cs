using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace Mall.Core.Repositories.Interface
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable
    {
        #region 01 Add 新增实体

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        int Add(T entity);

        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        int Add(IEnumerable<T> entities);

        /// <summary>
        /// 新增实体（符合查询条件的记录为空才会新增）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="predicate">查询条件</param>
        /// <returns>影响行数</returns>
        int AddIfNotExists(T entity, Expression<Func<T, bool>> predicate);

        #endregion

        #region 02 Update 更新实体

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        int Update(T entity);

        /// <summary>
        /// 按条件更新实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="expression">更新哪些字段 例：u => new User{ Age = 31, IsActive = true }</param>
        /// <returns></returns>
        int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> expression, bool isRowLock = true);

        #endregion

        #region 03 UpdateSelect 更新实体

        List<TResult> UpdateSelect<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, T>> expression,
            Expression<Func<T, TResult>> selector,
            int top
            );

        List<T> UpdateSelect(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, T>> expression,
            int top
            );

        #endregion

        #region 04 Delete 删除实体

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// //删除用户信息
        /// IUnitOfWork unitOfWork = new UnitOfWork();
        /// IRepository<SYS_User> repositoryUser = new Repository<SYS_User>(unitOfWork, new SqlConnection(connString));
        /// //获取用户信息
        /// var user = repositoryUser.Get(x => x.UserName == "test1");
        /// //删除用户信息
        /// var result = repositoryUser.Delete(user);
        /// if(result<1)
        /// {
        ///     //删除用户信息失败!
        /// }
        /// else
        /// {
        ///     //删除用户信息成功!
        /// }
        /// ]]>
        /// </code>
        /// </example>
        int Delete(T entity);

        /// <summary>
        /// 按条件删除实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>影响行数</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// //删除用户信息
        /// IUnitOfWork unitOfWork = new UnitOfWork();
        /// IRepository<SYS_User> repositoryUser = new Repository<SYS_User>(unitOfWork, new SqlConnection(connString));
        /// //删除用户信息
        /// var result = repositoryUser.Delete(x => x.UserName == "test1");
        /// if(result<1)
        /// {
        ///     //删除用户信息失败!
        /// }
        /// else
        /// {
        ///     //删除用户信息成功!
        /// }
        /// ]]>
        /// </code>
        /// </example>
        int Delete(Expression<Func<T, bool>> predicate);

        #endregion

        #region 05 BatchInsert 批量插入

        void BatchInsert(IEnumerable<T> entities, int timeout = 60);

        #endregion

        #region 06 执行存储过程

        /// <summary>
        /// 执行存储过程，返回存储过程返回值(只支持整型)
        /// </summary>
        /// <param name="procedure">存储过程名称</param>
        /// <param name="parms">参数，可以使用输出参数和返回参数</param>
        /// <returns>存储过程返回值</returns>
        int StoreProcedure(string procedure, DbParameter[] parms);

        /// <summary>
        /// 执行存储过程，返回影响行数
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="procedure">存储过程名称</param>
        /// <param name="parms">参数，可以使用输出参数和返回参数</param>
        /// <returns>结果集</returns>
        IEnumerable<TResult> QueryStoreProcedure<TResult>(string procedure, DbParameter[] parms);

        #endregion

    }
}
