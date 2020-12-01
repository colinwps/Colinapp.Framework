using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Colinapp.Data
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 当前数据库访问上下文
        /// </summary>
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbContextTransaction dbContextTransaction { get; set; }
        #region 事务
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task<IDatabase> BeginTrans();
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task<int> CommitTrans();
        /// <summary>
        /// 回滚
        /// </summary>
        /// <returns></returns>
        Task RollbackTrans();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        Task Close();
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        Task<int> ExecuteBySql(string strSql);
        /// <summary>
        /// 执行SQL语句(带参数)
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<int> ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        Task<int> ExecuteByProc(string procName);
        /// <summary>
        /// 执行存储过程(带参数)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<int> ExecuteByProc(string procName, DbParameter[] dbParameter);
        #endregion

        #region 增删改查
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> Insert<T>(T entity) where T : class;
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体集</param>
        /// <returns></returns>
        Task<int> Insert<T>(IEnumerable<T> entities) where T : class;

        #endregion
    }
}
