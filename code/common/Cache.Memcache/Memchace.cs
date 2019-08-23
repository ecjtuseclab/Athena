using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：Memchace缓存的设置、读取、写入、移除等
//最后修改时间：2018-01-20
//修改日志：
//2018-01-20 Memchace缓存的各种操作（张婷婷）

    
namespace Athena.common.Cache.Memcache
{   
    public class Memchace:ICache
    {
        private MemcachedClient memClient;
        public Memchace()
        {
            string[] servers = {"192.168.1.242:60426"};//设置本机IP及固定端口60426
            //初始化池
            SockIOPool spool = SockIOPool.GetInstance();
            spool.SetServers(servers);
            spool.InitConnections = 3;
            spool.MinConnections = 3;
            spool.MaxConnections = 5;
            spool.SocketConnectTimeout = 1000;
            spool.SocketTimeout = 3000;
            spool.MaxConnections = 30;
            spool.Failover = true;
            spool.Nagle = false;
            spool.Initialize();
            //客户端实例
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;
            memClient = mc;
        }
        //读取缓存
        public T GetCache<T>(string cacheKey) where T : class
        {
            return (T)memClient.Get(cacheKey);
        }
        //写入缓存
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            memClient.Add(cacheKey, value);
        }
        //写入缓存
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            memClient.Add(cacheKey, value, expireTime);
        }
        //移除指定数据缓存
        public void RemoveCache(string cacheKey)
        {
            memClient.Delete(cacheKey);
        }
        //移除全部缓存
        public void RemoveCache()
        {
            
        }
        //设置缓存过期
        public void SetCache(string key, object value, DateTime extDate)
        {
            memClient.Set(key, value, extDate);
        }
        //设置缓存
        public void SetCache(string key, object value)
        {
            memClient.Set(key,value);
        }
    }
}
