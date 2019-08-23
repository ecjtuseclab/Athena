using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//项目名称：Athena标准开发框架
//版本：0.0.1
//开发团队成员：周庆 张婷婷 王露 陈祚松 艾美珍 张梦丽 胡凯雨 陈兰兰 夏萍萍 康文洋 易传佳
//模块及代码页功能描述：article类
//最后修改时间：2018-01-26
//修改日志：

namespace Athena.model
{    
    public class article
    {
        public int id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public DateTime inserttime { get; set; }

        public DateTime edittime { get; set; }

        public int? type { get; set; }

        public int? SortID { get; set; }

        public string author { get; set; }
    }
}
