using System;
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
        /// 
        /// </summary>
        private string ConnectionString { get; set; }
    }
}
