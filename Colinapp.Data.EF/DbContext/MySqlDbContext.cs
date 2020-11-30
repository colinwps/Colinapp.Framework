using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string ConnectionString { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public MySqlDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// 重载配置文件
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, p => p.CommandTimeout(5000));
            optionsBuilder.AddInterceptors(new DbCommandCustomInterceptor());
        }
        /// <summary>
        /// 重载模型创建
        /// </summary>
        /// <param name="modelBuilder">模型构造器</param>
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
