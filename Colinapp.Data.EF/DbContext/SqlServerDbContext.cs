﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Colinapp.Data.EF
{
    /// <summary>
    /// sql数据库连接类
    /// </summary>
    public class SqlServerDbContext:DbContext,IDisposable
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string ConnectionString { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public SqlServerDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

    }
}
