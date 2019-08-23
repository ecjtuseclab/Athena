using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
using TanMiniToolSet.Common;
using System.Configuration;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：数据库连接
//最后修改时间:2017-04-01
//修改日志：

namespace Athena.dal
{   
    public class DB
    {
        //禁止实例化，一个数据库对象的单例模式, 静态实例方便直接引用，没必要反复去建对象
        private DB()
        {

        }
        private static string _connectionString;
        public static string ConnectionString 
        {
            get 
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.AppSettings["DataProvider"];
                }
                return _connectionString; 
            }
        }

        private static  SqlSugarClient _db;
             
        public static SqlSugarClient GetInstance()
        {
            if (_db == null)
            {
                _db= new SqlSugarClient(ConnectionString);
            }
          
            return _db;
        }
    }
}