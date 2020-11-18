using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Colinapp.Data.EF
{
    /// <summary>
    /// SQL 执行拦截器
    /// </summary>
    public class DbCommandCustomInterceptor:DbCommandInterceptor
    {
       
    }
}
