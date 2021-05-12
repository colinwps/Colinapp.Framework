using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;


namespace Colinapp.Cache.Redis
{
    /// <summary>
    /// Redis 操作方法
    /// </summary>
    public class RedisCache
    {
        public void test()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.11.230:6379");
            IDatabase db = redis.GetDatabase(2);
            db.StringSet("kk", "1234567");
        }
    }
}
