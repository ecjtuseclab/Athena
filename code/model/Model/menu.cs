using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 王露 张婷婷 陈祚松 张梦丽 艾美珍 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：菜单类
//最后修改时间:2018-04-01
//修改日志：

namespace Athena.model.Model
{
    public class menu
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string EnCode { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UrlAddress { get; set; }
        public string OpenTarget { get; set; }
        public bool IsMenu { get; set; }
        public bool IsExpand { get; set; }
        public bool IsPublic { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteUserId { get; set; }
    }
}
