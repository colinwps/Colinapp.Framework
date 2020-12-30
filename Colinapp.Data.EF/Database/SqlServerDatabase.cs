using System;
using System.Collections.Generic;
using System.Text;
using Colinapp.Data;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;


namespace Colinapp.Data.EF
{
    /// <summary>
    /// 程序名: Sqlserver 数据库读写实现
    /// 作者: ColinWang
    /// 描述: 
    /// 版本: Ver 1.0.0.1
    /// 创建时间: 2020.12.12
    /// 修改人:
    /// 修改时间:
    /// 版本: 
    /// </summary>
    public class SqlServerDatabase : IDatabase
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connString"></param>
        public SqlServerDatabase(string connString)
        {
            this.dbContext = new SqlServerDbContext(connString);
        }
        #endregion

        #region 属性

        /// <summary>
        /// 当前数据访问上下文对象
        /// </summary>
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbContextTransaction dbContextTransaction { get; set; }

        #endregion

        #region 事务
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public async Task<IDatabase> BeginTrans()
        {
            DbConnection dbConnection = this.dbContext.Database.GetDbConnection();
            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }
            this.dbContextTransaction = await this.dbContext.Database.BeginTransactionAsync();
            return this;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            await this.dbContext.DisposeAsync();
        }
        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitTrans()
        {
            try
            {
                //设置默认值 //代写
                int returnValue = await this.dbContext.SaveChangesAsync();
                if (this.dbContextTransaction != null)
                {
                    await this.dbContextTransaction.CommitAsync();
                    await this.Close();
                }
                else
                {
                    await this.Close();
                }
                return returnValue;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.dbContextTransaction == null)
                {
                    await this.Close();
                }
            }
        }
        /// <summary>
        /// 回滚当前操作
        /// </summary>
        /// <returns></returns>
        public async Task RollbackTrans()
        {
            await this.dbContextTransaction.RollbackAsync();
            await this.dbContextTransaction.DisposeAsync();
            await this.Close();
        }

        #endregion

        #region 直接执行 SQL 语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public async Task<int> ExecuteBySql(string strSql)
        {
            if (this.dbContextTransaction == null)
            {
                return await this.dbContext.Database.ExecuteSqlRawAsync(strSql);
            }
            else
            {
                await this.dbContext.Database.ExecuteSqlRawAsync(strSql);
                return this.dbContextTransaction == null ? await this.CommitTrans() : 0; 
            }
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            if (this.dbContextTransaction == null)
            {
                return await this.dbContext.Database.ExecuteSqlRawAsync(strSql, dbParameter);
            }
            else
            {
                await this.dbContext.Database.ExecuteSqlRawAsync(strSql, dbParameter);
                return this.dbContextTransaction == null ? await this.CommitTrans() : 0;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public async Task<int> ExecuteByProc(string procName)
        {
            if (this.dbContextTransaction == null)
            {
                return await this.dbContext.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName));
            }
            else
            {
                await this.dbContext.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName));
                if (this.dbContextTransaction == null)
                {
                    return await this.CommitTrans();
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 执行过程(带参数)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteByProc(string procName, DbParameter[] dbParameter)
        {
            if (this.dbContextTransaction == null)
            {
                return await this.dbContext.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName, dbParameter));
            }
            else
            {
                await this.dbContext.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName, dbParameter));
                if (this.dbContextTransaction == null)
                {
                    return await this.CommitTrans();
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion


        public Task<int> Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(IEnumerable<T> entitirs) where T : class
        {
            throw new NotImplementedException();
        }

        

        

        

        public Task<T> FindEntity<T>(object keyValue) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindEntity<T>(string strSql, object dbParameter = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>(Func<T, object> orderby) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>(string strSql) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>(string strsql, object dbParameter) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T> list)> FindList<T>(string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T> list)> FindList<T>(Expression<Func<T, bool>> condition, string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, string sort, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, object dbParameter, string sort, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<object> FindObject(string strSql)
        {
            throw new NotImplementedException();
        }

        public Task<object> FindObject(string strSql, object dbParameter)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindObject<T>(string strSql) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable(string strSql)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable(string strSql, object dbParameter)
        {
            throw new NotImplementedException();
        }

        public Task<(int total, DataTable)> FindTable(string strSql, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<(int total, DataTable)> FindTable(string strSql, object dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        

        public Task<int> Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Update<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
