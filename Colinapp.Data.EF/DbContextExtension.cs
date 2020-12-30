using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;


namespace Colinapp.Data.EF
{
    /// <summary>
    /// 数据库操作扩展
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">执行命令所需的SQL语句对应参数</param>
        /// <returns></returns>
        public static string BuilderProc(string procName, params DbParameter[] dbParameter)
        {
            StringBuilder strSql = new StringBuilder("exec " + procName);
            if (dbParameter != null)
            {
                foreach (var item in dbParameter)
                {
                    strSql.Append(" " + item + ",");
                }
                strSql = strSql.Remove(strSql.Length -1,1);
            }
            return strSql.ToString();
        }
    }
}
