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
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> Delete<T>(T entity) where T : class;
        /// <summary>
        /// 批量删除实体集
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entitirs"></param>
        /// <returns></returns>
        Task<int> Delete<T>(IEnumerable<T> entitirs) where T : class;
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> Update<T>(T entity) where T : class;
        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        Task<int> Update<T>(IEnumerable<T> entities) where T : class;
        #endregion

        #region 对象实体 查询
        /// <summary>
        /// 查找一个实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<T> FindEntity<T>(object keyValue) where T : class;
        /// <summary>
        /// 查找一个实体(根据表达式)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new();
        /// <summary>
        /// 查找一个实体(根据sql)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<T> FindEntity<T>(string strSql, object dbParameter = null) where T : class, new();
        /// <summary>
        /// 查询列表(获取所有数据)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>() where T : class, new();
        /// <summary>
        /// 查询列表(获取所有数据)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>(Func<T, object> orderby) where T : class, new();
        /// <summary>
        /// 查询列表(根据表达式)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new();
        /// <summary>
        /// 查询列表(根据SQL语句)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>(string strSql) where T : class;
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strsql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>(string strsql, object dbParameter) where T : class;
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sort">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<(int total, IEnumerable<T> list)> FindList<T>(string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new();
        /// <summary>
        /// 查询列表(分页)表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<(int total, IEnumerable<T> list)> FindList<T>(Expression<Func<T, bool>> condition, string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new();
        /// <summary>
        /// 查询列表(分页)Sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, string sort, bool isAsc, int pageSize, int pageIndex) where T : class;
        /// <summary>
        /// 查询列表(分页)Sql语句 带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="sort"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, object dbParameter, string sort, bool isAsc, int pageSize, int pageIndex) where T : class;

        #endregion
        #region 数据源查询
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        Task<DataTable> FindTable(string strSql);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<DataTable> FindTable(string strSql,object dbParameter);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<(int total, DataTable)> FindTable(string strSql, string sort, bool isAsc, int pageSize, int pageIndex);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<(int total, DataTable)> FindTable(string strSql, object dbParameter, string sort,bool isAsc,int pageSize,int pageIndex);
        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        Task<object> FindObject(string strSql);
        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        Task<object> FindObject(string strSql, object dbParameter);
        /// <summary>
        /// 查询对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        Task<T> FindObject<T>(string strSql) where T : class;
        #endregion

    }
}
