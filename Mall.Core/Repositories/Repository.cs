using Dapper;
using Mall.Common.Extend;
using Mall.Common.Utils;
using Mall.Core.Repositories.Enum;
using Mall.Core.Repositories.Expressions;
using Mall.Core.Repositories.Interface;
using Mall.Core.Repositories.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Core.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        #region 属性/构造函数

        private IDbConnection conn;
        private static bool isNonKey = SqlFormatter<T>.KeyFields.IsNull() || SqlFormatter<T>.KeyFields.Length == 0;
        public Repository(string connectString)
        {
            this.conn = new SqlConnection(connectString);
        }
        public Repository(Func<string> getConnectionString)
        {
            var str = getConnectionString();
            this.conn = new SqlConnection(str);
        }
        public Repository() 
        {
            this.conn = new SqlConnection("");
        }
        public IDbConnection Conn
        {
            get { return this.conn; }
            set { this.conn = value; }
        }


        private void ThrowIfNonKeys()
        {
            if (isNonKey)
                throw new Exception("{0}未设置主键");
        }

        #endregion

        #region 获取连接

        private IDbConnection GetConnection(bool readOnly)
        {
            return this.conn;
        }

        #endregion

        #region Count

        public int Count(Expression<Func<T, bool>> predicate, string dbLock = DbLock.Default, bool readOnly = false)
        {
            var cmd = SqlFormatter<T>.BuildCountCommand(predicate, dbLock);
            return this.GetConnection(readOnly).ExecuteScalar<int>(cmd.Sql, cmd.Parameters);
        }

        #endregion

        #region Exists

        public bool Exists(Expression<Func<T, bool>> predicate, string dbLock = DbLock.Default, bool readOnly = false)
        {
            return Count(predicate, dbLock, readOnly) > 0;
        }

        #endregion

        #region Get

        public T Get(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby = null, string dbLock = DbLock.Default, bool readOnly = false)
        {
            return GetEx<T>(predicate, null, orderby, dbLock, readOnly);
        }

        #endregion

        #region GetEx

        public TResult GetEx<TResult>(
          Expression<Func<T, bool>> predicate,
          Expression<Func<T, TResult>> selector
          )
        {
            return GetEx(predicate, selector, null);
        }

        public TResult GetEx<TResult>(
           Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector,
           Func<DbSort<T>, DbSort<T>> orderby = null,
           string dbLock = null,
           bool readOnly = false
           )
        {
            var orderstr = orderby == null ? string.Empty : orderby(new DbSort<T>());
            var cmd = SqlFormatter<T>.BuildGetCommand(predicate, selector, orderstr, dbLock);
            return this.GetConnection(readOnly).Query<TResult>(cmd.Sql, cmd.Parameters).SingleOrDefault();
        }

        #endregion

        #region ToList

        public List<T> ToList(Expression<Func<T, bool>> predicate)
        {
            return ToListEx<T>(predicate, null);
        }

        public List<T> ToList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby = null,
            int top = 0,
            string dbLock = DbLock.Default,
            bool readOnly = false
            )
        {
            return ToListEx<T>(predicate, null, orderby, top, dbLock, readOnly);
        }

        #endregion

        #region ToListEx

        public List<TResult> ToListEx<TResult>(
           Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector
           )
        {
            return ToListEx(predicate, selector, null);
        }

        public List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby = null,
            int top = 0,
            string dbLock = DbLock.Default,
            bool readOnly = false
            )
        {
            var orderstr = orderby.IsNull() ? string.Empty : orderby(new DbSort<T>());
            var cmd = SqlFormatter<T>.BuildListCommand(predicate, selector, orderstr, top, dbLock);
            return this.GetConnection(readOnly).Query<TResult>(cmd.Sql, cmd.Parameters).ToList();
        }

        #endregion

        #region PageList

        public PageList<T> PageList(
           Expression<Func<T, bool>> predicate,
           Func<DbSort<T>, DbSort<T>> orderby,
           int pageIndex,
           int pageSize)
        {
            return PageListEx<T>(predicate, null, orderby, pageIndex, pageSize);
        }

        public PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            string orderby,
            int pageIndex,
            int pageSize)
        {
            return PageListEx<T>(predicate, null, orderby, pageIndex, pageSize);
        }

        public PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            )
        {
            return PageListEx<T>(predicate, null, orderby, pageIndex, pageSize, dbLock, readOnly);
        }

        public PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            string orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            )
        {
            return PageListEx<T>(predicate, null, orderby, pageIndex, pageSize, dbLock, readOnly);
        }

        #endregion

        #region PageListEx

        public PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize)
        {
            return PageListEx(predicate, selector, orderby, pageIndex, pageSize, DbLock.Default);
        }

        public PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            string orderby,
            int pageIndex,
            int pageSize)
        {
            return PageListEx(predicate, selector, orderby, pageIndex, pageSize, DbLock.NoLock);
        }

        public PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            )
        {
            return GetPageList(predicate, selector, orderby(new DbSort<T>()), pageIndex, pageSize, dbLock, readOnly);
        }

        public PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            string orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            )
        {
            return GetPageList(predicate, selector, orderby, pageIndex, pageSize, dbLock, readOnly);
        }

        private PageList<TResult> GetPageList<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            string orderby,
            int pageIndex,
            int pageSize,
            string dbLock = DbLock.NoLock,
            bool readOnly = false
            )
        {
            var cmd = SqlFormatter<T>.BuildPageCommand(predicate, selector, orderby, pageIndex, pageSize, dbLock);
            using (var multi = this.GetConnection(readOnly).QueryMultiple(cmd.Sql, cmd.Parameters))
            {
                var total = multi.Read<long>().First();
                var items = multi.Read<TResult>().ToList();

                return new PageList<TResult>(
                    (int)total,
                    pageSize,
                    pageIndex,
                    items
                    );
            }
        }

        #endregion

        #region Query

        public IEnumerable<TResult> Query<TResult>(string sql, object parms)
        {
            return this.conn.Query<TResult>(sql, parms);
        }

        public List<IEnumerable> Query<T1, T2>(string sql, object parms)
        {
            var readedList = new List<IEnumerable>();
            using (var multi = this.conn.QueryMultiple(sql, parms))
            {
                readedList.Add(multi.Read<T1>());
                readedList.Add(multi.Read<T2>());
                return readedList;
            }
        }
        public List<IEnumerable> Query<T1, T2, T3>(string sql, object parms)
        {
            var readedList = new List<IEnumerable>();
            using (var multi = this.conn.QueryMultiple(sql, parms))
            {
                readedList.Add(multi.Read<T1>());
                readedList.Add(multi.Read<T2>());
                readedList.Add(multi.Read<T3>());
                return readedList;
            }
        }
        public List<IEnumerable> Query<T1, T2, T3, T4>(string sql, object parms)
        {
            var readedList = new List<IEnumerable>();
            using (var multi = this.conn.QueryMultiple(sql, parms))
            {
                readedList.Add(multi.Read<T1>());
                readedList.Add(multi.Read<T2>());
                readedList.Add(multi.Read<T3>());
                readedList.Add(multi.Read<T4>());
                return readedList;
            }
        }
        public List<IEnumerable> Query<T1, T2, T3, T4, T5>(string sql, object parms)
        {
            var readedList = new List<IEnumerable>();
            using (var multi = this.conn.QueryMultiple(sql, parms))
            {
                readedList.Add(multi.Read<T1>());
                readedList.Add(multi.Read<T2>());
                readedList.Add(multi.Read<T3>());
                readedList.Add(multi.Read<T4>());
                readedList.Add(multi.Read<T5>());
                return readedList;
            }
        }

        #endregion

        #region Add

        public int Add(T entity)
        {
            var cmd = SqlFormatter<T>.BuildAddCommand(entity);
            return dbAdd(entity, cmd, null);
        }

        private int dbAdd(T entity, SqlCmd cmd, IDbTransaction tran)
        {
            var auto = SqlFormatter<T>.AutoIncrement;
            if (!auto.IsNull())
            {
                var ret = conn.ExecuteScalar(cmd.Sql, cmd.Parameters, tran);
                auto.SetValue(entity, Convert.ChangeType(ret, auto.PropertyType));
                return 1;
            }
            return this.conn.Execute(cmd.Sql, cmd.Parameters, tran);
        }

        public int Add(IEnumerable<T> entities)
        {
            var cmd = SqlFormatter<T>.BuildAddCommand(entities);
            return dbAdd(entities, cmd, null);
        }

        private int dbAdd(IEnumerable<T> entities, SqlCmd cmd, IDbTransaction tran)
        {
            return this.conn.Execute(cmd.Sql, cmd.Parameters, tran);
        }

        #endregion

        #region AddIfNotExists

        public int AddIfNotExists(T entity, Expression<Func<T, bool>> predicate)
        {
            var cmd = SqlFormatter<T>.BuildAddCommand(entity, predicate);
            return dbAddIfNotExists(entity, cmd, null);
        }

        public int dbAddIfNotExists(T entity, SqlCmd cmd, IDbTransaction tran)
        {
            var ret = conn.ExecuteScalar(cmd.Sql, cmd.Parameters, tran);
            var auto = SqlFormatter<T>.AutoIncrement;
            if (!auto.IsNull())
            {
                if (ret == null) return 0;
                auto.SetValue(entity, Convert.ChangeType(ret, auto.PropertyType));
                return 1;
            }
            return (int)ret;
        }

        #endregion

        #region Update

        public int Update(T entity)
        {
            ThrowIfNonKeys();
            var cmd = SqlFormatter<T>.BuildUpdateCommand(entity);
            return dbUpdate(cmd, null);
        }

        public int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> expression, bool isRowLock = true)
        {
            var cmd = SqlFormatter<T>.BuildUpdateCommand(predicate, expression, isRowLock);
            return dbUpdate(cmd, null);
        }
        private int dbUpdate(SqlCmd cmd, IDbTransaction tran)
        {
            return this.conn.Execute(cmd.Sql, cmd.Parameters, tran);
        }

        #endregion

        #region Delete

        public int Delete(T entity)
        {
            ThrowIfNonKeys();
            var cmd = SqlFormatter<T>.BuildDeleteCommand(entity);
            return dbDelete(cmd, null);
        }

        #endregion

        #region Delete by expression

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var cmd = SqlFormatter<T>.BuildDeleteCommand(predicate);
            return dbDelete(cmd, null);
        }
        public int dbDelete(SqlCmd cmd, IDbTransaction tran)
        {
            return this.conn.Execute(cmd.Sql, cmd.Parameters, tran);
        }

        #endregion

        #region ExecuteScalar

        public TResult ExecuteScalar<TResult>(string sql, object parms)
        {
            return this.conn.ExecuteScalar<TResult>(sql, parms);
        }

        #endregion

        #region Execute

        public int Execute(string sql, object parms)
        {
            return dbExecute(sql, parms, null);
        }

        public int dbExecute(string sql, object parms, IDbTransaction tran)
        {
            return this.conn.Execute(sql, parms, tran);
        }

        #endregion

        #region UpdateSelect

        public List<TResult> UpdateSelect<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, T>> expression,
            Expression<Func<T, TResult>> selector,
            int top)
        {
            var cmd = SqlFormatter<T>.BuildUpdateSelectCommand(predicate, expression, selector, top);
            return conn.Query<TResult>(cmd.Sql, cmd.Parameters).ToList();
        }

        public List<T> UpdateSelect(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, T>> expression,
            int top)
        {
            var cmd = SqlFormatter<T>.BuildUpdateSelectCommand<T>(predicate, expression, null, top);
            return conn.Query<T>(cmd.Sql, cmd.Parameters).ToList();
        }

        #endregion

        #region BatchInsert

        public void BatchInsert(IEnumerable<T> entities, int timeout = 60)
        {
            var table = new DataTable();
            foreach (var col in SqlFormatter<T>.Fields)
            {
                if (TypeHelper.IsNullableType(col.PropertyType))
                    table.Columns.Add(col.Name, TypeHelper.GetNonNullableType(col.PropertyType));
                else
                    table.Columns.Add(col.Name, col.PropertyType);
            }

            foreach (var item in entities)
            {
                var row = table.NewRow();
                foreach (var field in SqlFormatter<T>.Fields)
                    row[field.Name] = MemberAccessor.Process(item, field) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            var openMySelf = false;
            if (this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
                openMySelf = true;
            }

            try
            {
                using (var copy = new SqlBulkCopy((SqlConnection)conn))
                {
                    copy.BatchSize = entities.Count();
                    copy.BulkCopyTimeout = timeout;
                    copy.DestinationTableName = typeof(T).Name.StartsWith("E") ? string.Format("[{0}]", "u8_" + typeof(T).Name.Substring(1)) : typeof(T).Name;
                    foreach (var field in SqlFormatter<T>.Fields)
                    {
                        copy.ColumnMappings.Add(field.Name, field.Name);
                    }
                    copy.WriteToServer(table);
                }
            }
            finally
            {
                if (openMySelf)
                    this.conn.Close();
            }
        }

        #endregion

        #region StoredProcedure

        public int StoreProcedure(string procedure, DbParameter[] parms)
        {
            var pms = new DynamicParameters();

            foreach (var pm in parms)
            {
                pms.Add(pm.ParameterName, pm.Value, pm.DbType, pm.Direction, null, null, null);
            }

            pms.Add("__return_value__", direction: ParameterDirection.ReturnValue);

            this.conn.Execute(procedure, pms, null, null, CommandType.StoredProcedure);

            // 读取输出参数
            foreach (var pm in parms)
            {
                if (pm.Direction == ParameterDirection.Output)
                {
                    pm.Value = pms.Get<object>(pm.ParameterName);
                }
            }

            return pms.Get<int>("__return_value__");
        }

        public IEnumerable<TResult> QueryStoreProcedure<TResult>(string procedure, DbParameter[] parms)
        {
            var pms = new DynamicParameters();

            foreach (var pm in parms)
            {
                pms.Add(pm.ParameterName, pm.Value, pm.DbType, pm.Direction, null, null, null);
            }

            var record = this.conn.Query<TResult>(procedure, pms, commandType: CommandType.StoredProcedure);

            // 返回输出参数
            foreach (var pm in parms)
            {
                if (pm.Direction == ParameterDirection.Output || pm.Direction == ParameterDirection.ReturnValue)
                {
                    pm.Value = pms.Get<object>(pm.ParameterName);
                }
            }

            return record;
        }

        #endregion

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.conn != null)
                        this.conn.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion

    }
}
