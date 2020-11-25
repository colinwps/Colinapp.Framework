using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;


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
        /// <summary>
        /// 配置重写
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.ConnectionString, p => p.CommandTimeout(5000)); //基础配置
            optionsBuilder.AddInterceptors(new DbCommandCustomInterceptor());  //sql拦截器
        }
        /// <summary>
        /// 模型创建重载
        /// </summary>
        /// <param name="modelBuilder">模型创建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("ColinApp.Application.Mapping"));
            var typesToRegister = assembly.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Model.AddEntityType(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

    }
}
