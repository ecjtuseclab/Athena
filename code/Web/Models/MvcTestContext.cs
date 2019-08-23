using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;//引入命名空间
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;//属性注解
using System.ComponentModel.DataAnnotations.Resources;

namespace Web.Models
{
    public class MvcTestContext:DbContext
    {
        public MvcTestContext()
            : base("name=MvcTest")//数据库名称
        {
        }
        public DbSet<LogOnModel> Login { get; set; }
    }
}