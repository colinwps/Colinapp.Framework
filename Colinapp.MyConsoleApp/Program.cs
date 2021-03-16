using System;
using Colinapp.Cache.Redis;
namespace Colinapp.MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisCache redis = new RedisCache();
            redis.test();
            Console.WriteLine("Hello World!");
        }
    }
}
