using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace Colinapp.Data.EF
{
    /// <summary>
    /// Mysql 数据连接类
    /// </summary>
    public class MySqlDbContext : DbContext, IDisposable
    {

    }
}
