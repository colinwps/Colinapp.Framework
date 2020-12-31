using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Colinapp.Util.Linq
{
    /// <summary>
    /// Linq 扩展
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 创建一个访问属性的表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }
    }
}
