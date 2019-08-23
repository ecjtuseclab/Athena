using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;//属性注解
using System.ComponentModel.DataAnnotations.Resources;
using System.Data.Entity;
using Athena.model;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员： 周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：ef数据库上下文对象
//最后修改时间:2017-04-25
//修改日志：

namespace Web.Models
{
   public class Context:DbContext
    {
        public Context()
            : base("name=Athena")//数据库名称
        {
          
        }
        public DbSet<area> area { get; set; }

    }
}
